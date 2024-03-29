
CREATE PROC [dbo].[SP_SheifSource_update]
(
	@Id VARCHAR(39),
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
    @reValue INT OUTPUT,
    @IsRss BIT
)
AS

DECLARE @COUNT INT
SET @COUNT = 0
SELECT  @COUNT = count(1) FROM SheifSource WHERE Id = @Id
IF(@COUNT != 1)
BEGIN
	SET @reValue = -1000000312
	RETURN;
END

UPDATE SheifSource
SET
	SourceName = @SourceName,
	SourceUrl = @SourceUrl,
	CharSet = @CharSet,
	ListAreaRole = @ListAreaRole,
	TopicListRole = @TopicListRole,
	TopicListDataRole = @TopicListDataRole,
	TopicRole = @TopicRole,
	TopicDataRole = @TopicDataRole,
	TopicPagerOld = @TopicPagerOld,
	TopicPagerTemp = @TopicPagerTemp,
	IsRss = @IsRss
WHERE ID = @Id

SET @reValue = 1

