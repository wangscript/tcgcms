CREATE FUNCTION [dbo].[GetNewsFilePath]
(
	@ClassFilePath VARCHAR(200),
	@dAddDate DATETIME,
	@vcExtension VARCHAR(6),
	@iID INT
)
RETURNS VARCHAR(255)
AS
BEGIN

DECLARE @t_Patch VARCHAR(255)

SET @t_Patch=@ClassFilePath
SET @t_Patch = @t_Patch + CAST(YEAR(@dAddDate) AS VARCHAR) + '/'
SET @t_Patch = @t_Patch + dbo.AddZreo(CAST(Month(@dAddDate) AS VARCHAR),2)
SET @t_Patch = @t_Patch + dbo.AddZreo(CAST(DAY(@dAddDate) AS VARCHAR),2) + '/'
SET @t_Patch = @t_Patch + dbo.AddZreo(CAST(@iID AS VARCHAR),10)
SET @t_Patch = @t_Patch + @vcExtension
RETURN @t_Patch
END