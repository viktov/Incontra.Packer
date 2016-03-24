CREATE PROCEDURE [dbo].[SystemLog_Insert]
(
	@UserID INT,		
	@LogDate DATETIME,
	@Message NVARCHAR,		
	@LogType INT
)
AS
BEGIN	
	INSERT INTO [dbo].[SystemLog] (
	[UserID],		
	[LogDate],
	[Message],		
	[LogType])
	SELECT
	@UserID,		
	@LogDate,
	@Message,		
	@LogType
END
