CREATE PROCEDURE [dbo].[PackagePrice_GetByPackageID]
(		
	@PackageID INT,
	@CurrencyID INT
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
	WHERE [PackageID] = @PackageID AND [CurrencyID] = @CurrencyID
END
