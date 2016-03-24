CREATE TABLE [dbo].[PackagePrice] (
    [ID]              INT             IDENTITY (1, 1) NOT NULL,
    [PackageID]       INT             NOT NULL,
    [PeriodID]        INT             NOT NULL,
    [CurrencyID]      INT             NOT NULL,
    [PriceTotal]      DECIMAL (18, 2) NOT NULL,
    [PricePerRequest] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_PackagePrice_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PackagePrice_CurrencyID] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[Currency] ([ID]),
    CONSTRAINT [FK_PackagePrice_PackageID] FOREIGN KEY ([PackageID]) REFERENCES [dbo].[Package] ([ID]),
    CONSTRAINT [FK_PackagePrice_PeriodID] FOREIGN KEY ([PeriodID]) REFERENCES [dbo].[Period] ([ID])
);

