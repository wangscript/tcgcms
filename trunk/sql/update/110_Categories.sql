--�����SQL��Ҫһ��,һ��ִ�е�

--ɾ��Ψһ����
ALTER TABLE categories DROP CONSTRAINT PK_ClassList
--�¼ӱ����ֶ�
ALTER TABLE categories ADD Id VARCHAR(36)
--��������
UPDATE categories SET Id = iId
--ɾ��iid
ALTER TABLE categories DROP COLUMN iId
--�����۽�����
CREATE CLUSTERED INDEX [PK_Id_Index] ON categories (Id) ON [PRIMARY]

---�ֶ�����������,�һ���֪����ô��SQL��������

ALTER TABLE categories ADD Parent VARCHAR(36) 

UPDATE categories SET Parent = iParent

ALTER TABLE categories DROP COLUMN iParent

ALTER TABLE resources ALTER COLUMN iClassID VARCHAR(36)