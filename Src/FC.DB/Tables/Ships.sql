CREATE TABLE [dbo].[Ships]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FleetId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(250) NOT NULL, 
    [ShipType] UNIQUEIDENTIFIER NOT NULL, 
    [CurrentArmor] INT NOT NULL, 
    [CurrentShield] INT NOT NULL, 
    [CurrentEnergy] INT NOT NULL, 
    [CurrentResistances] NVARCHAR(250) NOT NULL, 
    [AttackValues] NVARCHAR(250) NOT NULL, 
    [LocationX] INT NOT NULL, 
    [LocationY] INT NOT NULL, 
    [MaxArmor] INT NOT NULL, 
    [MaxShield] INT NOT NULL, 
    [MaxEnergy] INT NOT NULL, 
    [Effects] NVARCHAR(250) NOT NULL, 
    [StationId] UNIQUEIDENTIFIER NOT NULL
)
