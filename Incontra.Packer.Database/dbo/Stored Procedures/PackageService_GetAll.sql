CREATE PROCEDURE [dbo].[PackageService_GetAll]
AS
BEGIN	
	SELECT [ID],		
		[PackageID],
		[ServiceID]
	FROM [dbo].[PackageService]	
END
