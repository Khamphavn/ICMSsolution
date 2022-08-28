CREATE PROCEDURE [dbo].[SpUnit_GetById]
      @UnitId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.[Unit]  where [UnitId] = @UnitId

END
