CREATE TABLE [dbo].[Stars]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Class] INT NOT NULL, 
    [Status] INT NOT NULL, 
    [LocationX] FLOAT NOT NULL, 
    [LocationY] FLOAT NOT NULL
)
