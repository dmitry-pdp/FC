﻿CREATE TABLE [dbo].[LeaderSkills]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [LeaderId] UNIQUEIDENTIFIER NOT NULL, 
    [SkillId] UNIQUEIDENTIFIER NOT NULL
)
