CREATE TABLE [dbo].[User] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Login]            NVARCHAR (50)  NOT NULL,
    [Password]         NVARCHAR (50)  NOT NULL,
    [Email]            NVARCHAR (100) NOT NULL,
    [Company]          NVARCHAR (100) NULL,
    [Street]           NVARCHAR (100) NULL,
    [City]             NVARCHAR (100) NULL,
    [Code]             NVARCHAR (100) NULL,
    [CountryID]        INT            NOT NULL,
    [RegistrationDate] DATETIME       NOT NULL,
    [IsActive]         BIT            NOT NULL,
    CONSTRAINT [PK_User_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([ID])
);



