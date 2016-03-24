CREATE PROCEDURE [dbo].[Language_Insert]
(
	@Code NVARCHAR(2),
	@Name NVARCHAR(50)
)
AS
BEGIN	
	INSERT INTO [dbo].[Language] ([Code],[Name])
	SELECT @Code, @Name
END
