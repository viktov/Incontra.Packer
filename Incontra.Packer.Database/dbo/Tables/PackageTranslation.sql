CREATE TABLE [dbo].[PackageTranslation] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (4000) NOT NULL,
    [PackageID]   INT             NOT NULL,
    [LanguageID]  INT             NOT NULL,
    CONSTRAINT [PK_PackageTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PackageTranslation_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID]),
    CONSTRAINT [FK_PackageTranslation_PackageID] FOREIGN KEY ([PackageID]) REFERENCES [dbo].[Package] ([ID])
);

