CREATE PROCEDURE [dbo].[PackagePrice_GetByID]
(		
	@ID INT
)
AS
BEGIN	
	SELECT [ID],
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest] 
	FROM [dbo].[PackagePrice]
	WHERE [ID] = @ID
END
