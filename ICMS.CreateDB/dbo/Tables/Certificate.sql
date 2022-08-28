CREATE TABLE [dbo].[Certificate]
(
	[CertificateId] INT  IDENTITY(1,1) NOT NULL, 
    [CertificateNumber] NVARCHAR(127) NOT NULL, 
    [DoseQuantityId] int not null,
    [CalibDate] DATE NOT NULL, 

    [MachineId] INT NOT NULL, 
    [CustomerId] INT NOT NULL, 

    [Temperature] FLOAT NOT NULL DEFAULT (22), 
    [Humidity] FLOAT NOT NULL DEFAULT (60), 
    [Pressure] FLOAT NOT NULL DEFAULT (1013), 

    [PerformedBy] NVARCHAR(255) NOT NULL, 
    [TM] NVARCHAR(255) NOT NULL, 

    [Note] NVARCHAR(1023) NULL

    CONSTRAINT [PK_Certificate] PRIMARY KEY CLUSTERED ([CertificateId] ASC),

    -- Need delete Certificate first , then delete machine
    -- Can not delete Machine if existe a certificate which have MachineId
    CONSTRAINT [FK_Certificate_MachineId] FOREIGN KEY ([MachineId]) REFERENCES [Machine]([MachineId]),  
    CONSTRAINT [FK_Certificate_DoseQuantity] FOREIGN KEY ([DoseQuantityId]) REFERENCES [DoseQuantity]([DoseQuantityId]),  
    CONSTRAINT [FK_Certificate_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([CustomerId]),  
    CONSTRAINT [Certificate_UNIQUE_CertificateNumber] UNIQUE([CertificateNumber])
)
