CREATE PROCEDURE [dbo].[Period_Insert]
(	
	@Name NVARCHAR(50),
	@Days INT
)
AS
BEGIN	
	INSERT [dbo].[Period] ([Days]) SELECT @Days
	DECLARE @PeriodID INT = (SELECT SCOPE_IDENTITY())	
		
	INSERT INTO [dbo].[PeriodTranslation] ([LanguageID], [Name], [PeriodID])
	SELECT [ID], @Name, @PeriodID FROM [dbo].[Language]	
	
END
