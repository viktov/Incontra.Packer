CREATE TABLE [dbo].[PeriodTranslation] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [PeriodID]   INT            NOT NULL,
    [LanguageID] INT            NOT NULL,
    CONSTRAINT [PK_PeriodTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PeriodTranslation_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID]),
    CONSTRAINT [FK_PeriodTranslation_PeriodID] FOREIGN KEY ([PeriodID]) REFERENCES [dbo].[Period] ([ID])
);

