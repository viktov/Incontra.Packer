IF NOT EXISTS (SELECT * FROM [dbo].[ServiceTranslation] WHERE [Name]='Quick Multi Bin Packing')
BEGIN
	exec [dbo].[Service_Insert] 'Quick Multi Bin Packing', 'Quick Multi Bin Packing'
END

IF NOT EXISTS (SELECT * FROM [dbo].[CountryTranslation] WHERE [Name]='Poland')
BEGIN
	exec [dbo].[Country_Insert] 'Poland'
END

IF NOT EXISTS (SELECT * FROM [dbo].[CurrencyTranslation] WHERE [Name]='PLN')
BEGIN
	exec [dbo].[Currency_Insert] 'USD'
	exec [dbo].[Currency_Insert] 'EUR'
	exec [dbo].[Currency_Insert] 'PLN'
END

IF NOT EXISTS (SELECT * FROM [dbo].[Language] WHERE [Name]='English')
BEGIN
	exec [dbo].[Language_Insert] 'EN', 'English'	
END

IF NOT EXISTS (SELECT * FROM [dbo].[Language] WHERE [Name]='Polski')
BEGIN
	exec [dbo].[Language_Insert] 'PL', 'Polski'	
END

IF NOT EXISTS (SELECT * FROM [dbo].[Language] WHERE [Name]='Deustch')
BEGIN
	exec [dbo].[Language_Insert] 'DE', 'Deustch'	
END

IF NOT EXISTS (SELECT * FROM [dbo].[PackageTranslation] WHERE [Name]='Free')
BEGIN
	exec [dbo].[Package_Insert] '100', 'Free', 'Free for up to 100 packing requests'
	DECLARE @PackageID INT = (SELECT TOP 1 [ID] FROM [dbo].[Package] ORDER BY [ID] DESC)


END

