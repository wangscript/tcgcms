SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Files_Class]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Files_Class](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[vcFileName] [nvarchar](100) NOT NULL,
	[iParentId] [int] NOT NULL CONSTRAINT [DF_T_Files_Class_iParentId]  DEFAULT ((0)),
	[vcMeno] [nvarchar](1000) NOT NULL,
	[dCreateDate] [datetime] NOT NULL CONSTRAINT [DF_T_Files_Class_dCreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_T_Files_Class] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Files_FileInfos]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Files_FileInfos](
	[iID] [bigint] NOT NULL,
	[iClassId] [int] NOT NULL,
	[vcFileName] [nvarchar](100) NULL,
	[iSize] [int] NOT NULL CONSTRAINT [DF_T_Files_FileInfos_iSize]  DEFAULT ((0)),
	[vcType] [varchar](10) NULL,
	[iDowns] [int] NOT NULL CONSTRAINT [DF_T_Files_FileInfos_iDowns]  DEFAULT ((0)),
	[iRequest] [int] NOT NULL CONSTRAINT [DF_T_Files_FileInfos_iRequest]  DEFAULT ((0)),
	[vcIP] [varchar](15) NOT NULL,
	[dCreateDate] [datetime] NOT NULL CONSTRAINT [DF_T_Files_FileInfos_dCreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_T_Files_FileInfos] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[T_Files_FileInfos]') AND name = N'IX_T_Files_FileInfos')
CREATE NONCLUSTERED INDEX [IX_T_Files_FileInfos] ON [dbo].[T_Files_FileInfos] 
(
	[iClassId] ASC
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Manage_Popedom]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Manage_Popedom](
	[iID] [int] NOT NULL,
	[vcPopName] [varchar](50) NULL,
	[vcUrl] [varchar](50) NULL,
	[iParentId] [int] NULL CONSTRAINT [DF_T_Manage_popedom_iParentId]  DEFAULT ((0)),
	[dAddtime] [datetime] NULL CONSTRAINT [DF_popedom_addtime]  DEFAULT (getdate()),
	[cValid] [char](1) NOT NULL CONSTRAINT [DF_T_Manage_Popedom_cValid]  DEFAULT (N'Y'),
 CONSTRAINT [PK_popedom] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Manage_RefuseIp]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Manage_RefuseIp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[vcStartIp] [bigint] NOT NULL,
	[vcEndtIp] [bigint] NOT NULL,
	[cValid] [char](1) NOT NULL CONSTRAINT [DF_T_Manage_RefuseIp_cValid]  DEFAULT (N'Y'),
 CONSTRAINT [PK_T_Manage_RefuseIp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Sp_Manage_IpToNum]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
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
	@ip = ''127.0.0.1'',   
	@ip_value = @i_ip_value OUT,  
	@retval = @i_retval OUT  
SELECT @i_retval, @i_ip_value
 */
) 

AS
  
DECLARE @i_power_id int
DECLARE @c_power_value varchar(500)  
  
SELECT @ip_value = CONVERT(bigint,LEFT(@ip, CHARINDEX( ''.'', @ip) - 1))  
 , @ip = RIGHT(@ip, LEN(@ip) - CHARINDEX( ''.'', @ip))  
SELECT @ip_value = @ip_value * 256 * 256 * 256  
  
SELECT @ip_value = @ip_value + CONVERT(bigint,LEFT(@ip, CHARINDEX( ''.'', @ip) - 1)) * 256 * 256  
 , @ip = RIGHT(@ip, LEN(@ip) - CHARINDEX( ''.'', @ip))  
SELECT @ip_value = @ip_value + CONVERT(bigint,LEFT(@ip, CHARINDEX( ''.'', @ip) - 1)) * 256   
 , @ip = RIGHT(@ip, LEN(@ip) - CHARINDEX( ''.'', @ip))  
SELECT @ip_value = @ip_value + CONVERT(int, @ip)   
  
SELECT @retval = 1    



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Manage_Online]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Manage_Online](
	[vcadminname] [varchar](50) NOT NULL,
	[vcIp] [varchar](15) NOT NULL,
	[dActiveTime] [datetime] NOT NULL,
	[vcActive] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_T_Manage_Online] PRIMARY KEY CLUSTERED 
(
	[vcadminname] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_GetAdminRoleInfo]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'

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

SELECT  @admincount = COUNT(1) FROM  T_Manage_AdminInfo (NOLOCK) WHERE cIsDel <> ''Y''

SELECT  @deladmincount = COUNT(1) FROM  T_Manage_AdminInfo (NOLOCK) WHERE cIsDel = ''Y''

SELECT iID,vcRoleName,(SELECT COUNT(1) FROM T_Manage_AdminInfo WHERE iRole = A.iID AND cIsDel <> ''Y'') AS num 
FROM dbo.T_Manage_AdminRole A (NOLOCK) ORDER BY num DESC

SET @reValue = 1


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_GetAdminList]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
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

SELECT @iRoleCount = COUNT(1) FROM T_Manage_AdminRole (NOLOCK)


IF(@iRoleId = 0)
BEGIN
	SET @vcRoleName = ''所有管理员''
	SELECT @iAdminCount = COUNT(1) FROM T_Manage_AdminInfo (NOLOCK) WHERE cIsDel <> ''Y''
	SELECT A.vcAdminName,A.vcNickName,A.cLock,A.dAddDate,A.dUpdateDate,B.vcRoleName,B.iID
	FROM T_Manage_AdminInfo A (NOLOCK),T_Manage_AdminRole B (NOLOCK) 
	WHERE A.iRole = B.iID AND A.cIsDel <> ''Y''
END
IF(@iRoleId > 0)
BEGIN
	SELECT @iAdminCount = COUNT(1) FROM T_Manage_AdminInfo (NOLOCK) WHERE iRole = @iRoleId AND cIsDel <> ''Y''
	SELECT @vcRoleName = vcRoleName FROM T_Manage_AdminRole (NOLOCK) WHERE iId = @iRoleId
	SELECT A.vcAdminName,A.vcNickName,A.cLock,A.dAddDate,A.dUpdateDate,B.vcRoleName,B.iID
	FROM T_Manage_AdminInfo A (NOLOCK),T_Manage_AdminRole B (NOLOCK) 
	WHERE A.iRole = B.iID AND B.iID = @iRoleId AND A.cIsDel <> ''Y''
END

IF(@iRoleId =-1)
BEGIN
	SET @vcRoleName = ''管理员回收站''
	SELECT @iAdminCount = COUNT(1) FROM T_Manage_AdminInfo (NOLOCK) WHERE cIsDel = ''Y''
	SELECT A.vcAdminName,A.vcNickName,A.cLock,A.dAddDate,A.dUpdateDate,B.vcRoleName,B.iID
	FROM T_Manage_AdminInfo A (NOLOCK),T_Manage_AdminRole B (NOLOCK) 
	WHERE A.iRole = B.iID AND A.cIsDel = ''Y''
END

