CREATE TABLE [dbo].[ServiceTranslation] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (4000) NOT NULL,
    [ServiceID]   INT             NOT NULL,
    [LanguageID]  INT             NOT NULL,
    CONSTRAINT [PK_ServiceTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ServiceTranslation_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[Language] ([ID]),
    CONSTRAINT [FK_ServiceTranslation_ServiceID] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Service] ([ID])
);

