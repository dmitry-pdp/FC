CREATE TABLE [dbo].[Leaders]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Intelligence] INT NOT NULL, 
    [Charisma] INT NOT NULL, 
    [Willpower] INT NOT NULL, 
    [ImplantId] UNIQUEIDENTIFIER NULL, 
    [Bonuses] NVARCHAR(MAX) NOT NULL, 
    [ShipSlotNumber] INT NOT NULL, 
    [ShipId] UNIQUEIDENTIFIER NULL, 
    [StationId] UNIQUEIDENTIFIER NULL
)
