CREATE PROCEDURE [dbo].[SpMachine_DeleteById]
	@MachineId int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Machine] WHERE [MachineId] = @MachineId

END