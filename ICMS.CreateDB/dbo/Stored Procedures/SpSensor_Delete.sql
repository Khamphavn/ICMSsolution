CREATE PROCEDURE [dbo].[SpSensor_Delete]
	@SensorId int
AS
BEGIN 
	SET NOCOUNT ON
	
	DELETE FROM dbo.[Sensor] WHERE [SensorId] = @SensorId

END
