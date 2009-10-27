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