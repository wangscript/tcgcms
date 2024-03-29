USE [TCG_DB]
GO
/****** Object:  User [xzdsw]    Script Date: 05/13/2011 20:58:12 ******/
CREATE USER [xzdsw] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [jodie]    Script Date: 05/13/2011 20:58:12 ******/
CREATE USER [jodie] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[AddZreo]    Script Date: 05/13/2011 20:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[AddZreo]
(
	@vcStr VARCHAR(255),
	@iMaxLen INT
)
RETURNS VARCHAR(255)
AS
BEGIN
	IF(LEN(@vcStr)>=@iMaxLen)
	BEGIN
		RETURN @vcStr
	END
	
	DECLARE @T_Count INT
	SET @T_Count = @iMaxLen - LEN(@vcStr)
	WHILE(@T_Count>0)
	BEGIN
		SET @vcStr = '0' + @vcStr
		SET @T_Count = @T_Count -1
	END
	RETURN @vcStr
END
GO
/****** Object:  UserDefinedFunction [dbo].[IsSpeciality]    Script Date: 05/13/2011 20:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[IsSpeciality](@SpeList nvarchar(1000),@Spe nvarchar(20))  
RETURNS int AS  
BEGIN 
 RETURN(CHARINDEX(','+@Spe+',',','+@SpeList+','))
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetHtmlPathForWithOutBase]    Script Date: 05/13/2011 20:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[GetHtmlPathForWithOutBase]
(
	@vcFilePath VARCHAR(255)
)
RETURNS VARCHAR(255)
AS
BEGIN
RETURN (SUBSTRING((@vcFilePath),2,LEN(@vcFilePath)-1))
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetClassUrl]    Script Date: 05/13/2011 20:58:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetClassUrl]
(
	@vcUrl VARCHAR(255),
	@vcExp VARCHAR(10)
)
RETURNS VARCHAR(255)
AS
BEGIN

DECLARE @reValue VARCHAR(255)
IF(CHARINDEX('.',@vcUrl) = 0)
BEGIN
	SET @reValue = @vcUrl + @vcExp
END
ELSE
BEGIN
	SET @reValue = @vcUrl
END

RETURN  @reValue
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TCG_GetPage]    Script Date: 05/13/2011 20:58:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TCG_GetPage]
(
    @tblName	nvarchar(50),				----要显示的表或多个表的连接
	@fldName	nvarchar(500),				----要显示的字段列表
	@fldSort	nvarchar(200),				----排序字段列表或条件,必须输入
	@strCondition	nvarchar(4000),			----查询条件,不需where,必须输入
	@pageSize	int = 20,					----每页显示的记录个数
	@page		int = 1 ,					----要显示那一页的记录
	@curpage	int = 1 OUTPUT,				----返回显示那一页的记录
	@pageCount	int = 1 OUTPUT,				----查询结果分页后的总页数
	@counts		int = 1 OUTPUT,				----查询到的记录数
	@retval		int = 1 OUTPUT				----返回状态
)
AS
	DECLARE @sql nvarchar(4000)
	DECLARE @count int    

	--取得总记录数
	SET @sql = 'SELECT @cnt=COUNT(1) FROM '
				+ @tblName
				+ ' WITH (NOLOCK)'
				+ ' WHERE ' 
				+ @strCondition
	
	--print @sql
	EXEC sp_executesql @sql,N'@cnt int OUTPUT',@cnt=@counts OUTPUT
	
	IF @counts = 0
	BEGIN
		--如果总页数为0
		SET @pageCount = 0
		SET @curpage = 0
		SET @retval = 1
		RETURN
	END
	ELSE
		SET @count = @counts

    --取得分页总数
    SET @pageCount=(@count+@pageSize-1)/@pageSize

    /**//**当前页大于总页数 取最后一页**/
	SET @curpage = @page
    IF @page>@pageCount
        SET @curpage=@pageCount
	IF @page = 0
		SET @curpage=1

	--取得分页记录结果
	SET @sql = ' SELECT '
				+ @fldName 
				+ ' FROM ( SELECT '
				+ @fldName
				+ ', ROW_NUMBER() OVER(ORDER BY '
				+ @fldSort
				+ ') AS ROW FROM '
				+ @tblName
				+ ' WITH (NOLOCK)'
				+ ' WHERE '
				+ @strCondition
				+ ') tempDB WHERE  ROW BETWEEN '
				+ CAST(@pageSize*(@curpage-1) + 1 as varchar(12))
				+ ' AND '
				+ CAST(@curpage*@pageSize as varchar(12))

	print @sql
	EXEC (@sql)

	SET @retval = 1
GO
/****** Object:  Table [dbo].[AdminLog]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminLog](
	[iID] [int] IDENTITY(1,1) NOT NULL,
	[vcAdminName] [varchar](50) NOT NULL,
	[vcActive] [nvarchar](255) NOT NULL,
	[dAddDate] [datetime] NOT NULL,
	[vcIp] [varchar](15) NOT NULL,
 CONSTRAINT [PK_T_Manage_AdminLog] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Skin]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Skin](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Pic] [nvarchar](255) NULL,
	[Info] [nvarchar](255) NULL,
	[Filename] [nvarchar](100) NULL,
 CONSTRAINT [PK_Skin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Popedom]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Popedom](
	[iID] [int] NOT NULL,
	[vcPopName] [varchar](50) NULL,
	[vcUrl] [varchar](50) NULL,
	[iParentId] [int] NULL,
	[dAddtime] [datetime] NULL,
	[cValid] [char](1) NOT NULL,
	[iOrder] [int] NOT NULL,
 CONSTRAINT [PK_popedom] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SheifSource]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SheifSource](
	[ID] [varchar](39) NULL,
	[SourceName] [nvarchar](500) NULL,
	[SourceUrl] [nvarchar](255) NULL,
	[CharSet] [char](10) NULL,
	[ListAreaRole] [nvarchar](1000) NULL,
	[TopicListRole] [nvarchar](1000) NULL,
	[TopicListDataRole] [nvarchar](1000) NULL,
	[TopicRole] [nvarchar](1000) NULL,
	[TopicDataRole] [nvarchar](1000) NULL,
	[TopicPagerOld] [nvarchar](255) NULL,
	[TopicPagerTemp] [nvarchar](255) NULL,
	[IsRss] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[filecategories]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[filecategories](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[vcFileName] [nvarchar](100) NOT NULL,
	[iParentId] [int] NOT NULL,
	[vcMeno] [nvarchar](1000) NOT NULL,
	[dCreateDate] [datetime] NOT NULL,
	[vcKey] [varchar](36) NULL,
	[MaxSpace] [bigint] NOT NULL,
	[Space] [bigint] NOT NULL,
 CONSTRAINT [PK_T_Files_Class] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[resources]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[resources](
	[iID] [int] IDENTITY(1,1) NOT NULL,
	[iClassID] [varchar](36) NULL,
	[vcTitle] [varchar](100) NOT NULL,
	[vcUrl] [varchar](200) NULL,
	[vcContent] [text] NULL,
	[vcAuthor] [varchar](50) NULL,
	[iFrom] [int] NOT NULL,
	[iCount] [int] NULL,
	[vcKeyWord] [varchar](100) NULL,
	[vcEditor] [varchar](50) NOT NULL,
	[cCreated] [char](1) NOT NULL,
	[vcSmallImg] [varchar](255) NULL,
	[vcBigImg] [varchar](255) NULL,
	[vcShortContent] [varchar](500) NULL,
	[vcSpeciality] [varchar](100) NULL,
	[cChecked] [char](1) NOT NULL,
	[cDel] [char](1) NOT NULL,
	[cPostByUser] [char](1) NULL,
	[vcFilePath] [varchar](255) NULL,
	[dAddDate] [datetime] NULL,
	[dUpDateDate] [datetime] NULL,
	[vcTitleColor] [varchar](50) NULL,
	[cStrong] [varchar](1) NULL,
	[SheifUrl] [varchar](255) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fileresources]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fileresources](
	[iID] [bigint] NOT NULL,
	[iClassId] [int] NOT NULL,
	[vcFileName] [nvarchar](100) NULL,
	[iSize] [int] NOT NULL,
	[vcType] [varchar](10) NULL,
	[iDowns] [int] NOT NULL,
	[iRequest] [int] NOT NULL,
	[vcIP] [varchar](15) NOT NULL,
	[dCreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_T_Files_FileInfos] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_T_Files_FileInfos] ON [dbo].[fileresources] 
(
	[iClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_News_DelResourcesByIdS]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_DelResourcesByIdS]
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
	SET @reValue = -1000000006		--数据库出错
	RETURN;
END CATCH
SET XACT_ABORT OFF 

SET @reValue = 1
GO
/****** Object:  Table [dbo].[AdminRefuseIp]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminRefuseIp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[vcStartIp] [bigint] NOT NULL,
	[vcEndtIp] [bigint] NOT NULL,
	[cValid] [char](1) NOT NULL,
 CONSTRAINT [PK_T_Manage_RefuseIp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Sp_Manage_IpToNum]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Manage_IpToNum]  
(  
	@ip  varchar(15),    
	@ip_value bigint OUT,
	@retval  int OUT
/*
Add By Keano at 2007-11-02
function: 得到IP地址的value值

DECLARE @i_retval int, @i_ip_value bigint  
EXEC Sp_Manage_IpToNum
	@ip = '127.0.0.1',   
	@ip_value = @i_ip_value OUT,  
	@retval = @i_retval OUT  
SELECT @i_retval, @i_ip_value
 */
) 

AS
  
DECLARE @i_power_id int
DECLARE @c_power_value varchar(500)  
  
SELECT @ip_value = CONVERT(bigint,LEFT(@ip, CHARINDEX( '.', @ip) - 1))  
 , @ip = RIGHT(@ip, LEN(@ip) - CHARINDEX( '.', @ip))  
SELECT @ip_value = @ip_value * 256 * 256 * 256  
  
SELECT @ip_value = @ip_value + CONVERT(bigint,LEFT(@ip, CHARINDEX( '.', @ip) - 1)) * 256 * 256  
 , @ip = RIGHT(@ip, LEN(@ip) - CHARINDEX( '.', @ip))  
SELECT @ip_value = @ip_value + CONVERT(bigint,LEFT(@ip, CHARINDEX( '.', @ip) - 1)) * 256   
 , @ip = RIGHT(@ip, LEN(@ip) - CHARINDEX( '.', @ip))  
SELECT @ip_value = @ip_value + CONVERT(int, @ip)   
  
SELECT @retval = 1
GO
/****** Object:  Table [dbo].[categories]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categories](
	[Id] [varchar](36) NOT NULL,
	[vcClassName] [varchar](200) NOT NULL,
	[vcName] [varchar](50) NULL,
	[dUpdateDate] [datetime] NULL,
	[iTemplate] [varchar](36) NULL,
	[iListTemplate] [varchar](36) NOT NULL,
	[vcDirectory] [varchar](200) NULL,
	[vcUrl] [varchar](255) NULL,
	[iOrder] [int] NOT NULL,
	[Visible] [char](1) NOT NULL,
	[Parent] [varchar](36) NULL,
	[DataBaseService] [varchar](50) NOT NULL,
	[SkinId] [varchar](36) NOT NULL,
 CONSTRAINT [PK_categories] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE CLUSTERED INDEX [PK_Id_Index] ON [dbo].[categories] 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdminOnline]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminOnline](
	[vcadminname] [varchar](50) NOT NULL,
	[vcIp] [varchar](15) NOT NULL,
	[dActiveTime] [datetime] NOT NULL,
	[vcActive] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_T_Manage_Online] PRIMARY KEY CLUSTERED 
(
	[vcadminname] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SP_Skin_categories_Manage]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Skin_categories_Manage]
(
	@vcClassName VARCHAR(200),
	@vcName VARCHAR(50),
	@Parent VARCHAR(36),
	@iTemplate VARCHAR(36),
	@iListTemplate VARCHAR(36),
	@vcDirectory VARCHAR(200),
	@vcUrl VARCHAR(255),
	@SkinId VARCHAR(36) = '',
	@iOrder INT,
	@action CHAR(2) ='01',
	@cVisible CHAR(1) = 'Y',
	@iClassId VARCHAR(36),
	@DataBaseService VARCHAR(50) ='',
	@reValue INT OUTPUT
)

/*

DECLARE @reValue INT
EXEC SP_News_AddClassInfo
	@vcClassName ='dsfsdg',
	@vcName ='dgg',
	@iParent =0,
	@iTemplate = 0,
	@iListTemplate=0,
	@vcDirectory='',
	@vcUrl ='',
	@iOrder =0,
	@DataBaseService=''
	@reValue =@reValue OUTPUT

SELECT @reValue
*/
AS

IF (@DataBaseService='' OR @DataBaseService IS NULL)
BEGIN
	SET @DataBaseService = 'resourceDataBase';	
END

IF(@vcClassName='' OR @vcClassName IS NULL OR @vcName='' OR @vcName IS NULL)
BEGIN
	SET @reValue=-1000000020 --分类名或别名不能为空
	RETURN
END

IF(@action='01')
BEGIN
	IF(@SkinId='' OR @SkinId IS NULL)
	BEGIN
		SET @reValue=-1000000100 --分类所属模板不能为空
		RETURN
	END
END

IF(@Parent<>'0')
BEGIN
	IF(@iTemplate='' OR @iTemplate IS NULL)
	BEGIN
		SET @reValue=-1000000021 --模版编号不能为空
		RETURN
	END

	IF(@iListTemplate='' OR @iListTemplate IS NULL)
	BEGIN
		SET @reValue=-1000000029 --列表模版编号不能为空
		RETURN
	END

	IF(@vcDirectory='' OR @vcDirectory IS NULL)
	BEGIN
		SET @reValue=-1000000022 --生成路径不能为空
		RETURN
	END

	IF(@vcUrl='' OR @vcUrl IS NULL)
	BEGIN
		SET @reValue=-1000000023 --前台分类首页不能为空
		RETURN
	END
END


IF(@action='01')
BEGIN
	
	INSERT INTO Categories(Id,vcClassName,vcName,SkinId,Parent,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible,
	            DataBaseService)
	VALUES(@iClassId,@vcClassName,@vcName,@SkinId,@Parent,@iTemplate,@iListTemplate,@vcDirectory,@vcUrl,@iOrder,@cVisible,@DataBaseService)


END

IF(@action='02')
BEGIN
	IF(@iClassId=@Parent)
	BEGIN
		SET @reValue=-1000000030 --父类ID不能为自己的ID
		RETURN;
	END

	UPDATE Categories SET vcClassName=@vcClassName,vcName=@vcName,Parent=@Parent,
	iTemplate=@iTemplate,iListTemplate=@iListTemplate,vcDirectory=@vcDirectory,vcUrl=@vcUrl,iOrder=@iOrder,
	Visible = @cVisible,DataBaseService=@DataBaseService
	WHERE ID =@iClassId
END


SET @reValue =1
GO
/****** Object:  Table [dbo].[AdminRole]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdminRole](
	[iID] [int] IDENTITY(1,1) NOT NULL,
	[vcRoleName] [varchar](50) NOT NULL,
	[vcContent] [varchar](255) NULL,
	[vcPopedom] [varchar](1000) NULL,
	[vcClassPopedom] [varchar](255) NULL,
	[dUpdateDate] [smalldatetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[admin]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[admin](
	[vcAdminName] [varchar](50) NOT NULL,
	[vcNickName] [varchar](50) NOT NULL,
	[vcPassword] [varchar](32) NOT NULL,
	[iRole] [int] NOT NULL,
	[vcPopedom] [varchar](1000) NULL,
	[vcClassPopedom] [varchar](255) NULL,
	[cLock] [char](1) NULL,
	[dAddDate] [datetime] NULL,
	[dUpdateDate] [datetime] NULL,
	[dLoginDate] [datetime] NULL,
	[dLastLoginDate] [datetime] NULL,
	[iLoginCount] [int] NULL,
	[vcLastLoginIp] [varchar](15) NULL,
	[cIsOnline] [char](1) NULL,
	[cIsDel] [char](1) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[vcAdminName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SheifCategorieConfig]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SheifCategorieConfig](
	[Id] [varchar](36) NOT NULL,
	[SheifSourceId] [nvarchar](max) NULL,
	[LocalCategorieId] [varchar](36) NULL,
	[ResourceCreateDateTime] [datetime] NULL,
 CONSTRAINT [PK_SheifCategorieConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SheifCategorieConfig] ON [dbo].[SheifCategorieConfig] 
(
	[LocalCategorieId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_News_GetNewsInfosForCreatHTML]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_GetNewsInfosForCreatHTML]
(
	@cAction CHAR(2) ='01',
	@ITimeType INT=0,
	@dStartTime DATETIME = '1981-03-06',
	@dEndTime DATETIME = '1982-09-27',
	@vcClass VARCHAR(2000) ='',
	@iNum INT=0,
	@iDel INT=0
)
AS

IF(@cAction='01')
BEGIN
	DECLARE @SQL NVARCHAR(2000)
	IF(@iDel=1)
	BEGIN
		IF(@iNum=0)
		BEGIN
			SET @SQL = 'SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''Y'' AND cDel =''N'' AND iClassID IN ('+@vcClass+')'
		END
		ELSE
		BEGIN
			SET @SQL = 'SELECT TOP '+CAST(@iNum AS VARCHAR)+' iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''Y'' AND cDel =''N'' AND iClassID IN ('+@vcClass+')'
		END
	END	
	IF(@iDel=2)
	BEGIN
		IF(@iNum=0)
		BEGIN
			SET @SQL = 'SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''Y'' AND cDel =''N'' AND iClassID IN ('+@vcClass+') AND cCreated = ''N'''
		END
		ELSE
		BEGIN
			SET @SQL = 'SELECT TOP '+CAST(@iNum AS VARCHAR)+' iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''Y'' AND cDel =''N'' AND iClassID IN ('+@vcClass+') AND cCreated = ''N'''
		END
	END

	Execute Sp_Executesql @sql
END

IF(@cAction='02')
BEGIN
	IF(@iDel=1)
	BEGIN
		IF(@ITimeType=1)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND dAddDate BETWEEN
				@dStartTime AND @dEndTime
			END
			ELSE
			BEGIN
				SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND dAddDate BETWEEN
				@dStartTime AND @dEndTime
			END
		END
		
		IF(@ITimeType=2)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND dUpdateDate BETWEEN
				@dStartTime AND @dEndTime
			END
			ELSE
			BEGIN
				SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND dUpdateDate BETWEEN
				@dStartTime AND @dEndTime
			END 
		END
	END	
	IF(@iDel=2)
	BEGIN
		IF(@ITimeType=1)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND (dAddDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = 'N'
			END
			ELSE
			BEGIN
				SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND (dAddDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = 'N'
			END
		END
		
		IF(@ITimeType=2)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND (dUpdateDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = 'N'
			END
			ELSE
			BEGIN
				SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked='Y' AND cDel ='N' AND (dUpdateDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = 'N'
			END 
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_News_CheckThiefTopic]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SP_News_CheckThiefTopic]
(
	@vcTitle VARCHAR(100),
	@iClassID INT,
	@reValue INT OUTPUT
)
AS

IF(@vcTitle = '')
BEGIN
	SET @reValue=-1000000067 --没有匹配到相关文章
	RETURN
END

DECLARE @COUNT INT
SET @COUNT = 0

SELECT @COUNT = COUNT(1) FROM  T_News_NewsInfo (NOLOCK) WHERE iClassID = @iClassID
AND vcTitle=@vcTitle

IF(@COUNT<>0)
BEGIN
	SET @reValue=-1000000068 --已经抓取了该文章
	RETURN
END
ELSE
BEGIN
	SET @reValue = 1
	RETURN
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_News_GetNewsInfoById]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_GetNewsInfoById]
(
	@iID VARCHAR(36)
)
/*
EXEC SP_News_GetNewsInfoById
	@iID='8f2fe764-2e41-4a0b-96eb-73b27aaed4b2'
*/
AS

	SELECT iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iFrom,iCount,vcKeyWord,
	vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,
	cDel,cPostByUser,vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong
	FROM resources (NOLOCK)
	WHERE iId = @iID 

	SELECT B.vcClassName,B.vcName,B.Parent,B.iTemplate,B.iListTemplate,B.vcDirectory,B.vcUrl,B.iOrder
	FROM resources A (NOLOCK),Categories B (NOLOCK) 
	WHERE A.iId = @iID AND A.iClassID = B.Id
