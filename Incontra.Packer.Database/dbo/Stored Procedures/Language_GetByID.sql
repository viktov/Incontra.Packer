CREATE PROCEDURE [dbo].[Language_GetByID]
(
	@ID INT	
)
AS
BEGIN	
	SELECT [ID]
	, [Code]
	, [Name]
	FROM [dbo].[Language]
	WHERE [ID] = @ID	
END
