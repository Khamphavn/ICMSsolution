CREATE PROCEDURE [dbo].[SpDoseQuantity_GetById]
	@doseQuantityId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.DoseQuantity  where DoseQuantityId = @doseQuantityId

END