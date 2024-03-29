set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go






ALTER PROC [dbo].[SP_Manage_GetAdminInfoByName]
(
	@vcAdminName VARCHAR(50),
	@vcIP VARCHAR(15),
	@cAction CHAR(2) = '02', --01,登陆 02获得修改资料
	@reValue INT OUTPUT
)
/*
DECLARE @reValue INT
EXEC SP_Manage_GetAdminInfoByName
	@vcAdminName ='admin',
	@vcIP = '127.0.0.1',
	@cAction = '01',
	@reValue = @reValue OUTPUT

SELECT @reValue
*/
AS

EXEC SP_Manage_CheckAdminPower
	@vcAdminName =@vcAdminName,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

IF(@cAction='01')
BEGIN

	EXEC Sp_Manage_OnlineKickTimeOutAdmin

	DECLARE @t_ip VARCHAR(15)
	DECLARE @t_online CHAR(1)
	DECLARE @cIsDel CHAR(1)

	SELECT @t_ip = vcLastLoginIp,@t_online =cIsOnline,@cIsDel=cIsDel FROM admin (NOLOCK) 
	WHERE vcAdminName = @vcAdminName 
	IF(@t_online <> 'Y')
	BEGIN
		SET @reValue = -1000000007 --该管理员还没有登陆
		RETURN;
	END
	
	IF(@t_ip<>@vcIP)
	BEGIN
		SET @reValue = -1000000008 --该管理员用其他的IP登陆了
		RETURN;
	END

	IF(@cIsDel='Y')
	BEGIN
		SET @reValue = -1000000018 --您的帐号已经被删除，请联系管理员！
		RETURN;
	END
	
	DECLARE @t_vcPopedoms VARCHAR(1000)
	DECLARE @tt_vcPopedoms VARCHAR(1000)
	
	SELECT @t_vcPopedoms = A.vcPopedom ,@tt_vcPopedoms =B.vcPopedom
	FROM  admin A (NOLOCK),  AdminRole  B (NOLOCK) 
	WHERE A.vcAdminName = @vcAdminName AND A.iRole = B.iID

	IF(@t_vcPopedoms='' OR @t_vcPopedoms IS NULL)
	BEGIN
		SET @t_vcPopedoms = '0'
	END

	IF(@tt_vcPopedoms='' OR @tt_vcPopedoms IS NULL)
	BEGIN
		SET @tt_vcPopedoms = '0'
	END
	
	SET @t_vcPopedoms = @t_vcPopedoms +','+@tt_vcPopedoms

	DECLARE @sql NVARCHAR(1000)
	SET @sql = 'SELECT iId,vcPopName,vcUrl,iParentId,cValid FROM Popedom (NOLOCK) WHERE iID IN ( ' + @t_vcPopedoms + ')'
	Execute Sp_Executesql @sql

END

SELECT A.vcAdminName,A.vcNickName,A.vcPassword,A.vcClassPopedom ,A.cLock,A.vcPopedom,A.dAddDate,
A.dUpdateDate,A.dLoginDate,A.dLastLoginDate,A.iLoginCount,A.vcLastLoginIp,A.cIsOnline,
B.vcRoleName,B.vcContent,B.vcPopedom AS vcPopedomW,B.vcClassPopedom AS vcClassPopedomW
FROM  admin A (NOLOCK),  AdminRole  B (NOLOCK) 
WHERE A.vcAdminName = @vcAdminName AND A.iRole = B.iID


SET @reValue = 1






