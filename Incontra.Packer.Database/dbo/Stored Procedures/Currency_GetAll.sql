CREATE PROCEDURE [dbo].[Currency_GetAll]
AS
BEGIN	
	SELECT [ID]
	, [Code]	
	FROM [dbo].[Language]	
END
