CREATE PROCEDURE [dbo].[SpRole_GetById]
      @roleID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.[Role]  where [RoleId] = @roleID

END

