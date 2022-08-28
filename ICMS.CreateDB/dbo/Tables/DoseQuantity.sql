CREATE TABLE [dbo].[DoseQuantity]
(
	[DoseQuantityId] INT  IDENTITY(1,1) NOT NULL, 
	[NameVN] NVARCHAR(256) NOT NULL,
    [NameEN] NVARCHAR(256) NOT NULL, 
	[Notation] NVARCHAR(32) NOT NULL,
	[IsActive] BIT NOT NULL DEFAULT (1)

    CONSTRAINT [PK_DosimetryQuantity] PRIMARY KEY CLUSTERED ([DoseQuantityId] ASC),
	CONSTRAINT [DosimetryQuantity_UNIQUE_NameVN] UNIQUE([NameVN])
)
