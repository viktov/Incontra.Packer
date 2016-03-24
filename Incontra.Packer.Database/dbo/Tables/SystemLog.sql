CREATE TABLE [dbo].[SystemLog] (
    [ID]      INT             IDENTITY (1, 1) NOT NULL,
    [UserID]  INT             NULL,
    [LogDate] DATETIME        NOT NULL,
    [Message] NVARCHAR (4000) NULL,
    [LogType] INT             NOT NULL,
    CONSTRAINT [PK_SystemLog_ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SystemLog_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);

