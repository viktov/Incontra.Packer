CREATE PROCEDURE [dbo].[Service_Insert]
(	
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000)
)
AS
BEGIN	
	INSERT [dbo].[Service] DEFAULT VALUES
	DECLARE @ServiceID INT = (SELECT SCOPE_IDENTITY())	
		
	INSERT INTO [dbo].[ServiceTranslation] ([LanguageID], [Name], [Description], [ServiceID])
	SELECT [ID], @Name, @Description, @ServiceID FROM [dbo].[Language]	
	
END
