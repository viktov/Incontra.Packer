CREATE PROCEDURE [dbo].[Invoice_Update]
(	
	@ID INT,
	@UserID INT,
	@CurrencyID INT,
	@LicenseID INT,
	@PaymentID INT,
	@IssueDate DATETIME,
	@DueDate DATETIME,
	@Amount DECIMAL(18,2)
)
AS
BEGIN		
	UPDATE [dbo].[Invoice] SET
		[UserID] = @UserID,
		[CurrencyID] = @CurrencyID,
		[LicenseID] = @LicenseID,
		[PaymentID] = @PaymentID,
		[IssueDate] = @IssueDate,
		[DueDate] = @DueDate,
		[Amount] = @Amount
	WHERE [ID] = @ID
END
