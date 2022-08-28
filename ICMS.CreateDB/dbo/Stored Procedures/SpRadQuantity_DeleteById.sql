CREATE PROCEDURE [dbo].[SpRadQuantity_DeleteById]
	@RadQuantityId int
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[RadQuantity]  WHERE [RadQuantityId] = @RadQuantityId
END