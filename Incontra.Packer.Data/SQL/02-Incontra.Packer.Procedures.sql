IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Language_Insert')
	DROP PROCEDURE [dbo].[Language_Insert]
GO
CREATE PROCEDURE [dbo].[Language_Insert]
(
	@Code NVARCHAR(2),
	@Name NVARCHAR(50)
)
AS
BEGIN	
	INSERT INTO [dbo].[Language] ([Code],[Name])
	SELECT @Code, @Name
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Language_GetByID')
	DROP PROCEDURE [dbo].[Language_GetByID]
GO
CREATE PROCEDURE [dbo].[Language_GetByID]
(
	@ID INT	
)
AS
BEGIN	
	SELECT [ID]
	, [Code]
	, [Name]
	FROM [dbo].[Language]
	WHERE [ID] = @ID	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Language_GetAll')
	DROP PROCEDURE [dbo].[Language_GetAll]
GO
CREATE PROCEDURE [dbo].[Language_GetAll]
AS
BEGIN	
	SELECT [ID]
	, [Code]
	, [Name]
	FROM [dbo].[Language]	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Currency_Insert')
	DROP PROCEDURE [dbo].[Currency_Insert]
GO
CREATE PROCEDURE [dbo].[Currency_Insert]
(
	@Code NVARCHAR(3)	
)
AS
BEGIN	
	INSERT INTO [dbo].[Currency] ([Code])
	SELECT @Code
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Currency_GetByID')
	DROP PROCEDURE [dbo].[Currency_GetByID]
GO
CREATE PROCEDURE [dbo].[Currency_GetByID]
(
	@ID INT	
)
AS
BEGIN	
	SELECT [ID]
	, [Code]	
	FROM [dbo].[Currency]
	WHERE [ID] = @ID	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Currency_GetAll')
	DROP PROCEDURE [dbo].[Currency_GetAll]
GO
CREATE PROCEDURE [dbo].[Currency_GetAll]
AS
BEGIN	
	SELECT [ID]
	, [Code]	
	FROM [dbo].[Language]	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Country_Insert')
	DROP PROCEDURE [dbo].[Country_Insert]
GO
CREATE PROCEDURE [dbo].[Country_Insert]
(	
	@Name NVARCHAR(50)
)
AS
BEGIN	
	INSERT [dbo].[Country] DEFAULT VALUES
	DECLARE @CountryID INT = (SELECT SCOPE_IDENTITY())
	
	INSERT INTO [dbo].[CountryTranslation] ([LanguageID], [Name], [CountryID])
	SELECT [ID], @Name, @CountryID FROM [dbo].[Language]	
	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Country_Update')
	DROP PROCEDURE [dbo].[Country_Update]
GO
CREATE PROCEDURE [dbo].[Country_Update]
(	
	@ID INT,
	@Name NVARCHAR(50),	
	@LanguageID INT
)
AS
BEGIN
	UPDATE [dbo].[CountryTranslation] 
	SET [Name] = @Name
	WHERE [CountryID] = @ID AND [LanguageID] = @LanguageID	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Country_GetByID')
	DROP PROCEDURE [dbo].[Country_GetByID]
