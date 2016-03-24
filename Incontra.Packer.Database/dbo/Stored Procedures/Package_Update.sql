CREATE PROCEDURE [dbo].[Package_Update]
(	
	@ID INT,
	@Requests INT,
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000),
	@LanguageID INT
)
AS
BEGIN	
	UPDATE [dbo].[Package]
	SET [Requests] = @Requests
	WHERE [ID] = @ID

	UPDATE [dbo].[PackageTranslation] 
	SET [Name] = @Name,
	[Description] = @Description
	WHERE [PackageID] = @ID AND [LanguageID] = @LanguageID	
END
