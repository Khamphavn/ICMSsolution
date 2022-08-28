/*
IsActive:   0 = false
            1 = true
*/
CREATE TABLE [dbo].[RadQuantity]
(
	[RadQuantityId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [NameVN] NVARCHAR(256) NOT NULL, 
    [NameEN] NVARCHAR(256) NOT NULL, 
    [Energy] FLOAT NOT NULL,
    [ReUnc] FLOAT NULL DEFAULT (0.10), 
    [IsActive] BIT NOT NULL DEFAULT 1

    CONSTRAINT RadQuantity_UNIQUE_NameVN UNIQUE([NameVN]),
    CONSTRAINT RadQuantity_UNIQUE_NameEN UNIQUE([NameEN]),
)
