CREATE PROCEDURE [dbo].[User_Insert]
(		
	@Login NVARCHAR(50),
	@Password NVARCHAR(50),
	@Email NVARCHAR(100),
	@Company NVARCHAR(100),
	@Street NVARCHAR(100),
	@City NVARCHAR(100),
	@Code NVARCHAR(100),
	@CountryID INT,
	@RegistrationDate DATETIME,
	@IsActive BIT
)
AS
BEGIN	
	INSERT INTO [dbo].[User] (		
		[Login],
		[Password],
		[Email],
		[Company],
		[Street],
		[City],
		[Code],
		[CountryID],
		[RegistrationDate],
		[IsActive]) 
	SELECT 
		@Login,
		@Password,
		@Email,
		@Company,
		@Street,
		@City,
		@Code,
		@CountryID,
		@RegistrationDate,
		@IsActive
END
