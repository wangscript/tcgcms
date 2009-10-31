--修改表名
EXEC sp_rename 'T_News_ClassInfo','categories'  

--可惜啊，只有在2000可以使用
UPDATE b 
SET b.TEXT = REPLACE(b.TEXT,'T_News_ClassInfo','Categories') FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%T_News_ClassInfo%'

--得到所有需要修改的存储过程
SELECT *  FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%T_News_ClassInfo%'

--修改表名
EXEC sp_rename 'ResourcesInfo','resources'  

--得到所有需要修改的存储过程
SELECT *  FROM SYSOBJECTS a,SYSCOMMENTS b
WHERE a.id = b.id AND a.[type] = 'P' AND b.TEXT LIKE '%ResourcesInfo%'
