﻿CREATE TABLE [dbo].[SensorType]
(
	[SensorTypeId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(256) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT SensorType_UNIQUE_Name UNIQUE([Name])
)
