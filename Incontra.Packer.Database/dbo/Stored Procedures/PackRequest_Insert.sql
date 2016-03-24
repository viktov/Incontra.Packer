CREATE PROCEDURE [dbo].[PackRequest_Insert]
(
	@UserID INT,		
	@RequestDate DATETIME,
	@ResponseDate DATETIME,
	@ResponseTime DECIMAL(18,2),
	@CalculationTime DECIMAL(18,2),
	@Status INT
)
AS
BEGIN	
	INSERT INTO [dbo].[PackRequest] (
	[UserID],		
	[RequestDate],
	[ResponseDate],
	[ResponseTime],
	[CalculationTime],
	[Status])
	SELECT @UserID,		
	@RequestDate,
	@ResponseDate,
	@ResponseTime,
	@CalculationTime,
	@Status 
END