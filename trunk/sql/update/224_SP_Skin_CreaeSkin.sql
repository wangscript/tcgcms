Create proc SP_Skin_CreateSkin
(
	@Id VARCHAR(36),
	@Name NVARCHAR(50),
	@Pic NVARCHAR(255),
	@Info NVARCHAR(255),
	@reValue INT OUTPUT
)
AS

DECLARE @COUNT INT
SET @COUNT = 0

SELECT @COUNT= COUNT(1) FROM Skin WHERE id = @Id
IF(@COUNT >0)
BEGIN
	 UPDATE Skin SET Id=@Id,[Name]=@Name,Pic=@Pic,Info=@Info WHERE id = @Id
END
ELSE
BEGIN
	INSERT INTO Skin (ID,[NAME],PIC,INFO) VALUES(@Id,@Name,@Pic,@Info)
END

SET @reValue = 1
GO