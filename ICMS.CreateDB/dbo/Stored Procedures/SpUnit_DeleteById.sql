CREATE PROCEDURE [dbo].[SpUnit_DeleteById]
	@UnitId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [Unit] WHERE [UnitId] = @UnitId

END
