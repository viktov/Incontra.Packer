CREATE PROCEDURE [dbo].[PackagePrice_Insert]
(		
	@PackageID INT,
	@PeriodID INT,
	@CurrencyID INT,
	@PriceTotal DECIMAL(18,2),
	@PricePerRequest DECIMAL(18,2)
)
AS
BEGIN	
	INSERT INTO [dbo].[PackagePrice] (		
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest]) 
	SELECT 
	@PackageID,
	@PeriodID,
	@CurrencyID,
	@PriceTotal,
	@PricePerRequest
END
