CREATE PROCEDURE [dbo].[PackagePrice_GetAll]
AS
BEGIN	
	SELECT [ID],
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest] 
	FROM [dbo].[PackagePrice]	
END
