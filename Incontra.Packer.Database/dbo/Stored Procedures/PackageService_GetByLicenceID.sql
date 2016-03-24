CREATE PROCEDURE [dbo].[PackageService_GetByLicenceID]
(		
	@PackageID INT	
)
AS
BEGIN	
	SELECT [ID],		
		[PackageID],
		[ServiceID]
	FROM [dbo].[PackageService]
	WHERE [PackageID] = @PackageID
END
