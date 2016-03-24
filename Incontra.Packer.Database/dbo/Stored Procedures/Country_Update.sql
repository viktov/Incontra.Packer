CREATE PROCEDURE [dbo].[Country_Update]
(	
	@ID INT,
	@Name NVARCHAR(50),	
	@LanguageID INT
)
AS
BEGIN
	UPDATE [dbo].[CountryTranslation] 
	SET [Name] = @Name
	WHERE [CountryID] = @ID AND [LanguageID] = @LanguageID	
END
