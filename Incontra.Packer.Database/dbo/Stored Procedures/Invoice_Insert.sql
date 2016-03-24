﻿CREATE PROCEDURE [dbo].[Invoice_Insert]
(	
	@UserID INT,
	@CurrencyID INT,
	@LicenseID INT,
	@PaymentID INT,
	@IssueDate DATETIME,
	@DueDate DATETIME,
	@Amount NVARCHAR(4000)	
)
AS
BEGIN		
	INSERT INTO [dbo].[Invoice] (
		[UserID],
		[CurrencyID],
		[LicenseID],
		[PaymentID],
		[IssueDate],
		[DueDate],
		[Amount])
	SELECT
		@UserID,
		@CurrencyID,
		@LicenseID,
		@PaymentID,
		@IssueDate,
		@DueDate,
		@Amount	
END
