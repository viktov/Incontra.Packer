CREATE PROCEDURE [dbo].[License_Insert]
(	
	@UserID INT,
	@PackagePriceID INT,
	@DateStart DATETIME,
	@DateExpire DATETIME,
	@Key NVARCHAR(50)
)
AS
BEGIN	
	INSERT INTO [dbo].[License] (			
		[UserID],
		[PackagePriceID],
		[DateStart],
		[DateExpire],
		[Key]) 
	SELECT 	
		@UserID,
		@PackagePriceID,
		@DateStart,
		@DateExpire,
		@Key
END
