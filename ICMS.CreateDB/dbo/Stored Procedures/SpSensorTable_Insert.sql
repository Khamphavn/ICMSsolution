CREATE PROCEDURE [dbo].[SpSensorTable_Insert]
	@MachineId int,
	@SensorTypeId int,
	@Model nvarchar(128),
	@Serial nvarchar(64),
	@SensorId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.[Sensor] (
		[MachineId],
		[SensorTypeId],
		[Model],
		[Serial]
		)
	values
	(
		@MachineId ,
		@SensorTypeId,
		@Model,
		@Serial
	)

	SELECT @SensorId = SCOPE_IDENTITY();

END
