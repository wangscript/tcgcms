--修改表名
EXEC sp_rename 'T_Manage_AdminInfo','admin'

EXEC sp_rename 'T_Manage_AdminLog','AdminLog'

EXEC sp_rename 'T_Manage_Popedom','Popedom'

EXEC sp_rename 'T_Manage_Online','AdminOnline'

EXEC sp_rename 'T_Manage_RefuseIp','AdminRefuseIp'

EXEC sp_rename 'T_News_VisitorComment','VisitorComment'

EXEC sp_rename 'T_Files_Class','filecategories'

EXEC sp_rename 'T_Files_FileInfos','fileresources'

EXEC sp_rename 'TemplateInfo','Template'

--删除资源来源,实际运用中这个很不实用
DROP TABLE T_News_NewsFromInfo
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_News_NewsFromManage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_News_NewsFromManage]

SELECT * FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%TemplateInfo%'