GO
/****** Object:  UserDefinedFunction [dbo].[GetFilePath]    Script Date: 05/13/2011 20:58:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetFilePath]
(
	@vcUrl VARCHAR(255),
	@vcFilePath VARCHAR(255)
)
RETURNS VARCHAR(255)
AS
BEGIN

DECLARE @reValue VARCHAR(255)
IF(@vcUrl IS NULL OR @vcUrl = '')
BEGIN
	SET @reValue = @vcFilePath
END
ELSE
BEGIN
	SET @reValue = @vcUrl
END

RETURN  @reValue
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[User](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PassWord] [varchar](32) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NOT NULL,
	[State] [int] NOT NULL,
	[LastLoginIp] [varchar](32) NOT NULL,
	[UserLevel] [int] NOT NULL,
	[UserClubLevel] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserContact]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserContact](
	[UserId] [varchar](36) NOT NULL,
	[UserRealName] [nvarchar](50) NULL,
	[Province] [int] NULL,
	[City] [int] NULL,
	[District] [int] NULL,
	[Fulladdress] [nvarchar](200) NULL,
	[Postcode] [char](10) NULL,
	[Telephone] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserContact] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserPay]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[UserPay](
	[UserId] [varchar](36) NOT NULL,
	[FreeMoney] [money] NOT NULL,
	[FrezzMoney] [money] NOT NULL,
	[SumMoney] [money] NOT NULL,
	[Points] [money] NOT NULL,
	[PayPassWord] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_UserPay] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VisitorComment]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VisitorComment](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[vcTitle] [varchar](50) NULL,
	[vcContent] [varchar](2000) NULL,
	[vcEmail] [varchar](50) NULL,
	[iResId] [int] NULL,
	[vcIp] [varchar](15) NULL,
	[dAddDate] [datetime] NULL,
 CONSTRAINT [PK_T_News_VisitorComment] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_T_News_VisitorComment] ON [dbo].[VisitorComment] 
(
	[iResId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Template]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Template](
	[id] [varchar](36) NOT NULL,
	[SkinId] [varchar](36) NOT NULL,
	[TemplateType] [int] NOT NULL,
	[iParentId] [varchar](36) NOT NULL,
	[iSystemType] [int] NOT NULL,
	[vcTempName] [varchar](50) NOT NULL,
	[vcContent] [text] NULL,
	[vcUrl] [varchar](255) NULL,
	[dUpdateDate] [smalldatetime] NULL,
	[dAddDate] [smalldatetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Speciality](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[iSiteId] [int] NOT NULL,
	[iParent] [int] NOT NULL,
	[vcTitle] [varchar](50) NOT NULL,
	[vcExplain] [varchar](200) NULL,
	[dUpDateDate] [datetime] NULL,
 CONSTRAINT [PK_T_News_Speciality] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[GetNewsFilePath]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetNewsFilePath]
(
	@ClassFilePath VARCHAR(200),
	@dAddDate DATETIME,
	@vcExtension VARCHAR(6),
	@iID INT
)
RETURNS VARCHAR(255)
AS
BEGIN

DECLARE @t_Patch VARCHAR(255)

SET @t_Patch=@ClassFilePath
SET @t_Patch = @t_Patch + CAST(YEAR(@dAddDate) AS VARCHAR) + '/'
SET @t_Patch = @t_Patch + dbo.AddZreo(CAST(Month(@dAddDate) AS VARCHAR),2)
SET @t_Patch = @t_Patch + dbo.AddZreo(CAST(DAY(@dAddDate) AS VARCHAR),2) + '/'
SET @t_Patch = @t_Patch + dbo.AddZreo(CAST(@iID AS VARCHAR),10)
SET @t_Patch = @t_Patch + @vcExtension
RETURN @t_Patch
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_Manage_AddAdminLog]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_Manage_AddAdminLog]
(
	@vcAdminName VARCHAR(50),
	@vcActive NVARCHAR(255),
	@vcIp VARCHAR(15),
	@reValue INT OUTPUT
)--WITH ENCRYPTION 
AS


IF(@vcAdminName IS NULL OR @vcAdminName ='')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

INSERT INTO AdminLog (vcAdminName,vcActive,vcIp)
VALUES(@vcAdminName,@vcActive,@vcIp)

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Skin_CreateSkin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Skin_CreateSkin]
(
	@Id VARCHAR(36),
	@Name NVARCHAR(50),
	@Pic NVARCHAR(255),
	@Info NVARCHAR(255),
	@Filename NVARCHAR(100),
	@reValue INT OUTPUT
)
AS

DECLARE @COUNT INT
SET @COUNT = 0

SELECT @COUNT= COUNT(1) FROM Skin WHERE id = @Id
IF(@COUNT >0)
BEGIN
	 UPDATE Skin SET Id=@Id,[Name]=@Name,Pic=@Pic,Info=@Info,Filename=@Filename WHERE id = @Id
END
ELSE
BEGIN
	INSERT INTO Skin (ID,[NAME],PIC,INFO,Filename) VALUES(@Id,@Name,@Pic,@Info,@Filename)
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_SheifSource_update]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[SP_SheifSource_Add]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_SheifSource_Add]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_Files_FileInfoManageByAdmin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Files_FileInfoManageByAdmin]
(
	@cAction CHAR(2) = '01',
	@vcAdminName VARCHAR(50) = '',
	@vcIp VARCHAR(15)='',
	@iID BIGINT = 0,
	@iClassId INT = 0,
	@vcFileName NVARCHAR(100)='',
	@iSize INT = 0,
	@vcType VARCHAR(10) ='',
	@iDowns INT =0,
	@iRequest INT =0,
	@reValue INT OUTPUT
)
AS

IF(@cAction='01')
BEGIN
	IF(@vcAdminName IS NULL OR @vcAdminName ='')
	BEGIN
		SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
		RETURN;
	END

	IF(@iClassId =0 OR @iClassId IS NULL)
	BEGIN
		SET @reValue=-1000000060 --文件所在的文件夹不能为空
		RETURN;
	END
	IF(@vcFileName ='' OR @vcFileName IS NULL)
	BEGIN
		SET @reValue=-1000000061 --文件名称不能为空
		RETURN;
	END
	IF(@vcType ='' OR @vcType IS NULL)
	BEGIN
		SET @reValue=-1000000062 --文件类型不能为空
		RETURN;
	END

	IF(@iSize =0 OR @iSize IS NULL)
	BEGIN
		SET @reValue=-1000000063 --文件大小不能为空
		RETURN;
	END

	INSERT INTO fileresources (iID,iClassId,vcFileName,iSize,vcType,dCreateDate,vcIP)
	VALUES(@iID,@iClassId,@vcFileName,@iSize,@vcType,GETDATE(),@vcIP)
END

IF(@cAction='02')
BEGIN
	IF(@iID =0 OR @iID IS NULL)
	BEGIN
		SET @reValue=-1000000064 --需要删除的文件间编号不能为空
		RETURN;
	END
	DELETE fileresources WHERE iID = @iID
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_CheckIP]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_CheckIP]
(
	@vcIP VARCHAR(15),
	@reValue INT OUTPUT
)

/*

DECLARE @reValue INT
EXEC SP_Manage_CheckIP
	@vcIP = '127.0.0.1',
	@reValue = @reValue OUTPUT

SELECT @reValue
*/
AS

DECLARE @IP_Count int
DECLARE @IP_NUM BIGINT
SET @IP_Count = 0

EXEC Sp_Manage_IpToNum
	@ip = @vcIP,   
	@ip_value = @IP_NUM OUT,  
	@retval = @reValue OUT 

SELECT @IP_Count = count(*) FROM AdminRefuseIp (NOLOCK) WHERE 
vcStartIp<=@IP_NUM and vcEndtIp>=@IP_NUM and cValid = 'Y'

IF(@IP_Count>0)
BEGIN
	SET @reValue = -1000000001 --用户IP被锁定
	RETURN
END
ELSE
BEGIN
	SET @reValue = 1
	RETURN;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_Manage_OnlineKickTimeOutAdmin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_Manage_OnlineKickTimeOutAdmin]

