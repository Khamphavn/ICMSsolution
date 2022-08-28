CREATE PROCEDURE [dbo].[SpSensorType_Update]
	@SensorTypeId int,
	@Name nvarchar(256), 
	@IsActive bit
AS

BEGIN
	set nocount on

	update [dbo].[SensorType]

	set  [Name] = @Name,
	    [IsActive] = @IsActive

	where SensorTypeId = @SensorTypeId

END