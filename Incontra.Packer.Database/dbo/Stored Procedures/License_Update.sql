CREATE PROCEDURE [dbo].[License_Update]
(	
	@ID INT,
	@UserID INT,
	@PackagePriceID INT,
	@DateStart DATETIME,
	@DateExpire DATETIME,
	@Key NVARCHAR(50)
)
AS
BEGIN	
	UPDATE [dbo].[License] SET
		[UserID] = @UserID,
		[PackagePriceID] = @PackagePriceID,
		[DateStart] = @DateStart,
		[DateExpire] = @DateExpire,
		[Key] = @Key
	WHERE [ID] = @ID		
END
