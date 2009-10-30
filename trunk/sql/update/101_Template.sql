

--修改表名
EXEC sp_rename 'T_Template_TemplateInfo','TemplateInfo'  

--模板编号修改字段名和属性 
EXEC sp_rename 'TemplateInfo.iId','id'
Alter table [TemplateInfo] Alter COLUMN id  varchar(36)

--修改模板编号
EXEC sp_rename 'TemplateInfo.iSiteId','SkinId'
Alter table [TemplateInfo] Alter COLUMN SkinId  varchar(36)

--修改模板类型
EXEC sp_rename 'TemplateInfo.iType','TemplateType'

EXEC sp_rename 'T_News_NewsInfo','ResourcesInfo'  
Alter table Resources Alter COLUMN iID  varchar(36)