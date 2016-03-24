CREATE PROCEDURE [dbo].[Period_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]
	, t.[Days]
	, tt.[Name]
	FROM [dbo].[Period] t
	INNER JOIN [dbo].[PeriodTranslation] tt ON tt.[PeriodID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
