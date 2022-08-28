CREATE PROCEDURE [dbo].[SpSensor_DeleteByMachineId]
	@MachineId int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Sensor] WHERE [MachineId] = @MachineId

END