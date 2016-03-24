CREATE TABLE [dbo].[PackageService] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [PackageID] INT NOT NULL,
    [ServiceID] INT NOT NULL,
    CONSTRAINT [PK_LicenceService_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_LicenceService_AlgorithmID] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Service] ([ID]),
    CONSTRAINT [FK_PackageService_LicenceID] FOREIGN KEY ([PackageID]) REFERENCES [dbo].[Package] ([ID])
);

