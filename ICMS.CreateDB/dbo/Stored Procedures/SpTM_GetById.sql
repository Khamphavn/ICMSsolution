CREATE PROCEDURE [dbo].[SpTM_GetById]
	@TMId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.TM where [TMId] = @TMId

END