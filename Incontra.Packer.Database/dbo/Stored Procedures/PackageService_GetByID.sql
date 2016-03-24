CREATE PROCEDURE [dbo].[PackageService_GetByID]
(		
	@ID INT	
)
AS
BEGIN	
	SELECT [ID],		
		[PackageID],
		[ServiceID]
	FROM [dbo].[PackageService]	
	WHERE [ID] = @ID
END
