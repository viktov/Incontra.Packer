CREATE PROCEDURE [dbo].[Package_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT t.[ID]
	, t.[Requests]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Package] t
	INNER JOIN [dbo].[PackageTranslation] tt ON tt.[PackageID] = t.[ID] AND tt.[LanguageID] = @LanguageID
	WHERE t.[ID] = @ID
END
