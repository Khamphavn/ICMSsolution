CREATE PROCEDURE [dbo].[SpSensorType_GetById]
	@SensorTypeId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.SensorType  where [SensorTypeId] = @SensorTypeId

END