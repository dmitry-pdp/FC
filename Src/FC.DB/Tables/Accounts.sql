CREATE TABLE [dbo].[Accounts]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Email] NVARCHAR(250) NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [Status] INT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [LastLoggedOn] DATETIME NOT NULL, 
    [LastLoginIp] NVARCHAR(50) NOT NULL, 
    [Type] INT NOT NULL, 
    [Money] BIGINT NOT NULL
)
