/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

CREATE PROCEDURE [dbo].[SpMachine_GetById]
	@MachineId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT m.[MachineId], m.[Name], m.[Model], m.[Serial], m.Manufacturer, m.MadeIn, m.[MachineTypeId],
		   mt.[Name]

	FROM [dbo].[Machine] AS m

	LEFT JOIN [dbo].[MachineType] AS mt
	ON  (m.[MachineTypeId] = mt.[MachineTypeId])

	WHERE m.[MachineId] = @MachineId


	--SELECT m.[MachineId], m.[Name], m.[Model], m.[Serial], m.Manufacturer, m.[MachineTypeId],
	--	   mt.[Name],
	--	   s.[SensorId], s.[MachineId], s.[Model], s.[Serial], s.[SensorTypeId],
	--	   st.[Name]

	--FROM [dbo].[Machine] AS m

	--LEFT JOIN [dbo].[MachineType] AS mt
	--ON  (m.[MachineTypeId] = mt.[MachineTypeId])

	--LEFT JOIN [dbo].[Sensor] AS s
	--ON (m.[MachineId] = s.[MachineId]) 

	--LEFT JOIN [dbo].[SensorType] AS st
	--ON  s.[SensorTypeId] = st.[SensorTypeId]

	--WHERE m.[MachineId] = @MachineId

END 