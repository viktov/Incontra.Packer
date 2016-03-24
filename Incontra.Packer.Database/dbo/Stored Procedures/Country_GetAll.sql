CREATE PROCEDURE [dbo].[Country_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]
	, tt.[Name]
	FROM [dbo].[Country] t
	INNER JOIN [dbo].[CountryTranslation] tt ON tt.[CountryID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
