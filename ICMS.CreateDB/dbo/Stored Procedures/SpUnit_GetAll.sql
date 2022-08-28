CREATE PROCEDURE [dbo].[SpUnit_GetAll]
AS
BEGIN
	SET NOCOUNT ON
	SELECT * from dbo.[Unit] order by [IsActive] DESC, [Order] ASC
END
