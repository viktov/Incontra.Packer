CREATE TABLE [dbo].[UserRequest] (
    [ID]              INT             IDENTITY (1, 1) NOT NULL,
    [UserID]          INT             NOT NULL,
    [RequestDate]     DATETIME        NOT NULL,
    [ResponseDate]    DATETIME        NOT NULL,
    [ResponseTime]    DECIMAL (18, 2) NOT NULL,
    [CalculationTime] DECIMAL (18, 2) NOT NULL,
    [Status]          INT             NOT NULL,
    CONSTRAINT [PK_UserRequest_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserRequest_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

