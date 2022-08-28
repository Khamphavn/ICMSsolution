CREATE PROCEDURE [dbo].[SpTM_GetByName]
	@Name nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.TM where [Name] = @Name

END