AS
DECLARE @TimeOut int
SET @TimeOut = 60*30 --秒

update admin SET cIsOnline='N' 
WHERE  vcAdminName in (SELECT DISTINCT vcadminname FROM AdminOnline (NOLOCK) 
WHERE DATEDIFF(s, dActiveTime, GETDATE())> @TimeOut)

DELETE FROM AdminOnline 
WHERE DATEDIFF(s, dActiveTime, GETDATE())> @TimeOut
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_AdminLogout]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_AdminLogout]
(
	@vcAdminName VARCHAR(50)
)
AS
	UPDATE admin SET cIsOnline = 'N' WHERE vcAdminName =@vcAdminName
	DELETE AdminOnline WHERE vcAdminName =@vcAdminName
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_GetAdminList]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_GetAdminList]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_GetAdminRoleInfo]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_GetAdminRoleInfo]
(
	@admincount INT OUTPUT,
	@deladmincount INT OUTPUT,
	@reValue INT OUTPUT
)
/*
DECLARE @admincount INT
DECLARE @reValue INT 
EXEC SP_Manage_GetAdminRoleInfo
	@admincount = @admincount OUTPUT,
	@reValue = @reValue OUTPUT

SELECT @admincount,@reValue
*/
AS

SELECT  @admincount = COUNT(1) FROM  admin (NOLOCK) WHERE cIsDel <> 'Y'

SELECT  @deladmincount = COUNT(1) FROM  admin (NOLOCK) WHERE cIsDel = 'Y'

SELECT iID,vcRoleName,(SELECT COUNT(1) FROM admin WHERE iRole = A.iID AND cIsDel <> 'Y') AS num 
FROM dbo.AdminRole A (NOLOCK) ORDER BY num DESC

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_UserLogin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_UserLogin]
(
	@UserName NVARCHAR(50),
	@UserPassWord VARCHAR(32),
	@reValue INT OUTPUT
)
AS

IF(@UserName ='' OR @UserName IS NULL)
BEGIN
	SET @reValue = -1000000101	--用户名不能为空
	RETURN;
END

IF(@UserPassWord = '' OR  @UserPassWord IS NULL)
BEGIN
	SET @reValue = -1000000102  --密码不能为空
	RETURN;
END

DECLARE @t_UserStat INT
DECLARE @t_password VARCHAR(32)
SELECT @t_UserStat = STATE ,@t_password = [PassWord] FROM [User] (NOLOCK) WHERE [Name] = @UserName

IF(@t_UserStat IS NULL)
BEGIN
	SET @reValue = -1000000103   --该用户名还未注册
	RETURN;
END

IF(@t_password!=@UserPassWord)
BEGIN
	SET @reValue = -1000000104	-- 你输入的密码不正确
	RETURN;
END


SET @reValue = 1
SELECT * FROM [User] (NOLOCK) WHERE [Name] = @UserName
GO
/****** Object:  StoredProcedure [dbo].[SP_UserManage]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_UserManage]
(
	@State INT ,		--
	@UserLevel INT ,
	@UserClubLevel INT,
	@LastLoginTime DATETIME,
	@Id VARCHAR(36),
	@PassWord VARCHAR(32),		--
	@LastLoginIp VARCHAR(32),		--
	@Name NVARCHAR(50),		--
	@Email NVARCHAR(50),
	@action CHAR(2) ='01',		--操作01，添加。02，修改。03，删除
	@reValue INT OUTPUT 		--返回参数，小于0为异常
)
AS
IF(@action='01')
BEGIN
	
	If(@PassWord='')
	BEGIN
		SET @reValue = -1000000090	--密码不能为空！
		RETURN;
	END
	If(@LastLoginIp='')
	BEGIN
		SET @reValue = -1000000091	--最后登陆IP不能为空！
		RETURN;
	END
	If(@Name='')
	BEGIN
		SET @reValue = -1000000092	--姓名不能为空！
		RETURN;
	END
	
	INSERT INTO [USER] (State,UserLevel,UserClubLevel,CreateTime,LastLoginTime,Id,PassWord,LastLoginIp,[NAME])
	VALUES(@State,@UserLevel,@UserClubLevel,GETDATE(),GETDATE(),@Id,@PassWord,@LastLoginIp,@Name)
	
	IF(@Email!='')
	BEGIN
		INSERT INTO UserContact
		(
			UserId,
			UserRealName,
			Province,
			City,
			District,
			Fulladdress,
			Postcode,
			Telephone,
			Mobile,
			Email
		)
		VALUES
		(
			@Id,
			'',
			0,
			0,
			0,
			'',
			'',
			'',
			0,
			@Email
		)
	END
END

IF(@action='02')
BEGIN
	IF(@Id='')
	BEGIN
		SET @reValue = -1000000093 --需要修改的用户ID为空
		RETURN;
	END

	If(@PassWord='')
	BEGIN
		SET @reValue = -1000000090	--密码不能为空！
		RETURN;
	END
	If(@LastLoginIp='')
	BEGIN
		SET @reValue = -1000000091	--最后登陆IP不能为空！
		RETURN;
	END
	If(@Name='')
	BEGIN
		SET @reValue = -1000000092	--姓名不能为空！
		RETURN;
	END
	
	UPDATE [USER] SET 
	  State=@State,UserLevel=@UserLevel,
	  UserClubLevel=@UserClubLevel,
	  LastLoginTime=@LastLoginTime,
	  PassWord=@PassWord,
	  LastLoginIp=@LastLoginIp
	  WHERE Id=@Id
END


SET @reValue=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Files_FilesClassManage]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Files_FilesClassManage]
(
	@vcAdminName VARCHAR(50),
	@vcIP VARCHAR(15),
	@cAction CHAR(2) ='01',
	@iId INT = 0,
	@vcFileName NVARCHAR(100),
	@iParentId int =0,
	@vcMeno NVARCHAR(100),
	@reValue INT OUTPUT
)
AS
IF(@vcAdminName IS NULL OR @vcAdminName ='')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@vcFileName='' OR @vcFileName IS NULL)
BEGIN
	SET @reValue=-1000000057 --文件名不能为空
	RETURN
END

IF(@vcMeno='' OR @vcMeno IS NULL)
BEGIN
	SET @reValue=-1000000058 --简单说明不能为空 
	RETURN
END

DECLARE @TID INT
DECLARE @LOG NVARCHAR(100)

IF(@cAction='01')
BEGIN
	INSERT INTO filecategories (vcFileName,iParentId,vcMeno) VALUES(@vcFileName,@iParentId,@vcMeno)

	SET @TID=@@IDENTITY
	SET @LOG ='添加文件夹'+ @vcFileName
END

IF(@cAction='02')
BEGIN
	DECLARE @T_FILENAME NVARCHAR(100)
	DECLARE @T_DO NVARCHAR(100)
	SET @T_DO = ''
	
	SELECT @T_FILENAME = vcFileName,@T_DO=vcMeno FROM filecategories WHERE iId = @iId
	IF(@T_DO='')
	BEGIN
		SET @reValue=-1000000059 --修改的分类不存在
		RETURN
	END
	
	UPDATE filecategories SET vcFileName=@vcFileName,vcMeno=@vcMeno,iParentId=@iParentId
	WHERE iId = @iId

	IF(@T_FILENAME<>@vcFileName)
	BEGIN
		SET @LOG = '修改文件夹名'+@T_FILENAME+'为'+@vcFileName+' '
	END
	ELSE
	BEGIN
		SET @LOG = '修改文件夹'+@T_FILENAME
	END
	
	
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
SET @reValue =1
GO
/****** Object:  StoredProcedure [dbo].[SP_News_NewsInfoManage]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_NewsInfoManage]
(
	@cAction CHAR(2) ='01',
	@iId VARCHAR(36) = '',
	@iClassID VARCHAR(36),
	@vcTitle VARCHAR(100),	--资讯标题
	@vcUrl VARCHAR(255),	--跳转地址
	@vcContent TEXT,		--资讯内容
	@vcAuthor VARCHAR(50)='',	
	@iCount INT = 0,
	@vcKeyWord VARCHAR(100),	--资讯关键字
	@vcEditor VARCHAR(50),	--资讯编辑者
	@cCreated CHAR(1)='N',
	@cPostByUser CHAR(1)='N',
	@vcSmallImg VARCHAR(255),
	@vcBigImg VARCHAR(255),
	@vcShortContent VARCHAR(500),
	@vcSpeciality VARCHAR(100),
	@cChecked CHAR(1)='N',
	@cDel CHAR(1) ='N',
	@vcExtension VARCHAR(6),
	@cShief CHAR(2)='01',
	@cStrong CHAR(1)='N',
	@vcTitleColor VARCHAR(10)='',
	@vcFilePath VARCHAR(255) OUTPUT,--资讯路径
	@iIDOut INT OUTPUT,
	@reValue INT OUTPUT,
	@SheifUrl VARCHAR(255) = ''
)
/*

DECLARE @reValue INT
EXEC SP_News_NewsInfoManage
	@iClassID=0,
	@vcTitle ='',	--资讯标题
	@vcUrl ='',	--跳转地址
	@vcContent ='',		--资讯内容
	@vcAuthor ='',	
	@iFrom =0,	--资讯来源
	@vcKeyWord ='',	--资讯关键字
	@vcEditor ='',	--资讯编辑者
	@vcSmallImg ='',
	@vcBigImg ='',
	@vcShortContent ='',
	@vcSpeciality ='',
	@cChecked ='N',
	@vcFilePath ='',--资讯路径
	@reValue = @reValue OUTPUT

SELECT @reValue
*/
AS

