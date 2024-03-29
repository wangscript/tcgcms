set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROC [dbo].[SP_News_SpecialityAdmin]
(
	@vcAdminname VARCHAR(50),
	@vcIP VARCHAR(50),
	@cAction CHAR(2) ='01',
	@iId INT = 0,
	@iSiteId INT = 0,
	@iParent INT = 0,
	@vcTitle VARCHAR(50)='',
	@vcExplain VARCHAR(50)='',
	@IDs VARCHAR(200)='',
	@reValue INT OUTPUT
)
AS


IF(@vcAdminName IS NULL OR @vcAdminName ='')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

--检测
IF(@cAction='01' OR @cAction='02')
BEGIN
--	IF(@iSiteId=0 OR @iSiteId IS NULL)
--	BEGIN
--		SET @reValue = -1000000034 --特性所属站点不明确
--		RETURN
--	END
	
	IF(@vcTitle = '' OR @vcTitle IS NULL)
	BEGIN
		SET @reValue = -1000000035 --特性名称未输入
		RETURN
	END
END


DECLARE @LOG VARCHAR(1000)
IF(@cAction='01')
BEGIN
	INSERT INTO Speciality (iSiteId,iParent,vcTitle,vcExplain,dUpDateDate)
	VALUES(@iSiteId,@iParent,@vcTitle,@vcExplain,GETDATE())
	SET @LOG = '添加新的资讯特性['+ CAST(@@IDENTITY AS VARCHAR)+']'
END

IF(@cAction='02')
BEGIN
	IF(@iParent<>0)
	BEGIN
		IF(@iId=@iParent)
		BEGIN
			SET @reValue = -1000000036 --分类ID不能为自身ID
			RETURN
		END
		DECLARE @count INT
		SET @count =0
		SELECT @count =COUNT(1) FROM Speciality (NOLOCK) WHERE iSiteId=@iSiteId AND iID =@iParent
		IF(@count=0)
		BEGIN
			SET @reValue = -1000000037 --父类ID不存在
			RETURN
		END
	END

	SET @LOG = '修改资讯特性['+ CAST(@iId AS VARCHAR)+']'
	UPDATE Speciality SET iSiteId = @iSiteId,iParent=@iParent,
	vcTitle=@vcTitle,vcExplain=@vcExplain,dUpDateDate=GETDATE()
	WHERE iID=@iId
END

IF(@cAction='03')
BEGIN
	IF(@IDs='' OR @IDs IS NULL)
	BEGIN
		SET @reValue = -1000000038 --没有选择需要删除的ID
		RETURN
	END
	IF(CHARINDEX(',',@IDs)>0)
	BEGIN
		DECLARE @sql NVARCHAR(1000)
		SET @sql = 'DELETE Speciality WHERE iId IN ('+@IDs+')'
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		DELETE Speciality WHERE iId = @IDs
	END
	
	SET @LOG='删除资源特性['+@IDs+']'
END

EXEC Sp_Manage_AddAdminLog
	@vcAdminName =@vcAdminName,
	@vcActive = @LOG,
	@vcIp = @vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN
END
SET @reValue=1


