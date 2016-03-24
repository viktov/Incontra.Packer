CREATE PROCEDURE [dbo].[Service_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT t.[ID]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Service] t
	INNER JOIN [dbo].[ServiceTranslation] tt ON tt.[ServiceID] = t.[ID] AND tt.[LanguageID] = @LanguageID
	WHERE t.[ID] = @ID
END