GO
CREATE PROCEDURE [dbo].[Country_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT t.[ID]
	, tt.[Name]
	FROM [dbo].[Country] t
	INNER JOIN [dbo].[CountryTranslation] tt ON tt.[CountryID] = t.[ID] AND tt.[LanguageID] = @LanguageID
	WHERE t.[ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Country_GetAll')
	DROP PROCEDURE [dbo].[Country_GetAll]
GO
CREATE PROCEDURE [dbo].[Country_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]
	, tt.[Name]
	FROM [dbo].[Country] t
	INNER JOIN [dbo].[CountryTranslation] tt ON tt.[CountryID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'UserRequest_Insert')
	DROP PROCEDURE [dbo].[UserRequest_Insert]
GO
CREATE PROCEDURE [dbo].[UserRequest_Insert]
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
	INSERT INTO [dbo].[UserRequest] (
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
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SystemLog_Insert')
	DROP PROCEDURE [dbo].[SystemLog_Insert]
GO
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
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Period_Insert')
	DROP PROCEDURE [dbo].[Period_Insert]
GO
CREATE PROCEDURE [dbo].[Period_Insert]
(	
	@Name NVARCHAR(50),
	@Days INT
)
AS
BEGIN	
	INSERT [dbo].[Period] ([Days]) SELECT @Days
	DECLARE @PeriodID INT = (SELECT SCOPE_IDENTITY())	
		
	INSERT INTO [dbo].[PeriodTranslation] ([LanguageID], [Name], [PeriodID])
	SELECT [ID], @Name, @PeriodID FROM [dbo].[Language]	
	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Period_Update')
	DROP PROCEDURE [dbo].[Period_Update]
GO
CREATE PROCEDURE [dbo].[Period_Update]
(	
	@ID INT,
	@Name NVARCHAR(50),
	@Days INT,
	@LanguageID INT
)
AS
BEGIN
	UPDATE [dbo].[Period]
	SET [Days] = @Days
	WHERE [ID] = @ID

	UPDATE [dbo].[PeriodTranslation] 
	SET [Name] = @Name
	WHERE [PeriodID] = @ID AND [LanguageID] = @LanguageID	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Period_GetByID')
	DROP PROCEDURE [dbo].[Period_GetByID]
GO
CREATE PROCEDURE [dbo].[Period_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT t.[ID]
	, t.[Days]
	, tt.[Name]
	FROM [dbo].[Period] t
	INNER JOIN [dbo].[PeriodTranslation] tt ON tt.[PeriodID] = t.[ID] AND tt.[LanguageID] = @LanguageID
	WHERE t.[ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Period_GetAll')
	DROP PROCEDURE [dbo].[Period_GetAll]
GO
CREATE PROCEDURE [dbo].[Period_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]
	, t.[Days]
	, tt.[Name]
	FROM [dbo].[Period] t
	INNER JOIN [dbo].[PeriodTranslation] tt ON tt.[PeriodID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'User_Insert')
	DROP PROCEDURE [dbo].[User_Insert]
GO
CREATE PROCEDURE [dbo].[User_Insert]
(		
	@Login NVARCHAR(50),
	@Password NVARCHAR(50),
	@Email NVARCHAR(100),
	@CompanyName NVARCHAR(100),
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
		[CompanyName],
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
		@CompanyName,
		@Street,
		@City,
		@Code,
		@CountryID,
		@RegistrationDate,
		@IsActive
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'User_Update')
	DROP PROCEDURE [dbo].[User_Update]
GO
CREATE PROCEDURE [dbo].[User_Update]
(	
	@ID INT,				
	@Login NVARCHAR(50),
	@Password NVARCHAR(50),
	@Email NVARCHAR(100),
	@CompanyName NVARCHAR(100),
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
		[CompanyName] = @CompanyName,
		[Street] = @Street,
		[City] = @City,
		[Code] = @Code,
		[CountryID] = @CountryID,
		[RegistrationDate] = @RegistrationDate,
		[IsActive] = @IsActive
	WHERE [ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'User_GetByID')
	DROP PROCEDURE [dbo].[User_GetByID]
GO
CREATE PROCEDURE [dbo].[User_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT [ID],				
		[Login],
		[Password],
		[Email],
		[CompanyName],
		[Street],
		[City],
		[Code],
		[CountryID],
		[RegistrationDate],
		[IsActive]
	FROM [dbo].[User]
	WHERE [ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Package_Insert')
	DROP PROCEDURE [dbo].[Package_Insert]
GO
CREATE PROCEDURE [dbo].[Package_Insert]
(	
	@Requests INT,
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000)
)
AS
BEGIN	
	INSERT [dbo].[Package] ([Requests]) SELECT @Requests
	DECLARE @PackageID INT = (SELECT SCOPE_IDENTITY())	
		
	INSERT INTO [dbo].[PackageTranslation] ([LanguageID], [Name], [Description], [PackageID])
	SELECT [ID], @Name, @Description, @PackageID FROM [dbo].[Language]	
	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Package_Update')
	DROP PROCEDURE [dbo].[Package_Update]
GO
CREATE PROCEDURE [dbo].[Package_Update]
(	
	@ID INT,
	@Requests INT,
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000),
	@LanguageID INT
)
AS
BEGIN	
	UPDATE [dbo].[Package]
	SET [Requests] = @Requests
	WHERE [ID] = @ID

	UPDATE [dbo].[PackageTranslation] 
	SET [Name] = @Name,
	[Description] = @Description
	WHERE [PackageID] = @ID AND [LanguageID] = @LanguageID	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Package_GetByID')
	DROP PROCEDURE [dbo].[Package_GetByID]
GO
CREATE PROCEDURE [dbo].[Package_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT t.[ID]
	, t.[Requests]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Package] t
	INNER JOIN [dbo].[PackageTranslation] tt ON tt.[PackageID] = t.[ID] AND tt.[LanguageID] = @LanguageID
	WHERE t.[ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Package_GetAll')
	DROP PROCEDURE [dbo].[Package_GetAll]
GO
CREATE PROCEDURE [dbo].[Package_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]
	, t.[Requests]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Package] t
	INNER JOIN [dbo].[PackageTranslation] tt ON tt.[PackageID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackagePrice_Insert')
	DROP PROCEDURE [dbo].[PackagePrice_Insert]
GO
CREATE PROCEDURE [dbo].[PackagePrice_Insert]
(		
	@PackageID INT,
	@PeriodID INT,
	@CurrencyID INT,
	@PriceTotal DECIMAL(18,2),
	@PricePerRequest DECIMAL(18,2)
)
AS
BEGIN	
	INSERT INTO [dbo].[PackagePrice] (		
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest]) 
	SELECT 
	@PackageID,
	@PeriodID,
	@CurrencyID,
	@PriceTotal,
	@PricePerRequest
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackagePrice_GetByID')
	DROP PROCEDURE [dbo].[PackagePrice_GetByID]
GO
CREATE PROCEDURE [dbo].[PackagePrice_GetByID]
(		
	@ID INT
)
AS
BEGIN	
	SELECT [ID],
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest] 
	FROM [dbo].[PackagePrice]
	WHERE [ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackagePrice_GetAll')
	DROP PROCEDURE [dbo].[PackagePrice_GetAll]
GO
CREATE PROCEDURE [dbo].[PackagePrice_GetAll]
AS
BEGIN	
	SELECT [ID],
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest] 
	FROM [dbo].[PackagePrice]	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackagePrice_GetByPackageID')
	DROP PROCEDURE [dbo].[PackagePrice_GetByPackageID]
GO
CREATE PROCEDURE [dbo].[PackagePrice_GetByPackageID]
(		
	@PackageID INT,
	@CurrencyID INT
)
AS
BEGIN	
	SELECT [ID],
	[PackageID],
	[PeriodID],
	[CurrencyID],
	[PriceTotal],
	[PricePerRequest] 
	FROM [dbo].[LicencePrice]
	WHERE [PackageID] = @PackageID AND [CurrencyID] = @CurrencyID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Licence_Insert')
	DROP PROCEDURE [dbo].[Licence_Insert]
GO
CREATE PROCEDURE [dbo].[Licence_Insert]
(	
	@UserID INT,
	@PackagePriceID INT,
	@DateStart DATETIME,
	@DateExpire DATETIME,
	@Key NVARCHAR(50)
)
AS
BEGIN	
	INSERT INTO [dbo].[Licence] (			
		[UserID],
		[PackagePriceID],
		[DateStart],
		[DateExpire],
		[Key]) 
	SELECT 	
		@UserID,
		@PackagePriceID,
		@DateStart,
		@DateExpire,
		@Key
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Licence_Update')
	DROP PROCEDURE [dbo].[Licence_Update]
GO
CREATE PROCEDURE [dbo].[Licence_Update]
(	
	@ID INT,
	@UserID INT,
	@PackagePriceID INT,
	@DateStart DATETIME,
	@DateExpire DATETIME,
	@Key NVARCHAR(50)
)
AS
BEGIN	
	UPDATE [dbo].[Licence] SET
		[UserID] = @UserID,
		[PackagePriceID] = @PackagePriceID,
		[DateStart] = @DateStart,
		[DateExpire] = @DateExpire,
		[Key] = @Key
	WHERE [ID] = @ID		
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Licence_GetByID')
	DROP PROCEDURE [dbo].[Licence_GetByID]
GO
CREATE PROCEDURE [dbo].[Licence_GetByID]
(		
	@ID INT
)
AS
BEGIN	
	SELECT 
		[ID],		
		[UserID],
		[PackagePriceID],
		[DateStart],
		[DateExpire],
		[Key]
	FROM [dbo].[Licence]
	WHERE [ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Licence_GetByUserID')
	DROP PROCEDURE [dbo].[Licence_GetByUserID]
GO
CREATE PROCEDURE [dbo].[Licence_GetByUserID]
(		
	@UserID INT
)
AS
BEGIN	
	SELECT 
		[ID],		
		[UserID],
		[PackagePriceID],
		[DateStart],
		[DateExpire],
		[Key]
	FROM [dbo].[Licence]
	WHERE [UserID] = @UserID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Licence_GetAll')
	DROP PROCEDURE [dbo].[Licence_GetAll]
GO
CREATE PROCEDURE [dbo].[Licence_GetAll]
AS
BEGIN	
	SELECT 
		[ID],		
		[UserID],
		[PackagePriceID],
		[DateStart],
		[DateExpire],
		[Key]
	FROM [dbo].[Licence]
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Service_Insert')
	DROP PROCEDURE [dbo].[Service_Insert]
GO
CREATE PROCEDURE [dbo].[Service_Insert]
(	
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000)
)
AS
BEGIN	
	INSERT [dbo].[Service] DEFAULT VALUES
	DECLARE @ServiceID INT = (SELECT SCOPE_IDENTITY())	
		
	INSERT INTO [dbo].[ServiceTranslation] ([LanguageID], [Name], [Description], [ServiceID])
	SELECT [ID], @Name, @Description, @ServiceID FROM [dbo].[Language]	
	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Service_Update')
	DROP PROCEDURE [dbo].[Service_Update]
GO
CREATE PROCEDURE [dbo].[Service_Update]
(	
	@ID INT,	
	@Name NVARCHAR(50),
	@Description NVARCHAR(4000),
	@LanguageID INT
)
AS
BEGIN
	UPDATE [dbo].[ServiceTranslation] 
	SET [Name] = @Name,
	[Description] = @Description
	WHERE [ServiceID] = @ID AND [LanguageID] = @LanguageID	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Service_GetByID')
	DROP PROCEDURE [dbo].[Service_GetByID]
GO
CREATE PROCEDURE [dbo].[Service_GetByID]
(
	@ID INT,
	@LanguageID INT
)
AS
BEGIN
	SELECT t.[ID]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Service] t
	INNER JOIN [dbo].[ServiceTranslation] tt ON tt.[ServiceID] = t.[ID] AND tt.[LanguageID] = @LanguageID
	WHERE t.[ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Service_GetAll')
	DROP PROCEDURE [dbo].[Service_GetAll]
GO
CREATE PROCEDURE [dbo].[Service_GetAll]
(
	@LanguageID INT
)
AS
BEGIN	
	SELECT t.[ID]	
	, tt.[Name]
	, tt.[Description]
	FROM [dbo].[Algorithm] t
	INNER JOIN [dbo].[AlgorithmTranslation] tt ON tt.[AlgorithmID] = t.[ID] AND tt.[LanguageID] = @LanguageID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackageService_Insert')
	DROP PROCEDURE [dbo].[PackageService_Insert]
GO
CREATE PROCEDURE [dbo].[PackageService_Insert]
(		
	@PackageID INT,
	@ServiceID INT
)
AS
BEGIN	
	INSERT INTO [dbo].[PackageService] (		
	[PackageID],
	[ServiceID]) 
	SELECT 
	@PackageID,
	@ServiceID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackageService_GetByID')
	DROP PROCEDURE [dbo].[PackageService_GetByID]
GO
CREATE PROCEDURE [dbo].[PackageService_GetByID]
(		
	@ID INT	
)
AS
BEGIN	
	SELECT [ID],		
		[PackageID],
		[ServiceID]
	FROM [dbo].[PackageService]	
	WHERE [ID] = @ID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackageService_GetAll')
	DROP PROCEDURE [dbo].[PackageService_GetAll]
GO
CREATE PROCEDURE [dbo].[PackageService_GetAll]
AS
BEGIN	
	SELECT [ID],		
		[PackageID],
		[ServiceID]
	FROM [dbo].[PackageService]	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PackageService_GetByPackageID')
	DROP PROCEDURE [dbo].[PackageService_GetByPackageID]
GO
CREATE PROCEDURE [dbo].[PackageService_GetByPackageID]
(		
	@PackageID INT	
)
AS
BEGIN	
	SELECT [ID],		
		[PackageID],
		[ServiceID]
	FROM [dbo].[PackageService]
	WHERE [PackageID] = @PackageID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Payment_Insert')
	DROP PROCEDURE [dbo].[Payment_Insert]
GO
CREATE PROCEDURE [dbo].[Payment_Insert]
(	
	@UserID INT,
	@CurrencyID INT,
	@LicenceID INT,
	@PaymentDate DATETIME,
	@Amount NVARCHAR(4000)	
)
AS
BEGIN		
	INSERT INTO [dbo].[Payment] (
		[UserID],
		[CurrencyID],
		[LicenceID],
		[PaymentDate],
		[Amount])
	SELECT
		@UserID,
		@CurrencyID,
		@LicenceID,
		@PaymentDate,
		@Amount	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Invoice_Insert')
	DROP PROCEDURE [dbo].[Invoice_Insert]
GO
CREATE PROCEDURE [dbo].[Invoice_Insert]
(	
	@UserID INT,
	@CurrencyID INT,
	@LicenceID INT,
	@PaymentID INT,
	@IssueDate DATETIME,
	@DueDate DATETIME,
	@Amount NVARCHAR(4000)	
)
AS
BEGIN		
	INSERT INTO [dbo].[Invoice] (
		[UserID],
		[CurrencyID],
		[LicenceID],
		[PaymentID],
		[IssueDate],
		[DueDate],
		[Amount])
	SELECT
		@UserID,
		@CurrencyID,
		@LicenceID,
		@PaymentID,
		@IssueDate,
		@DueDate,
		@Amount	
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Invoice_Update')
	DROP PROCEDURE [dbo].[Invoice_Update]
GO
CREATE PROCEDURE [dbo].[Invoice_Update]
(	
	@ID INT,
	@UserID INT,
	@CurrencyID INT,
	@LicenceID INT,
	@PaymentID INT,
	@IssueDate DATETIME,
	@DueDate DATETIME,
	@Amount NVARCHAR(4000)	
)
AS
BEGIN		
	UPDATE [dbo].[Invoice] SET
		[UserID] = @UserID,
		[CurrencyID] = @CurrencyID,
		[LicenceID] = @LicenceID,
		[PaymentID] = @PaymentID,
		[IssueDate] = @IssueDate,
		[DueDate] = @DueDate,
		[Amount] = @Amount
	WHERE [ID] = @ID
END
GO