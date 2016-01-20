CREATE TABLE [dbo].[LeaderArchetypes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [BaseIntelligence] INT NOT NULL, 
    [BaseCharisma] INT NOT NULL, 
    [BaseWillpower] INT NOT NULL, 
    [Bonuses] NVARCHAR(MAX) NOT NULL 
)