IF(@vcTitle='' OR @vcTitle IS NULL)
BEGIN
	SET @reValue = -1000000039 --资讯标题不能为空
	RETURN
END

IF(@vcAuthor='')
BEGIN
	SET @vcAuthor=@vcEditor
END

IF(@vcEditor='' OR @vcEditor IS NULL)
BEGIN
	SET @reValue = -1000000041 --资讯编辑者不能为空
	RETURN
END


IF(@iClassID ='' OR @iClassID IS NULL)
BEGIN
	SET @reValue = -1000000056 --资讯分类不能为空
	RETURN
END

IF(@vcKeyWord ='' OR @vcKeyWord IS NULL)
BEGIN
	SET @reValue = -1000000043 --资讯关键字不能为空
	RETURN
END

IF(@cShief='02')
BEGIN
	DECLARE @C INT
	SET @C=0
	SELECT @C=COUNT(1) FROM resources (NOLOCK) WHERE SheifUrl = @SheifUrl
	IF(@C>0)
	BEGIN
		SET @reValue = -1000000065 --抓取文章标题不能重复
		RETURN;
	END
END

DECLARE @ClassPath VARCHAR(200)
SET @ClassPath=''
SELECT @ClassPath = vcDirectory FROM Categories WHERE Id=@iClassID
IF(@ClassPath='' OR @ClassPath IS NULL)
BEGIN
	SET @reValue = -1000000045 --资讯分类不存在
	RETURN
END

SET XACT_ABORT ON
BEGIN TRY
BEGIN TRAN

	IF(@cAction='01')
	BEGIN
		DECLARE @DATE DATETIME
		SET @DATE = GETDATE()
		
		INSERT INTO resources (iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iCount,vcKeyWord,
		vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,cDel,cPostByUser,
		vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong,SheifUrl) VALUES(@iClassID,@vcTitle,@vcUrl,@vcContent,@vcAuthor,
		@iCount,@vcKeyWord,@vcEditor,@cCreated,@vcSmallImg,@vcBigImg,@vcShortContent,@vcSpeciality,
		@cChecked,@cDel,@cPostByUser,@vcFilePath,@DATE,@DATE,@vcTitleColor,@cStrong,@SheifUrl)
	
		SET @iIDOut = @@IDENTITY

		SET @vcFilePath = dbo.GetNewsFilePath(@ClassPath,@DATE,@vcExtension,@iIDOut)
		UPDATE resources SET vcFilePath=@vcFilePath WHERE iId = @iIDOut
	END
	
	IF(@cAction='02')
	BEGIN
		IF(@iId='')
		BEGIN
			SET @reValue = -1000000046 --修改文章的ID不能为0
			RETURN
		END
		
		DECLARE @T_COUNT INT
		SET @T_COUNT = 0
		SET @DATE = NULL
		SELECT @DATE = dAddDate FROM resources (NOLOCK) WHERE iId=@iId
		IF(@DATE IS NULL)
		BEGIN
			SET @reValue = -1000000047 --修改文章不存在
			RETURN
		END
		
		SET @vcFilePath = dbo.GetNewsFilePath(@ClassPath,@DATE,@vcExtension,@iId)

		UPDATE resources SET iClassID=@iClassID,vcTitle=@vcTitle,vcUrl=@vcUrl,vcContent=@vcContent,
		vcAuthor=@vcAuthor,iCount=@iCount,vcKeyWord=@vcKeyWord,vcEditor=@vcEditor,
		cCreated=@cCreated,vcSmallImg=@vcSmallImg,vcBigImg=@vcBigImg,vcShortContent=@vcShortContent,
		vcSpeciality=@vcSpeciality,cChecked=@cChecked,cDel=@cDel,cPostByUser=@cPostByUser,vcFilePath=@vcFilePath,
		dUpDateDate=GETDATE(),vcTitleColor = @vcTitleColor,cStrong = @cStrong WHERE iId = @iId
		
	END
COMMIT
END TRY

BEGIN CATCH  
	ROLLBACK     
	SET @reValue = -1000000006		--数据库出错
	RETURN;
END CATCH
SET XACT_ABORT OFF 

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_News_DelNewsClassById]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_DelNewsClassById]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@iClassId VARCHAR(36),
	@reValue INT OUTPUT
)
AS

IF(@vcAdminName ='' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@iClassId = '0' OR @iClassId IS NULL OR @iClassId ='')
BEGIN
	SET @reValue=-1000000031 --资讯分类编号为空
	RETURN;
END

DECLARE @n_count INT
SET @n_count = 0
SELECT @n_count =  COUNT(1) FROM resources (NOLOCK) WHERE iClassID = @iClassId
IF(@n_count>0)
BEGIN
	SET @reValue=-1000000032 --该分类下还存在资源，请移出后再删除
	RETURN;
END

SET @n_count = 0
SELECT @n_count =  COUNT(1) FROM Categories (NOLOCK) WHERE Parent = @iClassId
IF(@n_count>0)
BEGIN
	SET @reValue=-1000000033 --该分类下还存在子分类，请移出后再删除
	RETURN;
END

DELETE Categories WHERE ID=@iClassId

DECLARE @LOG VARCHAR(100)
SET @LOG ='删除分类['+CAST(@iClassId AS VARCHAR)+']'

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
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_OnlineMdy]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_OnlineMdy]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vcActive NVARCHAR(255),
	@reValue INT OUTPUT
)
AS

EXEC Sp_Manage_AddAdminLog
	@vcAdminName = @vcAdminName,
	@vcActive = @vcActive,
	@vcIp = @vcIp,
	@reValue=@reValue OUTPUT

IF(@reValue<0)
BEGIN
	EXEC Sp_Manage_OnlineKickTimeOutAdmin
	RETURN;
END

DECLARE @n_count INT
SET @n_count = 0

SELECT @n_count = COUNT(1) FROM AdminOnline (NOLOCK) WHERE vcAdminName = @vcAdminName

IF(@n_count=0)
BEGIN
	UPDATE admin SET cIsOnline = 'Y',vcLastLoginIp=@vcIp WHERE vcAdminName=@vcAdminName
	INSERT INTO AdminOnline (vcAdminName,vcIp,dActiveTime,vcActive)
	VALUES(@vcAdminName,@vcIp,GETDATE(),@vcActive)
END

IF(@n_count=1)
BEGIN
	UPDATE AdminOnline SET dActiveTime = GETDATE(),vcActive = @vcActive
	WHERE vcAdminName = @vcAdminName
END

IF(@n_count>1)
BEGIN
	DELETE AdminOnline WHERE vcAdminName = @vcAdminName
	INSERT INTO AdminOnline (vcAdminName,vcIp,dActiveTime,vcActive)
	VALUES(@vcAdminName,@vcIp,GETDATE(),@vcActive)
END


EXEC Sp_Manage_OnlineKickTimeOutAdmin

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_CheckAdminPower]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_CheckAdminPower]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@reValue INT OUTPUT
)
/*
DECLARE @reValue INT
EXEC SP_Manage_CheckAdminPower
	@vcAdminName ='admins',
	@vcIp ='127.0.0.1',
	@reValue = @reValue OUTPUT

SELECT @reValue
*/
AS

EXEC SP_Manage_CheckIP
	@vcIP = @vcip,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

IF(@vcAdminName IS NULL OR @vcAdminName ='')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

DECLARE @t_Online CHAR(1)
SELECT @t_Online = cIsOnline FROM admin (NOLOCK) WHERE vcAdminName =@vcAdminName

IF(@t_Online<>'Y')
BEGIN
	SET @reValue=-1000000017 --您不并不在线，您是否尚未登陆？
END

SET @reValue=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Template_ManageTemplate]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Template_ManageTemplate]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@SkinId VARCHAR(36),
	@TemplateType INT,
	@iParentId  VARCHAR(36) ='',
	@iSystemType INT =0,
	@vcTempName VARCHAR(50),
	@vcContent TEXT,
	@vcUrl VARCHAR(255),
	@action CHAR(2) = '01',
	@Id VARCHAR(36) = '',
	@reValue INT OUTPUT
)
AS

DECLARE @LOG VARCHAR(200)
DECLARE @TID INT

