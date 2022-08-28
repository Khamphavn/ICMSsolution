CREATE PROCEDURE [dbo].[SpMachineType_DeleteById]
	@MachineTypeId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [MachineType] WHERE [MachineTypeId] = @MachineTypeId

END
