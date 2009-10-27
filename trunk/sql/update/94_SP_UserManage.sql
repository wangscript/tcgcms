ALTER PROC SP_UserManage
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
	@action CHAR(2) ='01',		--����01����ӡ�02���޸ġ�03��ɾ��
	@reValue INT OUTPUT 		--���ز�����С��0Ϊ�쳣
)
AS
IF(@action='01')
BEGIN
	
	If(@PassWord='')
	BEGIN
		SET @reValue = -1000000090	--���벻��Ϊ�գ�
		RETURN;
	END
	If(@LastLoginIp='')
	BEGIN
		SET @reValue = -1000000091	--����½IP����Ϊ�գ�
		RETURN;
	END
	If(@Name='')
	BEGIN
		SET @reValue = -1000000092	--��������Ϊ�գ�
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
		SET @reValue = -1000000093 --��Ҫ�޸ĵ��û�IDΪ��
		RETURN;
	END

	If(@PassWord='')
	BEGIN
		SET @reValue = -1000000090	--���벻��Ϊ�գ�
		RETURN;
	END
	If(@LastLoginIp='')
	BEGIN
		SET @reValue = -1000000091	--����½IP����Ϊ�գ�
		RETURN;
	END
	If(@Name='')
	BEGIN
		SET @reValue = -1000000092	--��������Ϊ�գ�
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