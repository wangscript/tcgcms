ALTER PROC SP_News_SaveOrDelResource
(
	@ids NVarChar(100),
	@Action NVARCHAR(10),
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@reValue INT OUTPUT
)
AS

DECLARE @LOG VARCHAR(2000)


IF(@ids ='' OR @ids IS NULL)
BEGIN
	SET @reValue = -1000000501 --尚未选择资源
	RETURN;
END	

IF(@vcAdminName='' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue= -1000000502 --操作员为空，您是否尚未登陆？
	RETURN
END


DECLARE @sql NVARCHAR(2000)

IF @Action = 'SAVE'
BEGIN
	SET @sql = 'UPDATE resources SET cDel = ''N'' WHERE iId IN ('+@ids+')'
	Execute Sp_Executesql @sql
	SET @LOG='救回资源['+@ids+']'
END

IF @Action = 'DEL'
BEGIN
	SET @sql = 'DELETE resources WHERE iId IN ('+@ids+')'
	Execute Sp_Executesql @sql
	SET @LOG='删除资源['+@ids+']'
END


EXEC Sp_Manage_AddAdminLog
	@vcAdminName = @vcAdminName,
	@vcActive = @LOG,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN
END
GO