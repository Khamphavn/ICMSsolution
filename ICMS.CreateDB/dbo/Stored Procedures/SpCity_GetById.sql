CREATE PROCEDURE [dbo].[SpCity_GetById]
	@CityId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.City  where [CityId] = @CityId

END