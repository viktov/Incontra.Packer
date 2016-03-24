CREATE PROCEDURE [dbo].[User_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT [ID],				
		[Login],
		[Password],
		[Email],
		[CompanyName],
		[Street],
		[City],
		[Code],
		[CountryID],
		[RegistrationDate],
		[IsActive]
	FROM [dbo].[User]
	WHERE [ID] = @ID
END
