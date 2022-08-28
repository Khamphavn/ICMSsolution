CREATE TABLE [dbo].[Sensor]
(
	[SensorId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [MachineId] INT NOT NULL, 
    [Model] nvarchar(128),
    [Serial] nvarchar(64),
    [SensorTypeId] INT NULL, 

    CONSTRAINT [FK_Sensors_SensorType] FOREIGN KEY ([SensorTypeId]) REFERENCES [SensorType]([SensorTypeId]), 
    CONSTRAINT [FK_Sensors_Machine] FOREIGN KEY ([MachineId]) REFERENCES [Machine]([MachineId])
)
