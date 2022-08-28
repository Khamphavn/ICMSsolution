CREATE TABLE [dbo].[Machine]
(
	[MachineId]  INT  IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Model] NVARCHAR(128) NULL, 
    [Serial] NVARCHAR(64) NOT NULL, 
    [MachineTypeId] INT NOT NULL, 
    [Manufacturer] NVARCHAR(128) NULL, 

    [MadeIn] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED ([MachineId] ASC),
    CONSTRAINT [FK_Machine_MachineType] FOREIGN KEY ([MachineTypeId]) REFERENCES [MachineType]([MachineTypeId])
)
