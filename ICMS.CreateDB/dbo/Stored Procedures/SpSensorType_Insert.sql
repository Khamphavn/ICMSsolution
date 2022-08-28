CREATE PROCEDURE [dbo].[SpSensorType_Insert]
	@Name  nvarchar(256),
	@IsActive bit,
	@SensorTypeId int=0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.[SensorType]([Name], [IsActive]) values(@Name, @IsActive)

	SELECT @SensorTypeId = SCOPE_IDENTITY();
END