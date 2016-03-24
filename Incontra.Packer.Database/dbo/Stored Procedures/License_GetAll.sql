CREATE PROCEDURE [dbo].[License_GetAll]
AS
BEGIN	
	SELECT 
		[ID],		
		[UserID],
		[PackagePriceID],
		[DateStart],
		[DateExpire],
		[Key]
	FROM [dbo].[License]
END