SET @reValue = 1

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Manage_AdminRole]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Manage_AdminRole](
	[iID] [int] IDENTITY(1,1) NOT NULL,
	[vcRoleName] [varchar](50) NOT NULL,
	[vcContent] [varchar](255) NULL,
	[vcPopedom] [varchar](1000) NULL,
	[vcClassPopedom] [varchar](255) NULL,
	[dUpdateDate] [smalldatetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Manage_AdminInfo]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Manage_AdminInfo](
	[vcAdminName] [varchar](50) NOT NULL,
	[vcNickName] [varchar](50) NOT NULL,
	[vcPassword] [varchar](32) NOT NULL,
	[iRole] [int] NOT NULL,
	[vcPopedom] [varchar](1000) NULL,
	[vcClassPopedom] [varchar](255) NULL,
	[cLock] [char](1) NULL,
	[dAddDate] [datetime] NULL CONSTRAINT [DF_Admin_AddTime]  DEFAULT (getdate()),
	[dUpdateDate] [datetime] NULL CONSTRAINT [DF_Admin_UpTime]  DEFAULT (getdate()),
	[dLoginDate] [datetime] NULL CONSTRAINT [DF_Admin_LoginTime]  DEFAULT (getdate()),
	[dLastLoginDate] [datetime] NULL CONSTRAINT [DF_Admin_LastLoginTime]  DEFAULT (getdate()),
	[iLoginCount] [int] NULL CONSTRAINT [DF_Admin_LoginCount]  DEFAULT ((0)),
	[vcLastLoginIp] [varchar](15) NULL,
	[cIsOnline] [char](1) NULL CONSTRAINT [DF_T_Manage_AdminInfo_cIsOnline]  DEFAULT (N'N'),
	[cIsDel] [char](1) NOT NULL CONSTRAINT [DF_T_Manage_AdminInfo_cIsDel]  DEFAULT ('N'),
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[vcAdminName] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Template_TemplateInfo]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Template_TemplateInfo](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[iSiteId] [int] NOT NULL CONSTRAINT [DF_T_Template_NewsTemplateInfo_iSiteId]  DEFAULT ((0)),
	[iType] [int] NOT NULL,
	[iParentId] [int] NOT NULL CONSTRAINT [DF_T_Template_TemplateInfo_iParentId]  DEFAULT ((0)),
	[iSystemType] [int] NOT NULL CONSTRAINT [DF_T_Template_TemplateInfo_iSystemType]  DEFAULT ((0)),
	[vcTempName] [varchar](50) NOT NULL,
	[vcContent] [text] NULL,
	[vcUrl] [varchar](255) NULL,
	[dUpdateDate] [smalldatetime] NULL CONSTRAINT [DF_News_Template_UpTime]  DEFAULT (getdate()),
	[dAddDate] [smalldatetime] NULL CONSTRAINT [DF_News_Template_AddTime]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[AddZreo]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[AddZreo]
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
		SET @vcStr = ''0'' + @vcStr
		SET @T_Count = @T_Count -1
	END
	RETURN @vcStr
END
' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_News_NewsInfo]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_News_NewsInfo](
	[iID] [int] IDENTITY(1,1) NOT NULL,
	[iClassID] [int] NOT NULL,
	[vcTitle] [varchar](100) NOT NULL,
	[vcUrl] [varchar](200) NULL,
	[vcContent] [text] NULL,
	[vcAuthor] [varchar](50) NULL,
	[iFrom] [int] NOT NULL CONSTRAINT [DF_T_News_NewsInfo_iFrom]  DEFAULT ((0)),
	[iCount] [int] NULL,
	[vcKeyWord] [varchar](100) NULL,
	[vcEditor] [varchar](50) NOT NULL,
	[cCreated] [char](1) NOT NULL CONSTRAINT [DF_News_Created]  DEFAULT ('N'),
	[vcSmallImg] [varchar](255) NULL,
	[vcBigImg] [varchar](255) NULL,
	[vcShortContent] [varchar](500) NULL,
	[vcSpeciality] [varchar](100) NULL,
	[cChecked] [char](1) NOT NULL CONSTRAINT [DF_News_IsChecked]  DEFAULT ('N'),
	[cDel] [char](1) NOT NULL CONSTRAINT [DF_News_Del]  DEFAULT ('N'),
	[cPostByUser] [char](1) NULL CONSTRAINT [DF_T_News_NewsInfo_cPostByUser]  DEFAULT ('N'),
	[vcFilePath] [varchar](255) NULL,
	[dAddDate] [datetime] NULL CONSTRAINT [DF_News_AddTime]  DEFAULT (getdate()),
	[dUpDateDate] [datetime] NULL CONSTRAINT [DF_News_UpTime]  DEFAULT (getdate()),
	[vcTitleColor] [varchar](50) NULL CONSTRAINT [DF_T_News_NewsInfo_vcTitleColor]  DEFAULT (N''),
	[cStrong] [varchar](1) NULL CONSTRAINT [DF_T_News_NewsInfo_cStrong]  DEFAULT (N'N'),
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_News_NewsFromInfo]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_News_NewsFromInfo](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[vcTitle] [nvarchar](150) NULL,
	[vcUrl] [varchar](255) NULL,
	[dUpdateDate] [datetime] NULL CONSTRAINT [DF_T_News_NewsFromInfo_dUpdateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_T_News_NewsFromInfo] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [varchar](36) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PassWord] [varchar](32) NOT NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_User_CreateTime]  DEFAULT (getdate()),
	[LastLoginTime] [datetime] NOT NULL CONSTRAINT [DF_User_LastLoginTime]  DEFAULT (getdate()),
	[State] [int] NOT NULL CONSTRAINT [DF_User_State]  DEFAULT ((0)),
	[LastLoginIp] [varchar](32) NOT NULL,
	[UserLevel] [int] NOT NULL CONSTRAINT [DF_User_UserLevel]  DEFAULT ((0)),
	[UserClubLevel] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[IsSpeciality]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[IsSpeciality](@SpeList nvarchar(1000),@Spe nvarchar(20))  
RETURNS int AS  
BEGIN 
 RETURN(CHARINDEX('',''+@Spe+'','','',''+@SpeList+'',''))
END' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GetHtmlPathForWithOutBase]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'create FUNCTION [dbo].[GetHtmlPathForWithOutBase]
(
	@vcFilePath VARCHAR(255)
)
RETURNS VARCHAR(255)
AS
BEGIN
RETURN (SUBSTRING((@vcFilePath),2,LEN(@vcFilePath)-1))
END' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GetClassUrl]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetClassUrl]
(
	@vcUrl VARCHAR(255),
	@vcExp VARCHAR(10)
)
RETURNS VARCHAR(255)
AS
BEGIN

DECLARE @reValue VARCHAR(255)
IF(CHARINDEX(''.'',@vcUrl) = 0)
BEGIN
	SET @reValue = @vcUrl + @vcExp
END
ELSE
BEGIN
	SET @reValue = @vcUrl
END

RETURN  @reValue
END

' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_TCG_GetPage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_TCG_GetPage]
(
    @tblName	nvarchar(50),				----要显示的表或多个表的连接
	@fldName	nvarchar(500),				----要显示的字段列表
	@fldSort	nvarchar(200),				----排序字段列表或条件,必须输入
	@strCondition	nvarchar(1000),			----查询条件,不需where,必须输入
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
	SET @sql = ''SELECT @cnt=COUNT(1) FROM ''
				+ @tblName
				+ '' WITH (NOLOCK)''
				+ '' WHERE '' 
				+ @strCondition
	
	--print @sql
	EXEC sp_executesql @sql,N''@cnt int OUTPUT'',@cnt=@counts OUTPUT
	
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
	SET @sql = '' SELECT ''
				+ @fldName 
				+ '' FROM ( SELECT ''
				+ @fldName
				+ '', ROW_NUMBER() OVER(ORDER BY ''
				+ @fldSort
				+ '') AS ROW FROM ''
				+ @tblName
				+ '' WITH (NOLOCK)''
				+ '' WHERE ''
				+ @strCondition
				+ '') tempDB WHERE  ROW BETWEEN ''
				+ CAST(@pageSize*(@curpage-1) + 1 as varchar(12))
				+ '' AND ''
				+ CAST(@curpage*@pageSize as varchar(12))

	print @sql
	EXEC (@sql)

	SET @retval = 1

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_Manage_AdminLog]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_Manage_AdminLog](
	[iID] [int] IDENTITY(1,1) NOT NULL,
	[vcAdminName] [varchar](50) NOT NULL,
	[vcActive] [nvarchar](255) NOT NULL,
	[dAddDate] [datetime] NOT NULL CONSTRAINT [DF_T_Manage_AdminLog_dAddDate]  DEFAULT (getdate()),
	[vcIp] [varchar](15) NOT NULL CONSTRAINT [DF_T_Manage_AdminLog_vcIp]  DEFAULT ('127.0.0.1'),
 CONSTRAINT [PK_T_Manage_AdminLog] PRIMARY KEY CLUSTERED 
(
	[iID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_News_ClassInfo]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_News_ClassInfo](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[vcClassName] [varchar](200) NOT NULL,
	[vcName] [varchar](50) NULL,
	[iParent] [int] NULL,
	[dUpdateDate] [datetime] NULL CONSTRAINT [DF_ClassList_UpTime]  DEFAULT (getdate()),
	[iTemplate] [int] NULL,
	[iListTemplate] [int] NOT NULL CONSTRAINT [DF_T_News_ClassInfo_iListTemplate]  DEFAULT ((0)),
	[vcDirectory] [varchar](200) NULL,
	[vcUrl] [varchar](255) NULL,
	[iOrder] [int] NOT NULL CONSTRAINT [DF_T_News_ClassInfo_iOrder]  DEFAULT ((0)),
	[Visible] [char](1) NOT NULL DEFAULT (N'Y'),
 CONSTRAINT [PK_ClassList] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[UserContact]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
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
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GetFilePath]') AND xtype in (N'FN', N'IF', N'TF'))
BEGIN
execute dbo.sp_executesql @statement = N'

CREATE FUNCTION [dbo].[GetFilePath]
(
	@vcUrl VARCHAR(255),
	@vcFilePath VARCHAR(255)
)
RETURNS VARCHAR(255)
AS
BEGIN

DECLARE @reValue VARCHAR(255)
IF(@vcUrl IS NULL OR @vcUrl = '''')
BEGIN
	SET @reValue = @vcFilePath
END
ELSE
BEGIN
	SET @reValue = @vcUrl
END

RETURN  @reValue
END


' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[UserPay]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[UserPay](
	[UserId] [varchar](36) NOT NULL,
	[FreeMoney] [money] NOT NULL CONSTRAINT [DF_UserPay_FreeMoney]  DEFAULT ((0)),
	[FrezzMoney] [money] NOT NULL CONSTRAINT [DF_UserPay_FrezzMoney]  DEFAULT ((0)),
	[SumMoney] [money] NOT NULL CONSTRAINT [DF_UserPay_SumMoney]  DEFAULT ((0)),
	[Points] [money] NOT NULL CONSTRAINT [DF_UserPay_Points]  DEFAULT ((0)),
	[PayPassWord] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_UserPay] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_News_VisitorComment]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_News_VisitorComment](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[vcTitle] [varchar](50) NULL,
	[vcContent] [varchar](2000) NULL,
	[vcEmail] [varchar](50) NULL,
	[iResId] [int] NULL,
	[vcIp] [varchar](15) NULL,
	[dAddDate] [datetime] NULL CONSTRAINT [DF_VisitorComment_AddTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_T_News_VisitorComment] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM dbo.sysindexes WHERE id = OBJECT_ID(N'[dbo].[T_News_VisitorComment]') AND name = N'IX_T_News_VisitorComment')
CREATE NONCLUSTERED INDEX [IX_T_News_VisitorComment] ON [dbo].[T_News_VisitorComment] 
(
	[iResId] ASC
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[T_News_Speciality]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
CREATE TABLE [dbo].[T_News_Speciality](
	[iId] [int] IDENTITY(1,1) NOT NULL,
	[iSiteId] [int] NOT NULL CONSTRAINT [DF_T_News_Speciality_iSiteId]  DEFAULT ((0)),
	[iParent] [int] NOT NULL,
	[vcTitle] [varchar](50) NOT NULL,
	[vcExplain] [varchar](200) NULL,
	[dUpDateDate] [datetime] NULL CONSTRAINT [DF_News_Speciality_UpTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_T_News_Speciality] PRIMARY KEY CLUSTERED 
(
	[iId] ASC
) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_NewsInfoManage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROC [dbo].[SP_News_NewsInfoManage]
(
	@cAction CHAR(2) =''01'',
	@iId INT =0,
	@iClassID INT,
	@vcTitle VARCHAR(100),	--资讯标题
	@vcUrl VARCHAR(255),	--跳转地址
	@vcContent TEXT,		--资讯内容
	@vcAuthor VARCHAR(50)='''',	
	@iFrom INT,	--资讯来源
	@iCount INT = 0,
	@vcKeyWord VARCHAR(100),	--资讯关键字
	@vcEditor VARCHAR(50),	--资讯编辑者
	@cCreated CHAR(1)=''N'',
	@cPostByUser CHAR(1)=''N'',
	@vcSmallImg VARCHAR(255),
	@vcBigImg VARCHAR(255),
	@vcShortContent VARCHAR(500),
	@vcSpeciality VARCHAR(100),
	@cChecked CHAR(1)=''N'',
	@cDel CHAR(1) =''N'',
	@vcFilePath VARCHAR(255),--资讯路径
	@iIDOut INT OUTPUT,
	@vcExtension VARCHAR(6),
	@cShief CHAR(2)=''01'',
	@cStrong CHAR(1)=''N'',
	@vcTitleColor VARCHAR(10)='''',
	@reValue INT OUTPUT
)
/*

DECLARE @reValue INT
EXEC SP_News_NewsInfoManage
	@iClassID=0,
	@vcTitle ='''',	--资讯标题
	@vcUrl ='''',	--跳转地址
	@vcContent ='''',		--资讯内容
	@vcAuthor ='''',	
	@iFrom =0,	--资讯来源
	@vcKeyWord ='''',	--资讯关键字
	@vcEditor ='''',	--资讯编辑者
	@vcSmallImg ='''',
	@vcBigImg ='''',
	@vcShortContent ='''',
	@vcSpeciality ='''',
	@cChecked =''N'',
	@vcFilePath ='''',--资讯路径
	@reValue = @reValue OUTPUT

SELECT @reValue
*/
AS

IF(@vcTitle='''' OR @vcTitle IS NULL)
BEGIN
	SET @reValue = -1000000039 --资讯标题不能为空
	RETURN
END

IF(@vcAuthor='''')
BEGIN
	SET @vcAuthor=@vcEditor
END

IF(@vcEditor='''' OR @vcEditor IS NULL)
BEGIN
	SET @reValue = -1000000041 --资讯编辑者不能为空
	RETURN
END

IF(@iFrom =0 OR @iFrom IS NULL)
BEGIN
	SET @reValue = -1000000042 --资讯来源不能为空
	RETURN
END

IF(@iClassID =0 OR @iClassID IS NULL)
BEGIN
	SET @reValue = -1000000056 --资讯分类不能为空
	RETURN
END

IF(@vcKeyWord ='''' OR @vcKeyWord IS NULL)
BEGIN
	SET @reValue = -1000000043 --资讯关键字不能为空
	RETURN
END

IF(@cShief=''02'')
BEGIN
	DECLARE @C INT
	SET @C=0
	SELECT @C=COUNT(1) FROM T_News_NewsInfo (NOLOCK) WHERE vcTitle = @vcTitle AND iClassID=@iClassID
	IF(@C>0)
	BEGIN
		SET @reValue = -1000000065 --抓取文章标题不能重复
		RETURN;
	END
END

DECLARE @ClassPath VARCHAR(200)
SET @ClassPath=''''
SELECT @ClassPath = vcDirectory FROM T_News_ClassInfo WHERE iId=@iClassID
IF(@ClassPath='''' OR @ClassPath IS NULL)
BEGIN
	SET @reValue = -1000000045 --资讯分类不存在
	RETURN
END

SET XACT_ABORT ON
BEGIN TRY
BEGIN TRAN

	IF(@cAction=''01'')
	BEGIN
		DECLARE @DATE DATETIME
		SET @DATE = GETDATE()
		
		INSERT INTO T_News_NewsInfo (iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iFrom,iCount,vcKeyWord,
		vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,cDel,cPostByUser,
		vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong) VALUES(@iClassID,@vcTitle,@vcUrl,@vcContent,@vcAuthor,@iFrom,
		@iCount,@vcKeyWord,@vcEditor,@cCreated,@vcSmallImg,@vcBigImg,@vcShortContent,@vcSpeciality,
		@cChecked,@cDel,@cPostByUser,@vcFilePath,@DATE,@DATE,@vcTitleColor,@cStrong)

		SET @iIDOut = @@IDENTITY
		
		SET @vcFilePath = REPLACE(@vcFilePath,''{0}'',dbo.AddZreo(CAST(@iIDOut AS VARCHAR(10)),9))
		UPDATE T_News_NewsInfo
		SET vcFilePath = @vcFilePath WHERE IID = @iIDOut
	END
	
	IF(@cAction=''02'')
	BEGIN
		IF(@iId=0)
		BEGIN
			SET @reValue = -1000000046 --修改文章的ID不能为0
			RETURN
		END
		
		DECLARE @T_COUNT INT
		SET @T_COUNT = 0
		SELECT @T_COUNT =COUNT(1) FROM T_News_NewsInfo (NOLOCK) WHERE iId=@iId
		IF(@T_COUNT<>1)
		BEGIN
			SET @reValue = -1000000047 --修改文章不存在
			RETURN
		END
	

		UPDATE T_News_NewsInfo SET iClassID=@iClassID,vcTitle=@vcTitle,vcUrl=@vcUrl,vcContent=@vcContent,
		vcAuthor=@vcAuthor,iFrom=@iFrom,iCount=@iCount,vcKeyWord=@vcKeyWord,vcEditor=@vcEditor,
		cCreated=@cCreated,vcSmallImg=@vcSmallImg,vcBigImg=@vcBigImg,vcShortContent=@vcShortContent,
		vcSpeciality=@vcSpeciality,cChecked=@cChecked,cDel=@cDel,cPostByUser=@cPostByUser,vcFilePath=@vcFilePath,
		dUpDateDate=GETDATE(),vcTitleColor = @vcTitleColor,cStrong = @cStrong WHERE iId =@iId
		
		
		
		SET @iIDOut = @iId
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












' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_GetNewsInfosForCreatHTML]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SP_News_GetNewsInfosForCreatHTML]
(
	@cAction CHAR(2) =''01'',
	@ITimeType INT=0,
	@dStartTime DATETIME = ''1981-03-06'',
	@dEndTime DATETIME = ''1982-09-27'',
	@vcClass VARCHAR(2000) ='''',
	@iNum INT=0,
	@iDel INT=0
)
AS

IF(@cAction=''01'')
BEGIN
	DECLARE @SQL NVARCHAR(2000)
	IF(@iDel=1)
	BEGIN
		IF(@iNum=0)
		BEGIN
			SET @SQL = ''SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND iClassID IN (''+@vcClass+'')''
		END
		ELSE
		BEGIN
			SET @SQL = ''SELECT TOP ''+CAST(@iNum AS VARCHAR)+'' iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND iClassID IN (''+@vcClass+'')''
		END
	END	
	IF(@iDel=2)
	BEGIN
		IF(@iNum=0)
		BEGIN
			SET @SQL = ''SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND iClassID IN (''+@vcClass+'') AND cCreated = ''''N''''''
		END
		ELSE
		BEGIN
			SET @SQL = ''SELECT TOP ''+CAST(@iNum AS VARCHAR)+'' iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND iClassID IN (''+@vcClass+'') AND cCreated = ''''N''''''
		END
	END

	Execute Sp_Executesql @sql
END

IF(@cAction=''02'')
BEGIN
       DECLARE @SQL02 NVARCHAR(2000)
	IF(@iDel=1)
	BEGIN
		IF(@ITimeType=1)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SET SQL02 ='' SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND dAddDate BETWEEN	
				@dStartTime AND @dEndTime''
			END
			ELSE
			BEGIN
		        	SET @SQL02 = ''SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND dAddDate BETWEEN
				@dStartTime AND @dEndTime ''
			END

		END
		
		IF(@ITimeType=2)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SET @SQL02 = '' SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND dUpdateDate BETWEEN
				@dStartTime AND @dEndTime''
			END
			ELSE
			BEGIN
				SET SQL02 = ''SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND dUpdateDate BETWEEN
				@dStartTime AND @dEndTime ''
			END 
		END
	END	
	IF(@iDel=2)
	BEGIN
		IF(@ITimeType=1)
		BEGIN
			IF(@iNum=0)
			BEGIN
				SET @SQL02 = ''SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND (dAddDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = ''''N''''''
			END
			ELSE
			BEGIN
                              	SET @SQL02 = ''SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND (dAddDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = ''''N''''''
			END
		END
		
		IF(@ITimeType=2)
		BEGIN
			IF(@iNum=0)
			BEGIN
                        	SET @SQL02 = ''SELECT iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND (dUpdateDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = ''''N''''''
			END
			ELSE
			BEGIN
				SET @SQL02 = ''SELECT TOP (@iNum) iId,iClassId,vcFilePath,cCreated FROM T_News_NewsInfo (NOLOCK) WHERE cChecked=''''Y'''' AND cDel =''''N'''' AND (dUpdateDate BETWEEN
				@dStartTime AND @dEndTime) AND cCreated = ''''N''''''
			END 
		END
	END
    Execute Sp_Executesql @SQL02
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_CheckThiefTopic]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROC [dbo].[SP_News_CheckThiefTopic]
(
	@vcTitle VARCHAR(100),
	@iClassID INT,
	@reValue INT OUTPUT
)
AS

IF(@vcTitle = '''')
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_GetNewsInfoById]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_News_GetNewsInfoById]
(
	@iID INT
)
/*
EXEC SP_News_GetNewsInfoById
	@iID=1
*/
AS

	SELECT iClassID,vcTitle,vcUrl,vcContent,vcAuthor,iFrom,iCount,vcKeyWord,
	vcEditor,cCreated,vcSmallImg,vcBigImg,vcShortContent,vcSpeciality,cChecked,
	cDel,cPostByUser,vcFilePath,dAddDate,dUpDateDate,vcTitleColor,cStrong
	 FROM T_News_NewsInfo (NOLOCK)
	 WHERE iId = @iID 

	SELECT B.vcClassName,B.vcName,B.iParent,B.iTemplate,B.iListTemplate,B.vcDirectory,B.vcUrl,B.iOrder
	FROM T_News_NewsInfo A (NOLOCK),T_News_ClassInfo B (NOLOCK) 
	WHERE A.iId = @iID AND A.iClassID = B.iId

	SELECT B.vcTitle,B.vcUrl,B.dUpdateDate
	FROM T_News_NewsInfo A (NOLOCK),T_News_NewsFromInfo B (NOLOCK) 
	WHERE A.iId = @iID AND A.iFrom = B.iId


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_UserManage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SP_UserManage]
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
	@action CHAR(2) =''01'',		--操作01，添加。02，修改。03，删除
	@reValue INT OUTPUT 		--返回参数，小于0为异常
)
AS
IF(@action=''01'')
BEGIN
	
	If(@PassWord='''')
	BEGIN
		SET @reValue = -1000000090	--密码不能为空！
		RETURN;
	END
	If(@LastLoginIp='''')
	BEGIN
		SET @reValue = -1000000091	--最后登陆IP不能为空！
		RETURN;
	END
	If(@Name='''')
	BEGIN
		SET @reValue = -1000000092	--姓名不能为空！
		RETURN;
	END
	
	DECLARE @uCount INT
	SET @uCount = 0;
	SELECT @uCount = COUNT(1) FROM [USER] (NOLOCK) WHERE [Name] = @Name
	
	IF(@uCount>0)
	BEGIN
		SET @reValue = -1000000094 --该用户名已经被其他人注册
		RETURN;
	END
	
	INSERT INTO [USER] (State,UserLevel,UserClubLevel,CreateTime,LastLoginTime,Id,PassWord,LastLoginIp,[NAME])
	VALUES(@State,@UserLevel,@UserClubLevel,GETDATE(),GETDATE(),@Id,@PassWord,@LastLoginIp,@Name)
	
	IF(@Email!='''')
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
			'''',
			0,
			0,
			0,
			'''',
			'''',
			'''',
			0,
			@Email
		)
	END
END

IF(@action=''02'')
BEGIN
	IF(@Id='''')
	BEGIN
		SET @reValue = -1000000093 --需要修改的用户ID为空
		RETURN;
	END

	If(@PassWord='''')
	BEGIN
		SET @reValue = -1000000090	--密码不能为空！
		RETURN;
	END
	If(@LastLoginIp='''')
	BEGIN
		SET @reValue = -1000000091	--最后登陆IP不能为空！
		RETURN;
	END
	If(@Name='''')
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


SET @reValue=1' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Sp_Manage_AddAdminLog]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[Sp_Manage_AddAdminLog]
(
	@vcAdminName VARCHAR(50),
	@vcActive NVARCHAR(255),
	@vcIp VARCHAR(15),
	@reValue INT OUTPUT
)--WITH ENCRYPTION 
AS


IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

INSERT INTO T_Manage_AdminLog (vcAdminName,vcActive,vcIp)
VALUES(@vcAdminName,@vcActive,@vcIp)

SET @reValue = 1


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Files_FileInfoManageByAdmin]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROC [dbo].[SP_Files_FileInfoManageByAdmin]
(
	@cAction CHAR(2) = ''01'',
	@vcAdminName VARCHAR(50) = '''',
	@vcIp VARCHAR(15)='''',
	@iID BIGINT = 0,
	@iClassId INT = 0,
	@vcFileName NVARCHAR(100)='''',
	@iSize INT = 0,
	@vcType VARCHAR(10) ='''',
	@iDowns INT =0,
	@iRequest INT =0,
	@reValue INT OUTPUT
)
AS

IF(@cAction=''01'')
BEGIN
	IF(@vcAdminName IS NULL OR @vcAdminName ='''')
	BEGIN
		SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
		RETURN;
	END

	IF(@iClassId =0 OR @iClassId IS NULL)
	BEGIN
		SET @reValue=-1000000060 --文件所在的文件夹不能为空
		RETURN;
	END
	IF(@vcFileName ='''' OR @vcFileName IS NULL)
	BEGIN
		SET @reValue=-1000000061 --文件名称不能为空
		RETURN;
	END
	IF(@vcType ='''' OR @vcType IS NULL)
	BEGIN
		SET @reValue=-1000000062 --文件类型不能为空
		RETURN;
	END

	IF(@iSize =0 OR @iSize IS NULL)
	BEGIN
		SET @reValue=-1000000063 --文件大小不能为空
		RETURN;
	END

	INSERT INTO T_Files_FileInfos (iID,iClassId,vcFileName,iSize,vcType,dCreateDate,vcIP)
	VALUES(@iID,@iClassId,@vcFileName,@iSize,@vcType,GETDATE(),@vcIP)
END

IF(@cAction=''02'')
BEGIN
	IF(@iID =0 OR @iID IS NULL)
	BEGIN
		SET @reValue=-1000000064 --需要删除的文件间编号不能为空
		RETURN;
	END
	DELETE T_Files_FileInfos WHERE iID = @iID
END

SET @reValue = 1
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_CheckIP]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SP_Manage_CheckIP]
(
	@vcIP VARCHAR(15),
	@reValue INT OUTPUT
)

/*

DECLARE @reValue INT
EXEC SP_Manage_CheckIP
	@vcIP = ''127.0.0.1'',
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

SELECT @IP_Count = count(*) FROM T_Manage_RefuseIp (NOLOCK) WHERE 
vcStartIp<=@IP_NUM and vcEndtIp>=@IP_NUM and cValid = ''Y''

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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Sp_Manage_OnlineKickTimeOutAdmin]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[Sp_Manage_OnlineKickTimeOutAdmin]

AS
DECLARE @TimeOut int
SET @TimeOut = 60*30 --秒

update T_Manage_AdminInfo SET cIsOnline=''N'' 
WHERE  vcAdminName in (SELECT DISTINCT vcadminname FROM T_Manage_Online (NOLOCK) 
WHERE DATEDIFF(s, dActiveTime, GETDATE())> @TimeOut)

DELETE FROM T_Manage_Online 
WHERE DATEDIFF(s, dActiveTime, GETDATE())> @TimeOut
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_AdminLogout]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SP_Manage_AdminLogout]
(
	@vcAdminName VARCHAR(50)
)
AS
	UPDATE T_Manage_AdminInfo SET cIsOnline = ''N'' WHERE vcAdminName =@vcAdminName
	DELETE T_Manage_Online WHERE vcAdminName =@vcAdminName
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_NewsInfoStatManage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROC [dbo].[SP_News_NewsInfoStatManage]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@cAction CHAR(2) = ''01'',
	@ids VARCHAR(1000) ='''',
	@vcKey VARCHAR(30) = '''',
	@vcKeValue CHAR(1) ='''',
	@reValue INT OUTPUT
)
AS

IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@ids='''')
BEGIN
	SET @reValue = -1000000051 --需要改变的文章不能为空
	RETURN
END

DECLARE @LOG NVARCHAR(200)
IF(@cAction=''01'')
BEGIN
	IF(@vcKeValue ='''' OR @vcKeValue IS NULL OR (@vcKeValue <> ''N'' AND @vcKeValue <> ''Y''))
	BEGIN
		SET @reValue = -1000000049 --需要修改的状态的值不能为空
		RETURN
	END
	
	DECLARE @SQL NVARCHAR(1000)
	IF(@vcKey ='''' OR @vcKey IS NULL)
	BEGIN
		SET @reValue = -1000000048 --需要修改的状态名不能为空
		RETURN
	END
	ELSE IF(@vcKey = ''Checked'')
	BEGIN
		IF(CHARINDEX('','',@ids)>0)
		BEGIN
			SET @SQL = ''UPDATE T_News_NewsInfo SET cChecked = ''''''+@vcKeValue+''''''''+
			+'' WHERE iID IN (''+@ids+'')''
			Execute Sp_Executesql @SQL
		END
		ELSE
		BEGIN
			UPDATE T_News_NewsInfo SET cChecked = @vcKeValue WHERE iID = @ids
		END
		SET @LOG = ''更改资源[''+@ids+'']审核状态为[''+@vcKeValue+'']''
	END
	ELSE IF(@vcKey = ''Created'')
	BEGIN
		IF(CHARINDEX('','',@ids)>0)
		BEGIN
			SET @SQL = ''UPDATE T_News_NewsInfo SET cCreated = ''''''+@vcKeValue+''''''''+
			+'' WHERE iID IN (''+@ids+'')''
			Execute Sp_Executesql @SQL
		END
		ELSE
		BEGIN
			UPDATE T_News_NewsInfo SET cCreated = @vcKeValue WHERE iID = @ids
		END
		SET @LOG = ''更改资源[''+@ids+'']生成状态为[''+@vcKeValue+'']''
	END
	ELSE IF(@vcKey = ''Del'')
	BEGIN
		IF(CHARINDEX('','',@ids)>0)
		BEGIN
			SET @SQL = ''UPDATE T_News_NewsInfo SET cDel = ''''''+@vcKeValue+''''''''+
			+'' WHERE iID IN (''+@ids+'')''
			Execute Sp_Executesql @SQL
		END
		ELSE
		BEGIN
			UPDATE T_News_NewsInfo SET cDel = @vcKeValue WHERE iID = @ids
		END
		IF(@vcKeValue=''Y'')
		BEGIN
			IF(CHARINDEX('','',@ids)>0)
			BEGIN
				SET @SQL = ''UPDATE T_News_NewsInfo SET cCreated = ''''N''''''+
				+'' WHERE iID IN (''+@ids+'')''
				Execute Sp_Executesql @SQL
			END
			ELSE
			BEGIN
				UPDATE T_News_NewsInfo SET cCreated = ''N'' WHERE iID = @ids
			END
		END
		
		SET @LOG = ''更改资源[''+@ids+'']存在状态为[''+@vcKeValue+'']''
	END
	ELSE
	BEGIN
		SET @reValue = -1000000050 --并无该状态
		RETURN
	END
END

IF(@cAction=''02'')
BEGIN
	SET @SQL = ''DELETE T_News_NewsInfo WHERE iId in ('' + @ids+'')''
	Execute Sp_Executesql @SQL

	SET @LOG = ''彻底删除资源[''+@ids+'']''
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



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_DelNewsClassById]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_News_DelNewsClassById]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@iClassId INT,
	@reValue INT OUTPUT
)
AS

IF(@vcAdminName ='''' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@iClassId =0 OR @iClassId IS NULL)
BEGIN
	SET @reValue=-1000000031 --资讯分类编号为空
	RETURN;
END

DECLARE @n_count INT
SET @n_count = 0
SELECT @n_count =  COUNT(1) FROM T_News_NewsInfo (NOLOCK) WHERE iClassID=@iClassId
IF(@n_count>0)
BEGIN
	SET @reValue=-1000000032 --该分类下还存在资源，请移出后再删除
	RETURN;
END

SET @n_count = 0
SELECT @n_count =  COUNT(1) FROM T_News_ClassInfo (NOLOCK) WHERE iParent = @iClassId
IF(@n_count>0)
BEGIN
	SET @reValue=-1000000033 --该分类下还存在子分类，请移出后再删除
	RETURN;
END

DELETE T_News_ClassInfo WHERE iID=@iClassId

DECLARE @LOG VARCHAR(100)
SET @LOG =''删除分类[''+CAST(@iClassId AS VARCHAR)+'']''

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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_NewsFromManage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SP_News_NewsFromManage]
(
	@vcAdminname VARCHAR(50),
	@vcIp VARCHAR(15),
	@iId INT=0,
	@vcTitle VARCHAR(150)='''',
	@vcUrl VARCHAR(255)='''',
	@cAction CHAR(2) =''01'',
	@iDS VARCHAR(100)='''',
	@reValue INT OUTPUT
)
AS

IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@cAction=''01'' OR @cAction=''02'')
BEGIN
	IF(@vcTitle ='''' OR @vcTitle IS NULL)
	BEGIN
		SET @reValue = -1000000052 --显示文字不能为空
		RETURN
	END
	IF(@vcUrl ='''' OR @vcUrl IS NULL)
	BEGIN
		SET @reValue = -1000000053 --显示链接不能为空
		RETURN
	END
END

IF(@cAction=''02'')
BEGIN
	IF(@iId=0)
	BEGIN
		SET @reValue = -1000000054 --修改的记录不存在
		RETURN
	END
END

IF(@cAction=''03'')
BEGIN
	IF(@iDS='''' OR @iDS IS NULL)
	BEGIN
		SET @reValue = -1000000055 --删除的记录不存在
		RETURN
	END
END

DECLARE @LOG NVARCHAR(1000)
IF(@cAction=''01'')
BEGIN
	DECLARE @TID INT
	INSERT INTO T_News_NewsFromInfo (vcTitle,vcUrl,dUpdateDate)
	VALUES(@vcTitle,@vcUrl,GETDATE())
	SET @TID=@@IDENTITY
	SET @LOG = ''添加新的来源[''+CAST(@TID AS VARCHAR)+'']''
END

IF(@cAction=''02'')
BEGIN
	SET @LOG = ''修改来源[''+CAST(@iId AS VARCHAR)+'']''
	UPDATE T_News_NewsFromInfo SET vcTitle=@vcTitle,vcUrl=@vcUrl
	WHERE iID=@iId
END

IF(@cAction=''03'')
BEGIN
	DECLARE @SQL NVARCHAR(1000)
	IF(CHARINDEX('','',@ids)>0)
	BEGIN
		SET @SQL = ''DELETE T_News_NewsFromInfo WHERE iID IN (''+@ids+'')''
		Execute Sp_Executesql @SQL
	END
	ELSE
	BEGIN
		DELETE T_News_NewsFromInfo WHERE iID = @ids
	END
	
	SET @LOG = ''删除来源[''+CAST(@ids AS VARCHAR)+'']''
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_ClassInfoManage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_News_ClassInfoManage]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vcClassName VARCHAR(200),
	@vcName VARCHAR(50),
	@iParent INT,
	@iTemplate INT,
	@iListTemplate INT,
	@vcDirectory VARCHAR(200),
	@vcUrl VARCHAR(255),
	@iOrder INT,
	@action CHAR(2) =''01'',
	@cVisible CHAR(1) = ''Y'',
	@iClassId INT = 0,
	@reValue INT OUTPUT
)

/*

DECLARE @reValue INT
EXEC SP_News_AddClassInfo
	@vcAdminName =''admin'',
	@vcIp =''127.0.0.1'',
	@vcClassName =''dsfsdg'',
	@vcName =''dgg'',
	@iParent =0,
	@iTemplate = 0,
	@iListTemplate=0,
	@vcDirectory='''',
	@vcUrl ='''',
	@iOrder =0,
	@reValue =@reValue OUTPUT

SELECT @reValue
*/
AS

IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@vcClassName='''' OR @vcClassName IS NULL OR @vcName='''' OR @vcName IS NULL)
BEGIN
	SET @reValue=-1000000020 --分类名或别名不能为空
	RETURN
END

IF(@iParent<>0)
BEGIN
	IF(@iTemplate=0 OR @iTemplate IS NULL)
	BEGIN
		SET @reValue=-1000000021 --模版编号不能为空
		RETURN
	END

	IF(@iListTemplate=0 OR @iListTemplate IS NULL)
	BEGIN
		SET @reValue=-1000000029 --列表模版编号不能为空
		RETURN
	END

	IF(@vcDirectory='''' OR @vcDirectory IS NULL)
	BEGIN
		SET @reValue=-1000000022 --生成路径不能为空
		RETURN
	END

	IF(@vcUrl='''' OR @vcUrl IS NULL)
	BEGIN
		SET @reValue=-1000000023 --前台分类首页不能为空
		RETURN
	END
END

DECLARE @TID INT
DECLARE @LOG VARCHAR(100)

IF(@action=''01'')
BEGIN
	
	INSERT INTO T_News_ClassInfo(vcClassName,vcName,iParent,iTemplate,iListTemplate,vcDirectory,vcUrl,iOrder,Visible)
	VALUES(@vcClassName,@vcName,@iParent,@iTemplate,@iListTemplate,@vcDirectory,@vcUrl,@iOrder,@cVisible)

	SET @TID=@@IDENTITY
	SET @LOG =''添加资讯分类''+CAST(@TID AS VARCHAR)
END

IF(@action=''02'')
BEGIN
	IF(@iClassId=@iParent)
	BEGIN
		SET @reValue=-1000000030 --父类ID不能为自己的ID
		RETURN;
	END
--	DECLARE @T_COUNT INT
--	SET @T_COUNT = 0
--	SELECT @T_COUNT = COUNT(1) FROM T_News_ClassInfo (NOLOCK) WHERE iParent= @iClassId
--	IF(@T_COUNT>0)
--	BEGIN
--		SET @reValue = -1000000056 --父分类不能变成孩子的孩子
--		RETURN
--	END

	SET @LOG =''修改资讯分类''+CAST(@iClassId AS VARCHAR)
	UPDATE T_News_ClassInfo SET vcClassName=@vcClassName,@vcName=vcName,iParent=@iParent,
	iTemplate=@iTemplate,iListTemplate=@iListTemplate,vcDirectory=@vcDirectory,vcUrl=@vcUrl,iOrder=@iOrder,
	Visible = @cVisible
	WHERE iID =@iClassId
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







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Files_FilesClassManage]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[SP_Files_FilesClassManage]
(
	@vcAdminName VARCHAR(50),
	@vcIP VARCHAR(15),
	@cAction CHAR(2) =''01'',
	@iId INT = 0,
	@vcFileName NVARCHAR(100),
	@iParentId int =0,
	@vcMeno NVARCHAR(100),
	@reValue INT OUTPUT
)
AS
IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

IF(@vcFileName='''' OR @vcFileName IS NULL)
BEGIN
	SET @reValue=-1000000057 --文件名不能为空
	RETURN
END

IF(@vcMeno='''' OR @vcMeno IS NULL)
BEGIN
	SET @reValue=-1000000058 --简单说明不能为空 
	RETURN
END

DECLARE @TID INT
DECLARE @LOG NVARCHAR(100)

IF(@cAction=''01'')
BEGIN
	INSERT INTO T_Files_Class (vcFileName,iParentId,vcMeno) VALUES(@vcFileName,@iParentId,@vcMeno)

	SET @TID=@@IDENTITY
	SET @LOG =''添加文件夹''+ @vcFileName
END

IF(@cAction=''02'')
BEGIN
	DECLARE @T_FILENAME NVARCHAR(100)
	DECLARE @T_DO NVARCHAR(100)
	SET @T_DO = ''''
	
	SELECT @T_FILENAME = vcFileName,@T_DO=vcMeno FROM T_Files_Class WHERE iId = @iId
	IF(@T_DO='''')
	BEGIN
		SET @reValue=-1000000059 --修改的分类不存在
		RETURN
	END
	
	UPDATE T_Files_Class SET vcFileName=@vcFileName,vcMeno=@vcMeno,iParentId=@iParentId
	WHERE iId = @iId

	IF(@T_FILENAME<>@vcFileName)
	BEGIN
		SET @LOG = ''修改文件夹名''+@T_FILENAME+''为''+@vcFileName+'' ''
	END
	ELSE
	BEGIN
		SET @LOG = ''修改文件夹''+@T_FILENAME
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_OnlineMdy]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'


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

SELECT @n_count = COUNT(1) FROM T_Manage_Online (NOLOCK) WHERE vcAdminName = @vcAdminName

IF(@n_count=0)
BEGIN
	UPDATE T_Manage_AdminInfo SET cIsOnline = ''Y'',vcLastLoginIp=@vcIp WHERE vcAdminName=@vcAdminName
	INSERT INTO T_Manage_Online (vcAdminName,vcIp,dActiveTime,vcActive)
	VALUES(@vcAdminName,@vcIp,GETDATE(),@vcActive)
END

IF(@n_count=1)
BEGIN
	UPDATE T_Manage_Online SET dActiveTime = GETDATE(),vcActive = @vcActive
	WHERE vcAdminName = @vcAdminName
END

IF(@n_count>1)
BEGIN
	DELETE T_Manage_Online WHERE vcAdminName = @vcAdminName
	INSERT INTO T_Manage_Online (vcAdminName,vcIp,dActiveTime,vcActive)
	VALUES(@vcAdminName,@vcIp,GETDATE(),@vcActive)
END


EXEC Sp_Manage_OnlineKickTimeOutAdmin

SET @reValue = 1



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_CheckAdminPower]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_Manage_CheckAdminPower]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@reValue INT OUTPUT
)
/*
DECLARE @reValue INT
EXEC SP_Manage_CheckAdminPower
	@vcAdminName =''admins'',
	@vcIp =''127.0.0.1'',
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

IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

DECLARE @t_Online CHAR(1)
SELECT @t_Online = cIsOnline FROM T_Manage_AdminInfo (NOLOCK) WHERE vcAdminName =@vcAdminName

IF(@t_Online<>''Y'')
BEGIN
	SET @reValue=-1000000017 --您不并不在线，您是否尚未登陆？
END

SET @reValue=1

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Template_DelTemplate]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_Template_DelTemplate]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vctemps VARCHAR(1000),
	@reValue INT OUTPUT 
)
AS

IF(@vctemps ='''' OR @vctemps IS NULL)
BEGIN
	SET @reValue = -1000000028 --尚未选择需要删除的资讯模版
	RETURN;
END	

IF(@vcAdminName='''' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue= -1000000012 --操作员为空，您是否尚未登陆？
	RETURN
END

DECLARE @sql NVARCHAR(2000)

IF(CHARINDEX('','',@vctemps)>0)
BEGIN
	SET @sql = ''DELETE T_Template_TemplateInfo WHERE iId IN (''+@vctemps+'')''
	Execute Sp_Executesql @sql
END
ELSE
BEGIN
	DELETE T_Template_TemplateInfo WHERE iId = @vctemps
END


DECLARE @LOG VARCHAR(2000)
SET @LOG=''删除模版[''+@vctemps+'']''
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Template_ManageTemplate]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROC [dbo].[SP_Template_ManageTemplate]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@iSiteId INT,
	@iType INT,
	@iParentId INT =0,
	@iSystemType INT =0,
	@vcTempName VARCHAR(50),
	@vcContent TEXT,
	@vcUrl VARCHAR(255),
	@action CHAR(2) = ''01'',
	@iId INT =0,
	@reValue INT OUTPUT
)
AS

DECLARE @LOG VARCHAR(200)
DECLARE @TID INT

IF(@vcAdminName='''' OR @vcAdminName IS NULL)
BEGIN
	SET @reValue= -1000000012 --操作员为空，您是否尚未登陆？
	RETURN
END

IF(@iType=-1)
BEGIN
	SET @reValue= -1000000026 --请选择模版类别！ 
	RETURN
END

IF(@iType=0)
BEGIN
	IF(@vcUrl='''' OR @vcUrl IS NULL)
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

if(@action=''01'')
BEGIN
	INSERT INTO T_Template_TemplateInfo (iSiteId,iType,iParentId,iSystemType,vcTempName,vcContent,vcUrl,dUpdateDate,dAddDate)
	VALUES(@iSiteId,@iType,@iParentId,@iSystemType,@vcTempName,@vcContent,@vcUrl,GETDATE(),GETDATE())
	SET @TID=@@IDENTITY
	SET @LOG = ''添加新模版[''+CAST(@TID AS VARCHAR)+'']''
END

if(@action=''02'')
BEGIN
	UPDATE T_Template_TemplateInfo SET vcTempName=@vcTempName,vcContent=@vcContent,
	vcUrl=@vcUrl,dUpdateDate=GETDATE() WHERE iId =@iID
	SET @LOG = ''修改模版[''+CAST(@iId AS VARCHAR)+'']''
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



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_News_SpecialityAdmin]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_News_SpecialityAdmin]
(
	@vcAdminname VARCHAR(50),
	@vcIP VARCHAR(50),
	@cAction CHAR(2) =''01'',
	@iId INT = 0,
	@iSiteId INT = 0,
	@iParent INT = 0,
	@vcTitle VARCHAR(50)='''',
	@vcExplain VARCHAR(50)='''',
	@IDs VARCHAR(200)='''',
	@reValue INT OUTPUT
)
AS


IF(@vcAdminName IS NULL OR @vcAdminName ='''')
BEGIN
	SET @reValue=-1000000012 --操作员为空，您是否尚未登陆？
	RETURN;
END

--检测
IF(@cAction=''01'' OR @cAction=''02'')
BEGIN
--	IF(@iSiteId=0 OR @iSiteId IS NULL)
--	BEGIN
--		SET @reValue = -1000000034 --特性所属站点不明确
--		RETURN
--	END
	
	IF(@vcTitle = '''' OR @vcTitle IS NULL)
	BEGIN
		SET @reValue = -1000000035 --特性名称未输入
		RETURN
	END
END


DECLARE @LOG VARCHAR(1000)
IF(@cAction=''01'')
BEGIN
	INSERT INTO T_News_Speciality (iSiteId,iParent,vcTitle,vcExplain,dUpDateDate)
	VALUES(@iSiteId,@iParent,@vcTitle,@vcExplain,GETDATE())
	SET @LOG = ''添加新的资讯特性[''+ CAST(@@IDENTITY AS VARCHAR)+'']''
END

IF(@cAction=''02'')
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
		SELECT @count =COUNT(1) FROM T_News_Speciality (NOLOCK) WHERE iSiteId=@iSiteId AND iID =@iParent
		IF(@count=0)
		BEGIN
			SET @reValue = -1000000037 --父类ID不存在
			RETURN
		END
	END

	SET @LOG = ''修改资讯特性[''+ CAST(@iId AS VARCHAR)+'']''
	UPDATE T_News_Speciality SET iSiteId = @iSiteId,iParent=@iParent,
	vcTitle=@vcTitle,vcExplain=@vcExplain,dUpDateDate=GETDATE()
	WHERE iID=@iId
END

IF(@cAction=''03'')
BEGIN
	IF(@IDs='''' OR @IDs IS NULL)
	BEGIN
		SET @reValue = -1000000038 --没有选择需要删除的ID
		RETURN
	END
	IF(CHARINDEX('','',@IDs)>0)
	BEGIN
		DECLARE @sql NVARCHAR(1000)
		SET @sql = ''DELETE T_News_Speciality WHERE iId IN (''+@IDs+'')''
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		DELETE T_News_Speciality WHERE iId = @IDs
	END
	
	SET @LOG=''删除资源特性[''+@IDs+'']''
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_GetAdminInfoByName]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE PROC [dbo].[SP_Manage_GetAdminInfoByName]
(
	@vcAdminName VARCHAR(50),
	@vcIP VARCHAR(15),
	@cAction CHAR(2) = ''02'', --01,登陆 02获得修改资料
	@reValue INT OUTPUT
)
/*
DECLARE @reValue INT
EXEC SP_Manage_GetAdminInfoByName
	@vcAdminName =''admin'',
	@vcIP = ''127.0.0.1'',
	@cAction = ''01'',
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

IF(@cAction=''01'')
BEGIN

	EXEC Sp_Manage_OnlineKickTimeOutAdmin

	DECLARE @t_ip VARCHAR(15)
	DECLARE @t_online CHAR(1)
	DECLARE @cIsDel CHAR(1)

	SELECT @t_ip = vcLastLoginIp,@t_online =cIsOnline,@cIsDel=cIsDel FROM T_Manage_AdminInfo (NOLOCK) 
	WHERE vcAdminName = @vcAdminName 
	IF(@t_online <> ''Y'')
	BEGIN
		SET @reValue = -1000000007 --该管理员还没有登陆
		RETURN;
	END
	
	IF(@t_ip<>@vcIP)
	BEGIN
		SET @reValue = -1000000008 --该管理员用其他的IP登陆了
		RETURN;
	END

	IF(@cIsDel=''Y'')
	BEGIN
		SET @reValue = -1000000018 --您的帐号已经被删除，请联系管理员！
		RETURN;
	END
	
	DECLARE @t_vcPopedoms VARCHAR(1000)
	DECLARE @tt_vcPopedoms VARCHAR(1000)
	
	SELECT @t_vcPopedoms = A.vcPopedom ,@tt_vcPopedoms =B.vcPopedom
	FROM  T_Manage_AdminInfo A (NOLOCK),  T_Manage_AdminRole  B (NOLOCK) 
	WHERE A.vcAdminName = @vcAdminName AND A.iRole = B.iID

	IF(@t_vcPopedoms='''' OR @t_vcPopedoms IS NULL)
	BEGIN
		SET @t_vcPopedoms = ''0''
	END

	IF(@tt_vcPopedoms='''' OR @tt_vcPopedoms IS NULL)
	BEGIN
		SET @tt_vcPopedoms = ''0''
	END
	
	SET @t_vcPopedoms = @t_vcPopedoms +'',''+@tt_vcPopedoms

	DECLARE @sql NVARCHAR(1000)
	SET @sql = ''SELECT iId,vcPopName,vcUrl,iParentId,cValid FROM T_Manage_Popedom (NOLOCK) WHERE iID IN ( '' + @t_vcPopedoms + '')''
	Execute Sp_Executesql @sql

END

SELECT A.vcAdminName,A.vcNickName,A.vcPassword,A.vcClassPopedom ,A.cLock,A.vcPopedom,A.dAddDate,
A.dUpdateDate,A.dLoginDate,A.dLastLoginDate,A.iLoginCount,A.vcLastLoginIp,A.cIsOnline,
B.vcRoleName,B.vcContent,B.vcPopedom AS vcPopedomW,B.vcClassPopedom AS vcClassPopedomW
FROM  T_Manage_AdminInfo A (NOLOCK),  T_Manage_AdminRole  B (NOLOCK) 
WHERE A.vcAdminName = @vcAdminName AND A.iRole = B.iID


SET @reValue = 1





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_AdminRoleDel]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
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

IF(@iRole IS NULL OR @iRole ='''')
BEGIN
	SET @reValue = -1000000014 -- 角色组编号不能为空
	RETURN;
END

DECLARE @t_Count INT
SELECT @t_Count = COUNT(1) FROM T_Manage_AdminInfo (NOLOCK) WHERE iRole = @iRole
IF(@t_Count>0)
BEGIN
	SET @reValue = -1000000015 -- 要删除此联系组，请先移出或删除此组中的管理员
	RETURN;
END


DELETE T_Manage_AdminRole WHERE iID = @iRole

DECLARE @log VARCHAR(1000)
SET @log = ''删除角色组ID[''+CAST(@iRole AS VARCHAR(1000))+'']''
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


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_AdminRoleInfoMdy]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROC [dbo].[SP_Manage_AdminRoleInfoMdy]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vcRoleName VARCHAR(50),
	@vcContent VARCHAR(255),
	@vcPopedom VARCHAR(1000),
	@vcClassPopedom VARCHAR(255),
	@cAction CHAR(2) =''01'',
	@iRole INT = 0,
	@reValue INT OUTPUT
)
AS

IF(@vcRoleName IS NULL OR @vcRoleName='''')
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

IF(@cAction=''01'')
BEGIN
	SET @log = ''添加角色组['' + @vcRoleName + '']''
	INSERT INTO T_Manage_AdminRole (vcRoleName,vcContent,vcPopedom,vcClassPopedom)
	VALUES(@vcRoleName,@vcContent,@vcPopedom,@vcClassPopedom)
END

IF(@cAction=''02'' AND @iRole>0)
BEGIN
	SET @log = ''修改角色组['' + CAST(@iRole AS VARCHAR(1000)) + '':'' + @vcRoleName + '']''
	UPDATE T_Manage_AdminRole
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





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_AddAdmin]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'






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
	@action CHAR(2) = ''01'', 
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
IF(@action=''01'')
BEGIN
	DECLARE @t_vcAdminName varchar(50)
	SELECT @t_vcAdminName = vcAdminName FROM T_Manage_AdminInfo (NOLOCK) WHERE vcAdminName = @vcAdminName
	IF(@t_vcAdminName<>'''' AND @t_vcAdminName IS NOT NULL)
	BEGIN
		SET @reValue = -1000000005 --管理员帐号已经存在
		RETURN;
	END
END

SET XACT_ABORT ON
BEGIN TRAN
	IF(@action=''01'')
	BEGIN
		SET @log = ''添加管理员['' + @vcAdminName + '']''
		INSERT INTO T_Manage_AdminInfo
		(vcAdminName,vcNickName,vcPassWord,iRole,clock,vcPopedom,vcClassPopedom)
		VALUES(@vcAdminName,@vcNickname,@vcPassWord,@iRole,@clock,@vcPopedom,@vcClassPopedom)
	END
	
	IF(@action=''02'')
	BEGIN
		SET @log = ''修改管理员['' + @vcAdminName + '']''
		
		IF(@vcPassWord<>'''' AND @vcPassWord IS NOT NULL)
		BEGIN
			UPDATE T_Manage_AdminInfo SET vcNickName=@vcNickName,vcPassWord=@vcPassWord,iRole=@iRole,
			clock=@clock,vcPopedom=@vcPopedom,vcClassPopedom=@vcClassPopedom
			WHERE vcAdminName=@vcAdminName
		END
		ELSE
		BEGIN
			UPDATE T_Manage_AdminInfo SET vcNickName=@vcNickName,iRole=@iRole,
			clock=@clock,vcPopedom=@vcPopedom,vcClassPopedom=@vcClassPopedom
			WHERE vcAdminName=@vcAdminName
		END
	END
COMMIT

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




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_ChanageAdminLoginInfo]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'


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

SELECT @t_pwd = vcPassword,@t_lock = cLock FROM T_Manage_AdminInfo (NOLOCK) WHERE  vcAdminName = @vcAdminName

IF(@t_pwd<>@oldPwd)
BEGIN
	SET @reValue = -1000000009 --输入原始密码不正确
	RETURN;
END

IF(@t_lock=''Y'')
BEGIN
	SET @reValue = -1000000010 --您的帐号已经锁定，不能修改登陆信息
	RETURN;
END

IF(@NewPwd='''' OR @NewPwd IS NULL)
BEGIN
	UPDATE T_Manage_AdminInfo SET vcNickName = @vcNickName
	WHERE vcAdminName = @vcAdminName
END
ELSE
BEGIN
	UPDATE T_Manage_AdminInfo SET vcNickName = @vcNickName,vcPassword = @NewPwd
	WHERE vcAdminName = @vcAdminName
END

EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcIp,
	@vcActive = ''修改登陆信息'',
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

SET @reValue = 1



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_AdminChangeGroup]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'



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
	@vcAdminName = ''admin'',
	@iRoleId =1,
	@vcAdmins =''''''leo85729'''',''''admin'''''',
	@vcIp =''127.0.0.1'',
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

IF(CHARINDEX('','',@vcAdmins)>0)
BEGIN
	DECLARE @sql NVARCHAR(2000)
	SET @sql = ''UPDATE T_Manage_AdminInfo SET iRole = ''+CAST(@iRoleId AS VARCHAR(1000))
	+'' WHERE vcAdminName IN (''+@vcAdmins+'')''
	Execute Sp_Executesql @sql
END
ELSE
BEGIN
	SET @vcAdmins = REPLACE(@vcAdmins,'''''''','''');
	UPDATE T_Manage_AdminInfo SET iRole = @iRoleId WHERE vcAdminName=@vcAdmins
END


DECLARE @log VARCHAR(1000)
SET @log = ''移动管理员['' + @vcAdmins + '']到角色组ID【''+CAST(@iRoleId AS VARCHAR(1000))+''】''
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




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_DelAdmins]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[SP_Manage_DelAdmins]
(
	@vcAdminName VARCHAR(50),
	@vcIp VARCHAR(15),
	@vcAdmins VARCHAR(1000),
	@action CHAR(2) =''01'',
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

IF(@vcAdmins ='''' OR @vcAdmins IS NULL)
BEGIN
	SET @reValue = -1000000016 --尚未选择需要删除的管理员
	RETURN;
END

DECLARE @sql NVARCHAR(2000)
DECLARE @log VARCHAR(1000)
IF(@action=''01'')
BEGIN
	IF(CHARINDEX('','',@vcAdmins)>0)
	BEGIN
		SET @sql = ''UPDATE T_Manage_AdminInfo SET cIsDel = ''''Y''''''+
		+'' WHERE vcAdminName IN (''+@vcAdmins+'')''
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		SET @vcAdmins = REPLACE(@vcAdmins,'''''''','''');
		UPDATE T_Manage_AdminInfo SET cIsDel = ''Y'' WHERE vcAdminName=@vcAdmins
	END
	
	SET @log = ''删除管理员['' + @vcAdmins + '']到回收站''
END

IF(@action=''02'')
BEGIN
	IF(CHARINDEX('','',@vcAdmins)>0)
	BEGIN
		SET @sql = ''DELETE T_Manage_AdminInfo WHERE vcAdminName IN (''+@vcAdmins+'')''
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		SET @vcAdmins = REPLACE(@vcAdmins,'''''''','''');
		DELETE T_Manage_AdminInfo WHERE vcAdminName=@vcAdmins
	END
	SET @log = ''彻底删除管理员['' + @vcAdmins + '']''
END

IF(@action=''03'')
BEGIN
	IF(CHARINDEX('','',@vcAdmins)>0)
	BEGIN
		SET @sql = ''UPDATE T_Manage_AdminInfo SET cIsDel = ''''N''''''+
		+'' WHERE vcAdminName IN (''+@vcAdmins+'')''
		Execute Sp_Executesql @sql
	END
	ELSE
	BEGIN
		SET @vcAdmins = REPLACE(@vcAdmins,'''''''','''');
		UPDATE T_Manage_AdminInfo SET cIsDel = ''N'' WHERE vcAdminName=@vcAdmins
	END
	
	SET @log = ''救回管理员['' + @vcAdmins + '']''
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

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[SP_Manage_AdminLogin]') AND OBJECTPROPERTY(id,N'IsProcedure') = 1)
BEGIN
EXEC dbo.sp_executesql @statement = N'








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
	@vcAdminName = ''admin'',
	@vcPassword = ''admin'',
	@vcip =''127.0.0.1'',
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

SET @cIsOnline = ''N''
SET @t_isDel = ''N''

EXEC Sp_Manage_OnlineKickTimeOutAdmin

SELECT @T_name = vcAdminName,@T_pwd=vcPassword,@clock = clock,@cIsOnline=cIsOnline ,
@t_lastIp = vcLastLoginIp,@t_isDel=cIsDel FROM T_Manage_AdminInfo (NOLOCK) WHERE vcAdminName = @vcAdminName

IF(@T_name IS NULL)
BEGIN
	SET  @reValue = -1000000002 --用户不存在
	RETURN
END

IF(@cIsOnline=''Y'' AND @t_lastIp <> @vcip)
BEGIN
	SET @reValue = -1000000005 --管理员已经在线
	RETURN
END

IF(@T_pwd<>@vcPassword)
BEGIN
	SET @reValue = -1000000003 --用户密码错误
	RETURN
END

IF(@clock=''Y'')
BEGIN
	SET @reValue = -1000000004 --用户已经被锁定
	RETURN
END

IF(@t_isDel=''Y'')
BEGIN
	SET @reValue = -1000000018 --您的帐号已经被删除，请联系管理员！
	RETURN;
END

EXEC SP_Manage_OnlineMdy
	@vcAdminName = @vcAdminName,
	@vcIp = @vcip,
	@vcActive = ''登陆后台'',
	@reValue = @reValue

IF(@reValue<0)
BEGIN
	RETURN;
END

UPDATE T_Manage_AdminInfo SET vcLastLoginIp = @vcip,iLoginCount=iLoginCount+1,
dLastLoginDate = GETDATE() WHERE vcAdminName = @vcAdminName

SET @reValue = 1






' 
END
