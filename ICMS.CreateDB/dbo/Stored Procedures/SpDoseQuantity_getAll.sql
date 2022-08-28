CREATE PROCEDURE [dbo].[SpDoseQuantity_GetAll]
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM dbo.DoseQuantity ORDER By DoseQuantityId ASC

END
