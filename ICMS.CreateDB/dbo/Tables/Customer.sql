CREATE TABLE [dbo].[Customer]
(
	[CustomerId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ShortName] NVARCHAR(50) NULL, 
    [FullName] NVARCHAR(255) NOT NULL, 
    [Address] NVARCHAR(512) NULL, 
    [CityId] INT NULL
    
    
    CONSTRAINT Customer_UNIQUE_FullName UNIQUE([FullName]), 
    CONSTRAINT [FK_Customer_City] FOREIGN KEY ([CityId]) REFERENCES [City]([CityId])
    
)
