CREATE PROCEDURE [dbo].[SpCity_DeleteById]
	@CityId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [City] WHERE [CityId] = @CityId

END
