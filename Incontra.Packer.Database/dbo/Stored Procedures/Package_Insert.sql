CREATE PROCEDURE [dbo].[Package_Insert]
(	
	@Requests INT,
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000)
)
AS
BEGIN	
	INSERT [dbo].[Package] ([Requests]) SELECT @Requests
	DECLARE @PackageID INT = (SELECT SCOPE_IDENTITY())	
		
	INSERT INTO [dbo].[PackageTranslation] ([LanguageID], [Name], [Description], [PackageID])
	SELECT [ID], @Name, @Description, @PackageID FROM [dbo].[Language]	
	
END
