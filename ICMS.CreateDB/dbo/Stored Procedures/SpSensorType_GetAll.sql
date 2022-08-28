CREATE PROCEDURE [dbo].[SpSensorType_GetAll]
AS
BEGIN
	SET NOCOUNT ON
	SELECT * from dbo.[SensorType] 
	ORDER By [SensorTypeId] ASC
END
