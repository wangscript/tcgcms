ALTER PROC SP_SheifSource_Add
(
	@SourceName NVarChar(500),
    @SourceUrl NVarChar(255),
    @CharSet Char(10),
    @ListAreaRole NVarChar(1000),
    @TopicListRole NVarChar(1000),
    @TopicListDataRole NVarChar(1000),
    @TopicRole NVarChar(1000),
    @TopicDataRole NVarChar(1000),
    @TopicPagerOld NVarChar(255),
    @TopicPagerTemp NVarChar(255),
    @Id VarChar(39),
    @reValue INT OUTPUT
)
AS

DECLARE @COUNT INT
SET @COUNT = 0
SELECT  @COUNT = count(1) FROM SheifSource WHERE SourceName = @SourceName
IF(@COUNT >0)
BEGIN
	SET @reValue = -1000000301
	RETURN;
END


INSERT INTO [SheifSource]
           ([ID]
           ,[SourceName]
           ,[SourceUrl]
           ,[CharSet]
           ,[ListAreaRole]
           ,[TopicListRole]
           ,[TopicListDataRole]
           ,[TopicRole]
           ,[TopicDataRole]
           ,[TopicPagerOld]
           ,[TopicPagerTemp])
     VALUES
           (@ID
           ,@SourceName
           ,@SourceUrl
           ,@CharSet
           ,@ListAreaRole
           ,@TopicListRole
           ,@TopicListDataRole
           ,@TopicRole
           ,@TopicDataRole
           ,@TopicPagerOld
           ,@TopicPagerTemp)

SET @reValue = 1

GO