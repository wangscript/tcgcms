--�޸ı���
EXEC sp_rename 'T_News_ClassInfo','categories'  

--��ϧ����ֻ����2000����ʹ��
UPDATE b 
SET b.TEXT = REPLACE(b.TEXT,'T_News_ClassInfo','Categories') FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%T_News_ClassInfo%'

--�õ�������Ҫ�޸ĵĴ洢����
SELECT *  FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%T_News_ClassInfo%'

--�޸ı���
EXEC sp_rename 'ResourcesInfo','resources'  

--�õ�������Ҫ�޸ĵĴ洢����
SELECT *  FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%ResourcesInfo%'
