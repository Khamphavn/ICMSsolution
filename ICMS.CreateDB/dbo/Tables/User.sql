/*
IsActive:   0 = false
            1 = true
*/

CREATE TABLE [dbo].[User]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoginName] NVARCHAR(64) NOT NULL, 
    [FullName] NVARCHAR(128) NOT NULL, 
    [Password] NVARCHAR(512) NOT NULL, 
    [RoleId] INT NOT NULL DEFAULT (1), 
    [IsActive] BIT NOT NULL DEFAULT (0), 

    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([RoleId]),
    CONSTRAINT User_UNIQUE_LoginName UNIQUE([LoginName])
)
