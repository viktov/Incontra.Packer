CREATE PROCEDURE [dbo].[User_GetByLoginAndPassword]
(
	@Login NVARCHAR(50),
	@Password NVARCHAR(50)
)
AS
BEGIN
	SELECT [ID],				
		[Login],
		[Password],
		[Email],
		[Company],
		[Street],
		[City],
		[Code],
		[CountryID],
		[RegistrationDate],
		[IsActive]
	FROM [dbo].[User]
	WHERE [Login] = @Login AND [Password] = @Password
END
