CREATE PROCEDURE [dbo].[SpCity_GetActive]
AS

BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.City  where [IsActive] = 1

END