CREATE TABLE [dbo].[ModulePrototypes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Effects] NVARCHAR(MAX) NOT NULL, 
    [Requirements] NVARCHAR(MAX) NOT NULL
)