IF(@vcAdminName='' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue= -1000000012 --操作员为空，您是否尚未登陆？
	RETURN
END

IF(@TemplateType=-1)
BEGIN
	SET @reValue= -1000000026 --请选择模版类别！ 
	RETURN
END

IF(@TemplateType=0)
BEGIN
	IF(@vcUrl='' OR @vcUrl IS NULL)
	BEGIN
		SET @reValue= -1000000024 --单页时地址不能为空 
		RETURN
	END
END

IF(@vcContent IS NULL)
BEGIN
	SET @reValue = -1000000027 --模板内容不能为空
	RETURN
END
--
--IF(@iSiteId <=0)
--BEGIN
--	SET @reValue = -1000000025 --站点ID不能为空
--	RETURN
--END

if(@action='01')
BEGIN
	INSERT INTO Template (Id,SkinId,TemplateType,iParentId,iSystemType,vcTempName,vcContent,vcUrl,dUpdateDate,dAddDate)
	VALUES(@Id,@SkinId,@TemplateType,@iParentId,@iSystemType,@vcTempName,@vcContent,@vcUrl,GETDATE(),GETDATE())
	SET @LOG = '添加新模版['+ @Id +']'
END

if(@action='02')
BEGIN
	UPDATE Template SET vcTempName=@vcTempName,vcContent=@vcContent,
	vcUrl=@vcUrl,dUpdateDate=GETDATE(),iParentId=@iParentId WHERE Id = @Id
	SET @LOG = '修改模版['+CAST(@Id AS VARCHAR)+']'
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

SET @reValue=1
GO
/****** Object:  StoredProcedure [dbo].[SP_News_NewsInfoStatManage]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_NewsInfoStatManage]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@cAction CHAR(2) = '01',
	@ids VARCHAR(1000) ='',
	@vcKey VARCHAR(30) = '',
	@vcKeValue CHAR(1) ='',
	@reValue INT OUTPUT
)
AS

IF(@vcAdminName IS NULL OR @vcAdminName ='')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@ids='')
BEGIN
	SET @reValue = -1000000051 --需要改变的文章不能为空
	RETURN
END

DECLARE @LOG NVARCHAR(200)
IF(@cAction='01')
BEGIN
	IF(@vcKeValue ='' OR @vcKeValue IS NULL OR (@vcKeValue <> 'N' AND @vcKeValue <> 'Y'))
	BEGIN
		SET @reValue = -1000000049 --需要修改的状态的值不能为空
		RETURN
	END
	
	DECLARE @SQL NVARCHAR(1000)
	IF(@vcKey ='' OR @vcKey IS NULL)
	BEGIN
		SET @reValue = -1000000048 --需要修改的状态名不能为空
		RETURN
	END
	ELSE IF(@vcKey = 'Checked')
	BEGIN
		IF(CHARINDEX(',',@ids)>0)
		BEGIN
			SET @SQL = 'UPDATE T_News_NewsInfo SET cChecked = '''+@vcKeValue+''''+
			+' WHERE iID IN ('+@ids+')'
			Execute Sp_Executesql @SQL
		END
		ELSE
		BEGIN
			UPDATE T_News_NewsInfo SET cChecked = @vcKeValue WHERE iID = @ids
		END
		SET @LOG = '更改资源['+@ids+']审核状态为['+@vcKeValue+']'
	END
	ELSE IF(@vcKey = 'Created')
	BEGIN
		IF(CHARINDEX(',',@ids)>0)
		BEGIN
			SET @SQL = 'UPDATE T_News_NewsInfo SET cCreated = '''+@vcKeValue+''''+
			+' WHERE iID IN ('+@ids+')'
			Execute Sp_Executesql @SQL
		END
		ELSE
		BEGIN
			UPDATE T_News_NewsInfo SET cCreated = @vcKeValue WHERE iID = @ids
		END
		SET @LOG = '更改资源['+@ids+']生成状态为['+@vcKeValue+']'
	END
	ELSE IF(@vcKey = 'Del')
	BEGIN
		IF(CHARINDEX(',',@ids)>0)
		BEGIN
			SET @SQL = 'UPDATE T_News_NewsInfo SET cDel = '''+@vcKeValue+''''+
			+' WHERE iID IN ('+@ids+')'
			Execute Sp_Executesql @SQL
		END
		ELSE
		BEGIN
			UPDATE T_News_NewsInfo SET cDel = @vcKeValue WHERE iID = @ids
		END
		IF(@vcKeValue='Y')
		BEGIN
			IF(CHARINDEX(',',@ids)>0)
			BEGIN
				SET @SQL = 'UPDATE T_News_NewsInfo SET cCreated = ''N'''+
				+' WHERE iID IN ('+@ids+')'
				Execute Sp_Executesql @SQL
			END
			ELSE
			BEGIN
				UPDATE T_News_NewsInfo SET cCreated = 'N' WHERE iID = @ids
			END
		END
		
		SET @LOG = '更改资源['+@ids+']存在状态为['+@vcKeValue+']'
	END
	ELSE
	BEGIN
		SET @reValue = -1000000050 --并无该状态
		RETURN
	END
END

IF(@cAction='02')
BEGIN
	SET @SQL = 'DELETE T_News_NewsInfo WHERE iId in (' + @ids+')'
	Execute Sp_Executesql @SQL

	SET @LOG = '彻底删除资源['+@ids+']'
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
GO
/****** Object:  StoredProcedure [dbo].[SP_News_SpecialityAdmin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_SpecialityAdmin]
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
GO
/****** Object:  StoredProcedure [dbo].[SP_News_SaveOrDelResource]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_News_SaveOrDelResource]
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
/****** Object:  StoredProcedure [dbo].[SP_Template_DelTemplate]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Template_DelTemplate]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vctemps VARCHAR(1000),
	@reValue INT OUTPUT 
)
AS

IF(@vctemps ='' OR @vctemps IS NULL)
BEGIN
	SET @reValue = -1000000028 --尚未选择需要删除的资讯模版
	RETURN;
END	

IF(@vcAdminName='' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue= -1000000012 --操作员为空，您是否尚未登陆？
	RETURN
END

DECLARE @sql NVARCHAR(2000)


SET @sql = 'DELETE Template WHERE Id IN ('+@vctemps+')'
Execute Sp_Executesql @sql


DECLARE @LOG VARCHAR(2000)
SET @LOG='删除模版['+@vctemps+']'
EXEC Sp_Manage_AddAdminLog
	@vcAdminName = @vcAdminName,
	@vcActive = @LOG,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN
END

SET @reValue =1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_GetAdminInfoByName]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_GetAdminInfoByName]
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
	SET @sql = 'SELECT iId,vcPopName,vcUrl,iParentId,cValid,iOrder FROM Popedom (NOLOCK) WHERE iID IN ( ' + @t_vcPopedoms + ') ORDER BY iOrder'
	Execute Sp_Executesql @sql

END

SELECT A.vcAdminName,A.vcNickName,A.vcPassword,A.vcClassPopedom ,A.cLock,A.vcPopedom,A.dAddDate,
A.dUpdateDate,A.dLoginDate,A.dLastLoginDate,A.iLoginCount,A.vcLastLoginIp,A.cIsOnline,
B.vcRoleName,B.vcContent,B.vcPopedom AS vcPopedomW,B.vcClassPopedom AS vcClassPopedomW
FROM  admin A (NOLOCK),  AdminRole  B (NOLOCK) 
WHERE A.vcAdminName = @vcAdminName AND A.iRole = B.iID


SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_AdminRoleDel]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_AdminRoleDel]
(
	@vcAdminName VARCHAR(50),
	@vcIP VARCHAR(15),
	@iRole INT,
	@reValue INT OUTPUT
)
AS

EXEC SP_Manage_CheckAdminPower
	@vcAdminName =@vcAdminName,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

IF(@iRole IS NULL OR @iRole ='')
BEGIN
	SET @reValue = -1000000014 -- 角色组编号不能为空
	RETURN;
END

DECLARE @t_Count INT
SELECT @t_Count = COUNT(1) FROM admin (NOLOCK) WHERE iRole = @iRole
IF(@t_Count>0)
BEGIN
	SET @reValue = -1000000015 -- 要删除此联系组，请先移出或删除此组中的管理员
	RETURN;
END


DELETE AdminRole WHERE iID = @iRole

DECLARE @log VARCHAR(1000)
SET @log = '删除角色组ID['+CAST(@iRole AS VARCHAR(1000))+']'
EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcip,
	@vcActive = @log,
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_AdminRoleInfoMdy]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_AdminRoleInfoMdy]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vcRoleName VARCHAR(50),
	@vcContent VARCHAR(255),
	@vcPopedom VARCHAR(1000),
	@vcClassPopedom VARCHAR(255),
	@cAction CHAR(2) ='01',
	@iRole INT = 0,
	@reValue INT OUTPUT
)
AS

IF(@vcRoleName IS NULL OR @vcRoleName='')
BEGIN
	SET @reValue = -1000000013  --角色组名称不能为空
	RETURN;
END

EXEC SP_Manage_CheckAdminPower
	@vcAdminName =@vcAdminName,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

DECLARE @log VARCHAR(1000)

IF(@cAction='01')
BEGIN
	SET @log = '添加角色组[' + @vcRoleName + ']'
	INSERT INTO AdminRole (vcRoleName,vcContent,vcPopedom,vcClassPopedom)
	VALUES(@vcRoleName,@vcContent,@vcPopedom,@vcClassPopedom)
END

IF(@cAction='02' AND @iRole>0)
BEGIN
	SET @log = '修改角色组[' + CAST(@iRole AS VARCHAR(1000)) + ':' + @vcRoleName + ']'
	UPDATE AdminRole
	SET vcRoleName = @vcRoleName,vcContent=@vcContent,vcPopedom=@vcPopedom,
	vcClassPopedom=@vcClassPopedom WHERE iID = @iRole
END



EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcip,
	@vcActive = @log,
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_AddAdmin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_AddAdmin]
(
	@vcAdminName varchar(50),
	@vcNickname varchar(50),
	@vcPassWord varchar(32),
	@iRole int,
	@clock CHAR(1),
	@vcPopedom VARCHAR(1000),
	@vcClassPopedom VARCHAR(255),
	@aAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@action CHAR(2) = '01', 
	@reValue int output
)
AS

EXEC SP_Manage_CheckAdminPower
	@vcAdminName =@vcAdminName,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

DECLARE @log VARCHAR(1000)
IF(@action='01')
BEGIN
	DECLARE @t_vcAdminName varchar(50)
	SELECT @t_vcAdminName = vcAdminName FROM admin (NOLOCK) WHERE vcAdminName = @vcAdminName
	IF(@t_vcAdminName<>'' AND @t_vcAdminName IS NOT NULL)
	BEGIN
		SET @reValue = -1000000005 --管理员帐号已经存在
		RETURN;
	END
END

SET XACT_ABORT ON
BEGIN TRY
BEGIN TRAN
	IF(@action='01')
	BEGIN
		SET @log = '添加管理员[' + @vcAdminName + ']'
		INSERT INTO admin
		(vcAdminName,vcNickName,vcPassWord,iRole,clock,vcPopedom,vcClassPopedom)
		VALUES(@vcAdminName,@vcNickname,@vcPassWord,@iRole,@clock,@vcPopedom,@vcClassPopedom)
	END
	
	IF(@action='02')
	BEGIN
		SET @log = '修改管理员[' + @vcAdminName + ']'
		
		IF(@vcPassWord<>'' AND @vcPassWord IS NOT NULL)
		BEGIN
			UPDATE admin SET vcNickName=@vcNickName,vcPassWord=@vcPassWord,iRole=@iRole,
			clock=@clock,vcPopedom=@vcPopedom,vcClassPopedom=@vcClassPopedom
			WHERE vcAdminName=@vcAdminName
		END
		ELSE
		BEGIN
			UPDATE admin SET vcNickName=@vcNickName,iRole=@iRole,
			clock=@clock,vcPopedom=@vcPopedom,vcClassPopedom=@vcClassPopedom
			WHERE vcAdminName=@vcAdminName
		END
	END
COMMIT
END TRY

BEGIN CATCH  
	ROLLBACK     
	SET @reValue = -1000000006		--数据库出错
	RETURN;
END CATCH
SET XACT_ABORT OFF 

EXEC SP_Manage_OnlineMdy
	@vcAdminName = @aAdminName,
	@vcIp = @vcip,
	@vcActive = @log,
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_ChanageAdminLoginInfo]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_ChanageAdminLoginInfo]
(
	@vcAdminName varchar(50),
	@oldPwd VARCHAR(32),
	@NewPwd VARCHAR(32),
	@vcNickName VARCHAR(50),
	@vcIP VARCHAR(15),
	@reValue INT OUTPUT
)
AS

EXEC SP_Manage_CheckAdminPower
	@vcAdminName =@vcAdminName,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

DECLARE @t_pwd VARCHAR(32)
DECLARE @t_lock CHAR(1)

SELECT @t_pwd = vcPassword,@t_lock = cLock FROM admin (NOLOCK) WHERE  vcAdminName = @vcAdminName

IF(@t_pwd<>@oldPwd)
BEGIN
	SET @reValue = -1000000009 --输入原始密码不正确
	RETURN;
END

IF(@t_lock='Y')
BEGIN
	SET @reValue = -1000000010 --您的帐号已经锁定，不能修改登陆信息
	RETURN;
END

IF(@NewPwd='' OR @NewPwd IS NULL)
BEGIN
	UPDATE admin SET vcNickName = @vcNickName
	WHERE vcAdminName = @vcAdminName
END
ELSE
BEGIN
	UPDATE admin SET vcNickName = @vcNickName,vcPassword = @NewPwd
	WHERE vcAdminName = @vcAdminName
END

EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcIp,
	@vcActive = '修改登陆信息',
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_AdminChangeGroup]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_AdminChangeGroup]
(
	@vcAdminName VARCHAR(50),
	@iRoleId INT,
	@vcAdmins VARCHAR(1000),
	@vcIp VARCHAR(15),
	@reValue INT OUTPUT
)
/*
DECLARE @reValue INT
EXEC SP_Manage_AdminChangeGroup
	@vcAdminName = 'admin',
	@iRoleId =1,
	@vcAdmins ='''leo85729'',''admin''',
	@vcIp ='127.0.0.1',
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


IF(@iRoleId=0 OR @iRoleId IS NULL)
BEGIN
	SET @reValue=-1000000011 --组编号不正确
	RETURN;
END

IF(CHARINDEX(',',@vcAdmins)>0)
BEGIN
	DECLARE @sql NVARCHAR(2000)
	SET @sql = 'UPDATE admin SET iRole = '+CAST(@iRoleId AS VARCHAR(1000))
	+' WHERE vcAdminName IN ('+@vcAdmins+')'
	Execute Sp_Executesql @sql
END
ELSE
BEGIN
	SET @vcAdmins = REPLACE(@vcAdmins,'''','');
	UPDATE admin SET iRole = @iRoleId WHERE vcAdminName=@vcAdmins
END


DECLARE @log VARCHAR(1000)
SET @log = '移动管理员[' + @vcAdmins + ']到角色组ID【'+CAST(@iRoleId AS VARCHAR(1000))+'】'
EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcip,
	@vcActive = @log,
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue=1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_DelAdmins]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_Manage_DelAdmins]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vcAdmins VARCHAR(1000),
	@action CHAR(2) ='01',
	@reValue INT OUTPUT 
)
AS

EXEC SP_Manage_CheckAdminPower
	@vcAdminName =@vcAdminName,
	@vcIp =@vcIp,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

IF(@vcAdmins ='' OR @vcAdmins IS NULL)
BEGIN
	SET @reValue = -1000000016 --尚未选择需要删除的管理员
	RETURN;
END

DECLARE @sql NVARCHAR(2000)
DECLARE @log VARCHAR(1000)
IF(@action='01')
BEGIN
	IF(CHARINDEX(',',@vcAdmins)>0)
	BEGIN
		SET @sql = 'UPDATE admin SET cIsDel = ''Y'''+
		+' WHERE vcAdminName IN ('+@vcAdmins+')'
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		SET @vcAdmins = REPLACE(@vcAdmins,'''','');
		UPDATE admin SET cIsDel = 'Y' WHERE vcAdminName=@vcAdmins
	END
	
	SET @log = '删除管理员[' + @vcAdmins + ']到回收站'
END

IF(@action='02')
BEGIN
	IF(CHARINDEX(',',@vcAdmins)>0)
	BEGIN
		SET @sql = 'DELETE admin WHERE vcAdminName IN ('+@vcAdmins+')'
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		SET @vcAdmins = REPLACE(@vcAdmins,'''','');
		DELETE admin WHERE vcAdminName=@vcAdmins
	END
	SET @log = '彻底删除管理员[' + @vcAdmins + ']'
END

IF(@action='03')
BEGIN
	IF(CHARINDEX(',',@vcAdmins)>0)
	BEGIN
		SET @sql = 'UPDATE admin SET cIsDel = ''N'''+
		+' WHERE vcAdminName IN ('+@vcAdmins+')'
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		SET @vcAdmins = REPLACE(@vcAdmins,'''','');
		UPDATE admin SET cIsDel = 'N' WHERE vcAdminName=@vcAdmins
	END
	
	SET @log = '救回管理员[' + @vcAdmins + ']'
END

EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcip,
	@vcActive = @log,
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue =1
GO
/****** Object:  StoredProcedure [dbo].[SP_Manage_AdminLogin]    Script Date: 05/13/2011 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Manage_AdminLogin]
(
	@vcAdminName varchar (50),
	@vcPassword varchar(32),
	@vcip VARCHAR(15),
	@reValue int output 
)

/*
DECLARE @reValue int
EXEC SP_Manage_AdminLogin
	@vcAdminName = 'admin',
	@vcPassword = 'admin',
	@vcip ='127.0.0.1',
	@reValue = @reValue output 

SELECT @reValue
*/
AS

EXEC SP_Manage_CheckIP
	@vcIP = @vcip,
	@reValue = @reValue OUTPUT

IF(@reValue<0)
BEGIN
	RETURN;
END

DECLARE @T_name varchar(50)
DECLARE @T_pwd varchar(32)
DECLARE @clock char(1)
DECLARE @cIsOnline CHAR(1)
DECLARE @t_lastIp VARCHAR(15)
DECLARE @t_isDel CHAR(1)

SET @cIsOnline = 'N'
SET @t_isDel = 'N'

EXEC Sp_Manage_OnlineKickTimeOutAdmin

SELECT @T_name = vcAdminName,@T_pwd=vcPassword,@clock = clock,@cIsOnline=cIsOnline ,
@t_lastIp = vcLastLoginIp,@t_isDel=cIsDel FROM admin (NOLOCK) WHERE vcAdminName = @vcAdminName

IF(@T_name IS NULL)
BEGIN
	SET  @reValue = -1000000002 --用户不存在
	RETURN
END

IF(@cIsOnline='Y' AND @t_lastIp <> @vcip)
BEGIN
	SET @reValue = -1000000005 --管理员已经在线
	RETURN
END

IF(@T_pwd<>@vcPassword)
BEGIN
	SET @reValue = -1000000003 --用户密码错误
	RETURN
END

IF(@clock='Y')
BEGIN
	SET @reValue = -1000000004 --用户已经被锁定
	RETURN
END

IF(@t_isDel='Y')
BEGIN
	SET @reValue = -1000000018 --您的帐号已经被删除，请联系管理员！
	RETURN;
END

EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcip,
	@vcActive = '登陆后台',
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

UPDATE admin SET vcLastLoginIp = @vcip,iLoginCount=iLoginCount+1,
dLastLoginDate = GETDATE() WHERE vcAdminName = @vcAdminName

SET @reValue = 1
GO
/****** Object:  Default [DF_T_Manage_AdminLog_dAddDate]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[AdminLog] ADD  CONSTRAINT [DF_T_Manage_AdminLog_dAddDate]  DEFAULT (getdate()) FOR [dAddDate]
GO
/****** Object:  Default [DF_T_Manage_AdminLog_vcIp]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[AdminLog] ADD  CONSTRAINT [DF_T_Manage_AdminLog_vcIp]  DEFAULT ('127.0.0.1') FOR [vcIp]
GO
/****** Object:  Default [DF_T_Manage_popedom_iParentId]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[Popedom] ADD  CONSTRAINT [DF_T_Manage_popedom_iParentId]  DEFAULT ((0)) FOR [iParentId]
GO
/****** Object:  Default [DF_popedom_addtime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[Popedom] ADD  CONSTRAINT [DF_popedom_addtime]  DEFAULT (getdate()) FOR [dAddtime]
GO
/****** Object:  Default [DF_T_Manage_Popedom_cValid]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[Popedom] ADD  CONSTRAINT [DF_T_Manage_Popedom_cValid]  DEFAULT (N'Y') FOR [cValid]
GO
/****** Object:  Default [DF_Popedom_iOrder]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[Popedom] ADD  CONSTRAINT [DF_Popedom_iOrder]  DEFAULT ((0)) FOR [iOrder]
GO
/****** Object:  Default [DF_SheifSource_IsRss]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[SheifSource] ADD  CONSTRAINT [DF_SheifSource_IsRss]  DEFAULT ((0)) FOR [IsRss]
GO
/****** Object:  Default [DF_T_Files_Class_iParentId]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[filecategories] ADD  CONSTRAINT [DF_T_Files_Class_iParentId]  DEFAULT ((0)) FOR [iParentId]
GO
/****** Object:  Default [DF_T_Files_Class_dCreateDate]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[filecategories] ADD  CONSTRAINT [DF_T_Files_Class_dCreateDate]  DEFAULT (getdate()) FOR [dCreateDate]
GO
/****** Object:  Default [DF_filecategories_MaxSpace]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[filecategories] ADD  CONSTRAINT [DF_filecategories_MaxSpace]  DEFAULT ((0)) FOR [MaxSpace]
GO
/****** Object:  Default [DF_filecategories_Space]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[filecategories] ADD  CONSTRAINT [DF_filecategories_Space]  DEFAULT ((0)) FOR [Space]
GO
/****** Object:  Default [DF_T_News_NewsInfo_iFrom]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_T_News_NewsInfo_iFrom]  DEFAULT ((0)) FOR [iFrom]
GO
/****** Object:  Default [DF_News_Created]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_News_Created]  DEFAULT ('N') FOR [cCreated]
GO
/****** Object:  Default [DF_News_IsChecked]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_News_IsChecked]  DEFAULT ('N') FOR [cChecked]
GO
/****** Object:  Default [DF_News_Del]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_News_Del]  DEFAULT ('N') FOR [cDel]
GO
/****** Object:  Default [DF_T_News_NewsInfo_cPostByUser]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_T_News_NewsInfo_cPostByUser]  DEFAULT ('N') FOR [cPostByUser]
GO
/****** Object:  Default [DF_News_AddTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_News_AddTime]  DEFAULT (getdate()) FOR [dAddDate]
GO
/****** Object:  Default [DF_News_UpTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_News_UpTime]  DEFAULT (getdate()) FOR [dUpDateDate]
GO
/****** Object:  Default [DF_T_News_NewsInfo_vcTitleColor]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_T_News_NewsInfo_vcTitleColor]  DEFAULT (N'') FOR [vcTitleColor]
GO
/****** Object:  Default [DF_T_News_NewsInfo_cStrong]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[resources] ADD  CONSTRAINT [DF_T_News_NewsInfo_cStrong]  DEFAULT (N'N') FOR [cStrong]
GO
/****** Object:  Default [DF_T_Files_FileInfos_iSize]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[fileresources] ADD  CONSTRAINT [DF_T_Files_FileInfos_iSize]  DEFAULT ((0)) FOR [iSize]
GO
/****** Object:  Default [DF_T_Files_FileInfos_iDowns]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[fileresources] ADD  CONSTRAINT [DF_T_Files_FileInfos_iDowns]  DEFAULT ((0)) FOR [iDowns]
GO
/****** Object:  Default [DF_T_Files_FileInfos_iRequest]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[fileresources] ADD  CONSTRAINT [DF_T_Files_FileInfos_iRequest]  DEFAULT ((0)) FOR [iRequest]
GO
/****** Object:  Default [DF_T_Files_FileInfos_dCreateDate]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[fileresources] ADD  CONSTRAINT [DF_T_Files_FileInfos_dCreateDate]  DEFAULT (getdate()) FOR [dCreateDate]
GO
/****** Object:  Default [DF_T_Manage_RefuseIp_cValid]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[AdminRefuseIp] ADD  CONSTRAINT [DF_T_Manage_RefuseIp_cValid]  DEFAULT (N'Y') FOR [cValid]
GO
/****** Object:  Default [DF_ClassList_UpTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[categories] ADD  CONSTRAINT [DF_ClassList_UpTime]  DEFAULT (getdate()) FOR [dUpdateDate]
GO
/****** Object:  Default [DF_T_News_ClassInfo_iListTemplate]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[categories] ADD  CONSTRAINT [DF_T_News_ClassInfo_iListTemplate]  DEFAULT ((0)) FOR [iListTemplate]
GO
/****** Object:  Default [DF_T_News_ClassInfo_iOrder]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[categories] ADD  CONSTRAINT [DF_T_News_ClassInfo_iOrder]  DEFAULT ((0)) FOR [iOrder]
GO
/****** Object:  Default [DF__T_News_Cl__Visib__5DCAEF64]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[categories] ADD  CONSTRAINT [DF__T_News_Cl__Visib__5DCAEF64]  DEFAULT (N'Y') FOR [Visible]
GO
/****** Object:  Default [DF__categorie__DataB__14270015]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[categories] ADD  CONSTRAINT [DF__categorie__DataB__14270015]  DEFAULT (N'resourceDataBase') FOR [DataBaseService]
GO
/****** Object:  Default [DF_Admin_AddTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_Admin_AddTime]  DEFAULT (getdate()) FOR [dAddDate]
GO
/****** Object:  Default [DF_Admin_UpTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_Admin_UpTime]  DEFAULT (getdate()) FOR [dUpdateDate]
GO
/****** Object:  Default [DF_Admin_LoginTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_Admin_LoginTime]  DEFAULT (getdate()) FOR [dLoginDate]
GO
/****** Object:  Default [DF_Admin_LastLoginTime]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_Admin_LastLoginTime]  DEFAULT (getdate()) FOR [dLastLoginDate]
GO
/****** Object:  Default [DF_Admin_LoginCount]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_Admin_LoginCount]  DEFAULT ((0)) FOR [iLoginCount]
GO
/****** Object:  Default [DF_T_Manage_AdminInfo_cIsOnline]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_T_Manage_AdminInfo_cIsOnline]  DEFAULT (N'N') FOR [cIsOnline]
GO
/****** Object:  Default [DF_T_Manage_AdminInfo_cIsDel]    Script Date: 05/13/2011 20:58:18 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_T_Manage_AdminInfo_cIsDel]  DEFAULT ('N') FOR [cIsDel]
GO
/****** Object:  Default [DF_User_CreateTime]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_LastLoginTime]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_LastLoginTime]  DEFAULT (getdate()) FOR [LastLoginTime]
GO
/****** Object:  Default [DF_User_State]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_State]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF_User_UserLevel]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserLevel]  DEFAULT ((0)) FOR [UserLevel]
GO
/****** Object:  Default [DF_UserPay_FreeMoney]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[UserPay] ADD  CONSTRAINT [DF_UserPay_FreeMoney]  DEFAULT ((0)) FOR [FreeMoney]
GO
/****** Object:  Default [DF_UserPay_FrezzMoney]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[UserPay] ADD  CONSTRAINT [DF_UserPay_FrezzMoney]  DEFAULT ((0)) FOR [FrezzMoney]
GO
/****** Object:  Default [DF_UserPay_SumMoney]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[UserPay] ADD  CONSTRAINT [DF_UserPay_SumMoney]  DEFAULT ((0)) FOR [SumMoney]
GO
/****** Object:  Default [DF_UserPay_Points]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[UserPay] ADD  CONSTRAINT [DF_UserPay_Points]  DEFAULT ((0)) FOR [Points]
GO
/****** Object:  Default [DF_VisitorComment_AddTime]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[VisitorComment] ADD  CONSTRAINT [DF_VisitorComment_AddTime]  DEFAULT (getdate()) FOR [dAddDate]
GO
/****** Object:  Default [DF_T_Template_NewsTemplateInfo_iSiteId]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Template] ADD  CONSTRAINT [DF_T_Template_NewsTemplateInfo_iSiteId]  DEFAULT ((0)) FOR [SkinId]
GO
/****** Object:  Default [DF_T_Template_TemplateInfo_iParentId]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Template] ADD  CONSTRAINT [DF_T_Template_TemplateInfo_iParentId]  DEFAULT ((0)) FOR [iParentId]
GO
/****** Object:  Default [DF_T_Template_TemplateInfo_iSystemType]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Template] ADD  CONSTRAINT [DF_T_Template_TemplateInfo_iSystemType]  DEFAULT ((0)) FOR [iSystemType]
GO
/****** Object:  Default [DF_News_Template_UpTime]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Template] ADD  CONSTRAINT [DF_News_Template_UpTime]  DEFAULT (getdate()) FOR [dUpdateDate]
GO
/****** Object:  Default [DF_News_Template_AddTime]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Template] ADD  CONSTRAINT [DF_News_Template_AddTime]  DEFAULT (getdate()) FOR [dAddDate]
GO
/****** Object:  Default [DF_T_News_Speciality_iSiteId]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Speciality] ADD  CONSTRAINT [DF_T_News_Speciality_iSiteId]  DEFAULT ((0)) FOR [iSiteId]
GO
/****** Object:  Default [DF_News_Speciality_UpTime]    Script Date: 05/13/2011 20:58:19 ******/
ALTER TABLE [dbo].[Speciality] ADD  CONSTRAINT [DF_News_Speciality_UpTime]  DEFAULT (getdate()) FOR [dUpDateDate]
GO
