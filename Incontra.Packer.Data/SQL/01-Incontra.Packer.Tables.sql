IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Language' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Language] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Language_ID] PRIMARY KEY CLUSTERED ([ID] ASC),				
		[Code] NVARCHAR(2) NOT NULL,
		[Name] NVARCHAR(50) NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Country' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Country] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Country_ID] PRIMARY KEY CLUSTERED ([ID] ASC)		
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CountryTranslation' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[CountryTranslation] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_CountryTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[Name] NVARCHAR(100) NOT NULL,
		[CountryID] INT NOT NULL CONSTRAINT [FK_CountryTranslation_CountryID] FOREIGN KEY (CountryID) REFERENCES [dbo].[Country]([ID]),
		[LanguageID] INT NOT NULL CONSTRAINT [FK_CountryTranslation_LanguageID] FOREIGN KEY (LanguageID) REFERENCES [dbo].[Language]([ID])		
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Currency' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Currency] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Currency_ID] PRIMARY KEY CLUSTERED ([ID] ASC),				
		[Code] NVARCHAR(3) NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Period' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Period] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Period_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[Days] INT NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PeriodTranslation' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[PeriodTranslation] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PeriodTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[Name] NVARCHAR(100) NOT NULL,		
		[PeriodID] INT NOT NULL CONSTRAINT [FK_PeriodTranslation_PeriodID] FOREIGN KEY (PeriodID) REFERENCES [dbo].[Period]([ID]),
		[LanguageID] INT NOT NULL CONSTRAINT [FK_PeriodTranslation_LanguageID] FOREIGN KEY (LanguageID) REFERENCES [dbo].[Language]([ID])
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[User] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_User_ID] PRIMARY KEY CLUSTERED ([ID] ASC),				
		[Login] NVARCHAR(50) NOT NULL,
		[Password] NVARCHAR(50) NOT NULL,
		[Email] NVARCHAR(100) NOT NULL,
		[CompanyName] NVARCHAR(100) NULL,
		[Street] NVARCHAR(100) NULL,
		[City] NVARCHAR(100) NULL,
		[Code] NVARCHAR(100) NULL,
		[CountryID] INT NOT NULL CONSTRAINT [FK_User_CountryID] FOREIGN KEY (CountryID) REFERENCES [dbo].[Country]([ID]),
		[RegistrationDate] DATETIME NOT NULL,
		[IsActive] BIT NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Package' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Package] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Package_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[Requests] INT NOT NULL		
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PackageTranslation' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[PackageTranslation] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PackageTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[Name] NVARCHAR(100) NOT NULL,
		[Description] NVARCHAR(4000) NOT NULL,		
		[PackageID] INT NOT NULL CONSTRAINT [FK_PackageTranslation_PackageID] FOREIGN KEY (PackageID) REFERENCES [dbo].[Package]([ID]),
		[LanguageID] INT NOT NULL CONSTRAINT [FK_PackageTranslation_LanguageID] FOREIGN KEY (LanguageID) REFERENCES [dbo].[Language]([ID])
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PackagePrice' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[PackagePrice] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PackagePrice_ID] PRIMARY KEY CLUSTERED ([ID] ASC),				
		[PackageID] INT NOT NULL CONSTRAINT [FK_PackagePrice_PackageID] FOREIGN KEY (PackageID) REFERENCES [dbo].[Package]([ID]),
		[PeriodID] INT NOT NULL CONSTRAINT [FK_PackagePrice_PeriodID] FOREIGN KEY (PeriodID) REFERENCES [dbo].[Period]([ID]),
		[CurrencyID] INT NOT NULL CONSTRAINT [FK_PackagePrice_CurrencyID] FOREIGN KEY (CurrencyID) REFERENCES [dbo].[Currency]([ID]),
		[PriceTotal] DECIMAL(18,2) NOT NULL,
		[PricePerRequest] DECIMAL(18,2) NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Licence' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Licence] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Licence_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[UserID] INT NOT NULL CONSTRAINT [FK_Licence_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User]([ID]),
		[PackagePriceID] INT NOT NULL CONSTRAINT [FK_Licence_PackagePriceID] FOREIGN KEY (PackagePriceID) REFERENCES [dbo].[PackagePrice]([ID]),
		[DateStart] DATETIME NOT NULL,
		[DateExpire] DATETIME NOT NULL,
		[Key] NVARCHAR(50) NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Service' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Service] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Service_ID] PRIMARY KEY CLUSTERED ([ID] ASC)		
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ServiceTranslation' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[ServiceTranslation] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ServiceTranslation_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[Name] NVARCHAR(100) NOT NULL,
		[Description] NVARCHAR(4000) NOT NULL,
		[ServiceID] INT NOT NULL CONSTRAINT [FK_ServiceTranslation_ServiceID] FOREIGN KEY (ServiceID) REFERENCES [dbo].[Service]([ID]),
		[LanguageID] INT NOT NULL CONSTRAINT [FK_ServiceTranslation_LanguageID] FOREIGN KEY (LanguageID) REFERENCES [dbo].[Language]([ID])
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PackageService' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[PackageService] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LicenceService_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[PackageID] INT NOT NULL CONSTRAINT [FK_PackageService_LicenceID] FOREIGN KEY (PackageID) REFERENCES [dbo].[Package]([ID]),
		[ServiceID] INT NOT NULL CONSTRAINT [FK_LicenceService_AlgorithmID] FOREIGN KEY (ServiceID) REFERENCES [dbo].[Service]([ID])
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRequest' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[UserRequest] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_UserRequest_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[UserID] INT NOT NULL CONSTRAINT [FK_UserRequest_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User]([ID]),		
		[RequestDate] DATETIME NOT NULL,
		[ResponseDate] DATETIME NOT NULL,
		[ResponseTime] DECIMAL(18,2) NOT NULL,
		[CalculationTime] DECIMAL(18,2) NOT NULL,
		[Status] INT NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SystemLog' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[SystemLog] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_SystemLog_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[UserID] INT NULL CONSTRAINT [FK_SystemLog_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User]([ID]),		
		[LogDate] DATETIME NOT NULL,
		[Message] NVARCHAR(4000),		
		[LogType] INT NOT NULL
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payment' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Payment] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Payment_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[UserID] INT NOT NULL CONSTRAINT [FK_Payment_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User]([ID]),
		[CurrencyID] INT NOT NULL CONSTRAINT [FK_Payment_CurrencyID] FOREIGN KEY (CurrencyID) REFERENCES [dbo].[Currency]([ID]),
		[LicenceID] INT NOT NULL CONSTRAINT [FK_Payment_LicenceID] FOREIGN KEY (LicenceID) REFERENCES [dbo].[Licence]([ID]),
		[PaymentDate] DATETIME NOT NULL,
		[Amount] NVARCHAR(4000)		
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Invoice' AND xtype='U')
BEGIN
	CREATE TABLE [dbo].[Invoice] (
		[ID] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Invoice_ID] PRIMARY KEY CLUSTERED ([ID] ASC),		
		[UserID] INT NOT NULL CONSTRAINT [FK_Invoice_UserID] FOREIGN KEY (UserID) REFERENCES [dbo].[User]([ID]),
		[CurrencyID] INT NOT NULL CONSTRAINT [FK_Invoice_CurrencyID] FOREIGN KEY (CurrencyID) REFERENCES [dbo].[Currency]([ID]),
		[LicenceID] INT NOT NULL CONSTRAINT [FK_Invoice_LicenceID] FOREIGN KEY (LicenceID) REFERENCES [dbo].[Licence]([ID]),
		[PaymentID] INT NULL CONSTRAINT [FK_Invoice_PaymentID] FOREIGN KEY (PaymentID) REFERENCES [dbo].[Payment]([ID]),
		[IssueDate] DATETIME NOT NULL,
		[DueDate] DATETIME NOT NULL,
		[Amount] NVARCHAR(4000)		
	) ON [PRIMARY]
END