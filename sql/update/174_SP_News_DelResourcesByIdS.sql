
ALTER PROC SP_News_DelResourcesByIdS
(
	@ids NVARCHAR(100),
	@reValue INT OUTPUT 

/*
DECLARE @reValue INT
EXEC SP_News_DelResourcesByIdS
	@ids = '8',
	@reValue = @reValue OUTPUT

SELECT @reValue
*/
)

AS
DECLARE @sql NVARCHAR(2000)

SET XACT_ABORT ON
BEGIN TRY
BEGIN TRAN

SET @sql = 'UPDATE resources SET cDel = ''Y'',cCreated = ''N'' WHERE iId IN ('+@ids+')'
Execute Sp_Executesql @sql

SET @sql = 'SELECT * FROM resources (NOLOCK) WHERE iId IN ('+@ids+')'
Execute Sp_Executesql @sql

COMMIT
END TRY

BEGIN CATCH  
	ROLLBACK     
	SET @reValue = -1000000006		--Êý¾Ý¿â³ö´í
	RETURN;
END CATCH
SET XACT_ABORT OFF 

SET @reValue = 1
GO