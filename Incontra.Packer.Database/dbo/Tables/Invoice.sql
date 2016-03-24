CREATE TABLE [dbo].[Invoice] (
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    [UserID]     INT             NOT NULL,
    [CurrencyID] INT             NOT NULL,
    [LicenseID]  INT             NOT NULL,
    [PaymentID]  INT             NULL,
    [IssueDate]  DATETIME        NOT NULL,
    [DueDate]    DATETIME        NOT NULL,
    [Amount]     NVARCHAR (4000) NULL,
    CONSTRAINT [PK_Invoice_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Invoice_CurrencyID] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([ID]),
    CONSTRAINT [FK_Invoice_LicenseID] FOREIGN KEY ([LicenseID]) REFERENCES [dbo].[License] ([ID]),
    CONSTRAINT [FK_Invoice_PaymentID] FOREIGN KEY ([PaymentID]) REFERENCES [dbo].[Payment] ([ID]),
    CONSTRAINT [FK_Invoice_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

