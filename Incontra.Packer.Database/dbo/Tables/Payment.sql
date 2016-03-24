CREATE TABLE [dbo].[Payment] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [UserID]      INT             NOT NULL,
    [CurrencyID]  INT             NOT NULL,
    [LicenseID]   INT             NOT NULL,
    [PaymentDate] DATETIME        NOT NULL,
    [Amount]      DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Payment_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Payment_CurrencyID] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([ID]),
    CONSTRAINT [FK_Payment_LicenceID] FOREIGN KEY ([LicenseID]) REFERENCES [dbo].[License] ([ID]),
    CONSTRAINT [FK_Payment_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

