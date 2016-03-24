CREATE PROCEDURE [dbo].[Currency_Insert]
(
	@Code NVARCHAR(3)	
)
AS
BEGIN	
	INSERT INTO [dbo].[Currency] ([Code])
	SELECT @Code
END
