CREATE PROCEDURE [dbo].[SpUnit_GetActive]

AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.Unit  where [IsActive] = 1

END