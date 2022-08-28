CREATE PROCEDURE [dbo].[SpDoseQuantity_GetActive]
AS

BEGIN
	SET NOCOUNT ON

	SELECT * FROM dbo.DoseQuantity  WHERE [IsActive] = 1

END