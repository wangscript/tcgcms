
DROP TABLE [User]

CREATE TABLE [dbo].[User](
	[Id] [varchar](36) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Name] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PassWord] [varchar](32) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_User_CreateTime]  DEFAULT (getdate()),
	[LastLoginTime] [datetime] NOT NULL CONSTRAINT [DF_User_LastLoginTime]  DEFAULT (getdate()),
	[State] [int] NOT NULL CONSTRAINT [DF_User_State]  DEFAULT ((0)),
	[LastLoginIp] [varchar](32) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[UserLevel] [int] NOT NULL CONSTRAINT [DF_User_UserLevel]  DEFAULT ((0)),
	[UserClubLevel] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF