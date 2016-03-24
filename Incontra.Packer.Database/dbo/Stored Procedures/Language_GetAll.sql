CREATE PROCEDURE [dbo].[Language_GetAll]
AS
BEGIN	
	SELECT [ID]
	, [Code]
	, [Name]
	FROM [dbo].[Language]	
END
