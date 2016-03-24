CREATE PROCEDURE [dbo].[Service_Update]
(	
	@ID INT,	
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000),
	@LanguageID INT
)
AS
BEGIN
	UPDATE [dbo].[ServiceTranslation] 
	SET [Name] = @Name,
	[Description] = @Description
	WHERE [ServiceID] = @ID AND [LanguageID] = @LanguageID	
END
