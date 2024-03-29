set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROC [dbo].[SP_Manage_GetAdminList]
(
	@iRoleId INT = 0 ,
	@vcRoleName VARCHAR(50) OUTPUT,
	@iRoleCount INT OUTPUT,
	@iAdminCount INT OUTPUT,
	@reValue INT OUTPUT 	
)
/*

DECLARE @iRoleCount INT 
DECLARE @iAdminCount INT 
DECLARE @reValue INT  
DECLARE @vcRoleName VARCHAR(50)

EXEC SP_Manage_GetAdminList
	@iRoleId = 0 ,
	@vcRoleName = @vcRoleName OUTPUT,
	@iRoleCount = @iRoleCount OUTPUT,
	@iAdminCount = @iAdminCount OUTPUT,
	@reValue = @reValue OUTPUT 

SELECT @iRoleCount,@iAdminCount,@reValue,@vcRoleName
*/
AS

SELECT @iRoleCount = COUNT(1) FROM AdminRole (NOLOCK)


IF(@iRoleId = 0)
BEGIN
	SET @vcRoleName = '所有管理员'
	SELECT @iAdminCount = COUNT(1) FROM admin (NOLOCK) WHERE cIsDel <> 'Y'
	SELECT A.vcAdminName,A.vcNickName,A.cLock,A.dAddDate,A.dUpdateDate,B.vcRoleName,B.iID
	FROM admin A (NOLOCK),AdminRole B (NOLOCK) 
	WHERE A.iRole = B.iID AND A.cIsDel <> 'Y'
END
IF(@iRoleId > 0)
BEGIN
	SELECT @iAdminCount = COUNT(1) FROM admin (NOLOCK) WHERE iRole = @iRoleId AND cIsDel <> 'Y'
	SELECT @vcRoleName = vcRoleName FROM AdminRole (NOLOCK) WHERE iId = @iRoleId
	SELECT A.vcAdminName,A.vcNickName,A.cLock,A.dAddDate,A.dUpdateDate,B.vcRoleName,B.iID
	FROM admin A (NOLOCK),AdminRole B (NOLOCK) 
	WHERE A.iRole = B.iID AND B.iID = @iRoleId AND A.cIsDel <> 'Y'
END

IF(@iRoleId =-1)
BEGIN
	SET @vcRoleName = '管理员回收站'
	SELECT @iAdminCount = COUNT(1) FROM admin (NOLOCK) WHERE cIsDel = 'Y'
	SELECT A.vcAdminName,A.vcNickName,A.cLock,A.dAddDate,A.dUpdateDate,B.vcRoleName,B.iID
	FROM admin A (NOLOCK),AdminRole B (NOLOCK) 
	WHERE A.iRole = B.iID AND A.cIsDel = 'Y'
END

SET @reValue = 1


