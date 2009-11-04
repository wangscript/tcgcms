--这里的SQL是要一条,一条执行的

--删除唯一索引
ALTER TABLE categories DROP CONSTRAINT PK_ClassList
--新加备份字段
ALTER TABLE categories ADD Id VARCHAR(36)
--备份数据
UPDATE categories SET Id = iId
--删除iid
ALTER TABLE categories DROP COLUMN iId
--建立聚焦索引
CREATE CLUSTERED INDEX [PK_Id_Index] ON categories (Id) ON [PRIMARY]

---手动设置下主键,我还不知道怎么用SQL设置主键

ALTER TABLE categories ADD Parent VARCHAR(36) 

UPDATE categories SET Parent = iParent

ALTER TABLE categories DROP COLUMN iParent

ALTER TABLE resources ALTER COLUMN iClassID VARCHAR(36)