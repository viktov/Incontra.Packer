CREATE PROCEDURE [dbo].[License_GetByID]
(		
	@ID INT
)
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
	WHERE [ID] = @ID
END
