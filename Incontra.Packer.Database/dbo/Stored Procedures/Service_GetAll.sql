CREATE PROCEDURE [dbo].[Service_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Service] t
	INNER JOIN [dbo].[ServiceTranslation] tt ON tt.[ServiceID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
