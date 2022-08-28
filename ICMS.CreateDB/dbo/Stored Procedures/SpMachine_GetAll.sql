CREATE PROCEDURE [dbo].[SpMachine_GetAll]
AS
BEGIN 
	SET NOCOUNT ON

	SELECT m.[MachineId], m.[Name], m.[Model], m.[Serial], m.Manufacturer, m.MadeIn, m.[MachineTypeId],
		   mt.[Name]

	FROM [dbo].[Machine] AS m

	LEFT JOIN [dbo].[MachineType] AS mt
	ON  (m.[MachineTypeId] = mt.[MachineTypeId])

END 
