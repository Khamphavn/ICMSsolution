CREATE TABLE [dbo].[MachineType]
(
	[MachineTypeId] INT  IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL,

	[IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_MachineType] PRIMARY KEY CLUSTERED ([MachineTypeId] ASC),
	CONSTRAINT MachineType_UNIQUE_Name UNIQUE([Name])
)
