CREATE TABLE [dbo].[CountryTranslation] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [CountryID]  INT            NOT NULL,
    [LanguageID] INT            NOT NULL,
    CONSTRAINT [PK_CountryTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CountryTranslation_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([ID]),
    CONSTRAINT [FK_CountryTranslation_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID])
);

