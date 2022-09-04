﻿CREATE TABLE [dbo].[Role]
(
	[RoleId] INT  IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL,
    [User] INT NOT NULL DEFAULT 0,
    [Permission] INT NOT NULL DEFAULT 0,
    [BackupDB] INT NOT NULL DEFAULT 0,
    [RestoreDB] INT NOT NULL DEFAULT 0,
    [RadQuantity] INT NOT NULL DEFAULT 0,
    [DoseQuantity] INT NOT NULL DEFAULT 0,
    [Unit] INT NOT NULL DEFAULT 0,
    [TM] INT NOT NULL DEFAULT 0,
    [Certificate] INT NOT NULL DEFAULT 0,
    [Customer] INT NOT NULL DEFAULT 0,
    [City] INT NOT NULL DEFAULT 0,
    [MachineName] INT NOT NULL DEFAULT 0,
    [MachineType] INT NOT NULL DEFAULT 0,
    [SensorType] INT NOT NULL DEFAULT 0,

    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC),
	 
    CONSTRAINT [Role_UNIQUE_Name] UNIQUE([Name])
)
