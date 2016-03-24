CREATE PROCEDURE [dbo].[Country_Insert]
(	
	@Name NVARCHAR(50)
)
AS
BEGIN	
	INSERT [dbo].[Country] DEFAULT VALUES
	DECLARE @CountryID INT = (SELECT SCOPE_IDENTITY())
	
	INSERT INTO [dbo].[CountryTranslation] ([LanguageID], [Name], [CountryID])
	SELECT [ID], @Name, @CountryID FROM [dbo].[Language]	
	
END
