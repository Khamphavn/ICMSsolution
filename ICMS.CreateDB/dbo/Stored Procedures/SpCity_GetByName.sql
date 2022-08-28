CREATE PROCEDURE [dbo].[SpCity_GetByName]
	@Name nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.City  where [Name] = @Name

END
