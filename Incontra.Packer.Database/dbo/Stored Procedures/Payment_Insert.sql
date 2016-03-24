﻿CREATE PROCEDURE [dbo].[Payment_Insert]
(	
	@UserID INT,
	@CurrencyID INT,
	@LicenseID INT,
	@PaymentDate DATETIME,
	@Amount NVARCHAR(4000)	
)
AS
BEGIN		
	INSERT INTO [dbo].[Payment] (
		[UserID],
		[CurrencyID],
		[LicenseID],
		[PaymentDate],
		[Amount])
	SELECT
		@UserID,
		@CurrencyID,
		@LicenseID,
		@PaymentDate,
		@Amount	
END
