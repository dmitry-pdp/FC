CREATE TABLE [dbo].[ShipPrototypes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Type] INT NOT NULL, 
    [BaseArmor] INT NOT NULL, 
    [BaseShield] INT NOT NULL, 
    [BaseEnergy] INT NOT NULL, 
    [GunSlots] INT NOT NULL, 
    [UtilitySlots] INT NOT NULL, 
    [ComanderSlots] INT NOT NULL, 
    [ComanderRequirements] NVARCHAR(250) NOT NULL, 
    [Resistances] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL
)
