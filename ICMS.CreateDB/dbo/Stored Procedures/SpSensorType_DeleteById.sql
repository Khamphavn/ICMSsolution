CREATE PROCEDURE [dbo].[SpSensorType_DeleteById]
	@SensorTypeId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [SensorType] WHERE [SensorTypeId] = @SensorTypeId

END
