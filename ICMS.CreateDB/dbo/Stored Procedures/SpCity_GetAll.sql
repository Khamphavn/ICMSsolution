CREATE PROCEDURE [dbo].[SpCity_GetAll]

AS
BEGIN
	SET NOCOUNT ON

	select * from dbo.City order by [CityId] asc

END