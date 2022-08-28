CREATE TABLE [dbo].[City]
(
	[CityId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(50) NOT NULL,
    [PhoneCode] int,
    [IsActive] BIT NOT NULL DEFAULT (1)

    CONSTRAINT City_UNIQUE_City UNIQUE([Name])
)
