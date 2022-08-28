CREATE PROCEDURE [dbo].[SpSensor_Update]
	@MachineId int,
	@SensorTypeId int,
	@Model nvarchar(128),
	@Serial nvarchar(64),
	@SensorId int  

AS
BEGIN
    SET NOCOUNT ON


    UPDATE [dbo].[Sensor] 
    SET [MachineId] = @MachineId ,
        [SensorTypeId] = @SensorTypeId,
        [Model] = @Model,
		[Serial] = @Serial
    WHERE [SensorId] = @SensorId

END
