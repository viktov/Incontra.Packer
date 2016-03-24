CREATE PROCEDURE [dbo].[PackageService_Insert]
(		
	@PackageID INT,
	@ServiceID INT
)
AS
BEGIN	
	INSERT INTO [dbo].[PackageService] (		
	[PackageID],
	[ServiceID]) 
	SELECT 
	@PackageID,
	@ServiceID
END
