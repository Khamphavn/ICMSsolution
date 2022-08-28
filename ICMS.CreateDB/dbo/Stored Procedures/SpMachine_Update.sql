CREATE PROCEDURE [dbo].[SpMachine_Update]
    @MachineId int,
	@Name nvarchar(256),
	@MachineTypeId int,
    @Model  nvarchar(128),
	@Serial nvarchar(64),
	@Manufacturer nvarchar(128),
	@MadeIn nvarchar(64)

AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[Machine] 
    SET [Name] = @Name,
        [MachineTypeId] = @MachineTypeId,
        [Model] = @Model,
        [Serial] = @Serial,
        [Manufacturer] = @Manufacturer,
        [MadeIn] = @MadeIn

    WHERE [MachineId] = @MachineId

END
