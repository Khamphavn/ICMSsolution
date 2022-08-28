/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

--CREATE PROCEDURE [dbo].[SpSensor_GetById]
--	@SensorId int
--AS
--BEGIN 
--	SET NOCOUNT ON

--	SELECT s.[SensorId], s.[MachineId], s.[Model], s.[Serial], s.[SensorTypeId],
--		   st.[Name]

--	FROM [dbo].[Sensor] AS s
--	LEFT JOIN [dbo].[SensorType] AS st

--	ON  s.SensorTypeId = st.[SensorTypeId]

--	WHERE [SensorId] = @SensorId

--END 