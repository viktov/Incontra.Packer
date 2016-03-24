CREATE PROCEDURE [dbo].[Currency_GetByID]
(
	@ID INT	
)
AS
BEGIN	
	SELECT [ID]
	, [Code]	
	FROM [dbo].[Currency]
	WHERE [ID] = @ID	
END
