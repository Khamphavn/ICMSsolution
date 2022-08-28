CREATE PROCEDURE [dbo].[SpRole_DeleteById]
	@RoleId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [Role] WHERE [RoleId] = @RoleId

END