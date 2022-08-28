CREATE PROCEDURE [dbo].[SpSensorType_GetByName]
	@Name nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.SensorType  where [Name] = @Name

END
