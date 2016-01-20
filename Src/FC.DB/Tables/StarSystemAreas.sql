﻿CREATE TABLE [dbo].[StarSystemAreas]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PlanetId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(250) NOT NULL, 
    [Type] INT NOT NULL, 
    [Number] INT NOT NULL, 
    [DropTableId] UNIQUEIDENTIFIER NOT NULL
)