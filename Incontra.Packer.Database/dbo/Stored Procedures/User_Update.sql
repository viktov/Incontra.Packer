CREATE PROCEDURE [dbo].[User_Update]
(	
	@ID INT,				
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
	UPDATE [dbo].[User]
	SET 				
		[Login] = @Login,
		[Password] = @Password,
		[Email] = @Email,
		[Company] = @Company,
		[Street] = @Street,
		[City] = @City,
		[Code] = @Code,
		[CountryID] = @CountryID,
		[RegistrationDate] = @RegistrationDate,
		[IsActive] = @IsActive
	WHERE [ID] = @ID
END
