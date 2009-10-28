CREATE PROC SP_UserLogin
(
	@UserName NVARCHAR(50),
	@UserPassWord VARCHAR(32),
	@reValue INT OUTPUT
)
AS

IF(@UserName ='' OR @UserName IS NULL)
BEGIN
	SET @reValue = -1000000101	--用户名不能为空
	RETURN;
END

IF(@UserPassWord = '' OR  @UserPassWord IS NULL)
BEGIN
	SET @reValue = -1000000102  --密码不能为空
	RETURN;
END

DECLARE @t_UserStat INT
SELECT @t_UserStat = STATE FROM [User] (NOLOCK) WHERE [Name] = @UserName

IF(@t_UserStat IS NULL)
BEGIN
	SET @reValue = -1000000103   --该用户名还未注册
	RETURN;
END


SET @reValue = 1
SELECT * FROM [User] (NOLOCK) WHERE [Name] = @UserName
GO