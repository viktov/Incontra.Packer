CREATE TABLE [dbo].[Currency] (
    [ID]   INT          IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (3) NOT NULL,
    CONSTRAINT [PK_Currency_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);

