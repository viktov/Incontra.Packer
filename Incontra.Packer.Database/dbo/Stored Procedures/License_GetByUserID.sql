CREATE PROCEDURE [dbo].[License_GetByUserID]
(		
	@UserID INT
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
	WHERE [UserID] = @UserID
END
