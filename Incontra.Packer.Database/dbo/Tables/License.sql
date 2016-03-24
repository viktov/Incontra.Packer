CREATE TABLE [dbo].[License] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [UserID]         INT           NOT NULL,
    [PackagePriceID] INT           NOT NULL,
    [DateStart]      DATETIME      NOT NULL,
    [DateExpire]     DATETIME      NOT NULL,
    [Key]            NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_License_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_License_PackagePriceID] FOREIGN KEY ([PackagePriceID]) REFERENCES [dbo].[PackagePrice] ([ID]),
    CONSTRAINT [FK_License_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

