CREATE PROCEDURE [dbo].[Period_Update]
(	
	@ID INT,
	@Name NVARCHAR(50),
	@Days INT,
	@LanguageID INT
)
AS
BEGIN
	UPDATE [dbo].[Period]
	SET [Days] = @Days
	WHERE [ID] = @ID

	UPDATE [dbo].[PeriodTranslation] 
	SET [Name] = @Name
	WHERE [PeriodID] = @ID AND [LanguageID] = @LanguageID	
END
