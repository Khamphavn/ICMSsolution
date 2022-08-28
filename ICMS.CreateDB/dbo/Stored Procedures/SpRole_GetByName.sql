CREATE PROCEDURE [dbo].[SpRole_GetByName]
      @Name NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.[Role]  where [Name] = @Name

END
