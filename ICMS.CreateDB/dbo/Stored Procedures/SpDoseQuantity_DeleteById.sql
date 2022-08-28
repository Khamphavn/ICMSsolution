CREATE PROCEDURE [dbo].[SpDoseQuantity_DeleteById]
	@DoseQuantityId int
AS
BEGIN

	SET NOCOUNT ON
	
	DELETE FROM [DoseQuantity] WHERE [DoseQuantityId] = @DoseQuantityId

END