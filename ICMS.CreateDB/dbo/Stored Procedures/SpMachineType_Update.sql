CREATE PROCEDURE [dbo].[SpMachineType_Update]
	@MachineTypeId int,
	@Name nvarchar(256),
	@IsActive bit
AS

BEGIN
	set nocount on

	update [dbo].[MachineType]

	set  [Name] = @Name,
	     [IsActive] = @IsActive

	where MachineTypeId = @MachineTypeId

END
