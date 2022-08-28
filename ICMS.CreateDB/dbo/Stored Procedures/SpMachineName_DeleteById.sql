CREATE PROCEDURE [dbo].[SpMachineName_DeleteById]
	@MachineNameId int
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM dbo.[MachineName]  WHERE [MachineNameId] = @MachineNameId

END