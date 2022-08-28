CREATE TABLE [dbo].CalibData
(
	[CalibDataId] INT  IDENTITY(1,1) NOT NULL, 
    [CertificateId] INT NOT NULL,
    
    [MachineReading] NVARCHAR(255) NOT NULL, 
    [AvgReading] FLOAT NOT NULL, 
    [MachineUnit] NVARCHAR(63) NOT NULL, 
    [RefValue] FLOAT NOT NULL, 
    [RefUnit] NVARCHAR(63) NOT NULL, 
    [CF] FLOAT NOT NULL, 
    [CF_reUnc] FLOAT NOT NULL,
    [RadQuantityId] INT NOT NULL

    CONSTRAINT [PK_CalibResult] PRIMARY KEY CLUSTERED ([CalibDataId] ASC),
    CONSTRAINT [FK_CalibResult_Certificate] FOREIGN KEY ([CertificateId]) REFERENCES [Certificate]([CertificateId]),
	CONSTRAINT [FK_CalibResult_RadQuantity] FOREIGN KEY ([RadQuantityId]) REFERENCES [RadQuantity]([RadQuantityId]),


)
