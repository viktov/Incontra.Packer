CREATE TABLE [dbo].[Language] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Code] NVARCHAR (2)  NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Language_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);

