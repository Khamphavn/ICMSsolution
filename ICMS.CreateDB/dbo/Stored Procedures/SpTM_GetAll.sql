CREATE PROCEDURE [dbo].[SpTM_GetAll]
AS
BEGIN
	SET NOCOUNT ON

	select * from [dbo].[TM] order by [TMId] ASC

END