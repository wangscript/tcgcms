USE [TCG_DB]
GO
/****** 对象:  StoredProcedure [dbo].[SP_SheifSource_Add]    脚本日期: 09/24/2010 00:31:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SP_SheifSource_Add]
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
    @reValue INT OUTPUT,
    @IsRss BIT
)
AS

DECLARE @COUNT INT
SET @COUNT = 0
SELECT  @COUNT = count(1) FROM SheifSource WHERE SourceUrl = @SourceUrl
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
           ,[TopicPagerTemp],
           IsRss)
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
           ,@TopicPagerTemp,
           @IsRss)

SET @reValue = 1

