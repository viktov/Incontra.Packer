CREATE TABLE [dbo].[Package] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [Requests] INT NOT NULL,
    CONSTRAINT [PK_Package_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);

