CREATE PROCEDURE [dbo].[SpUser_DeleteById]
	@UserId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [User] WHERE [UserId] = @UserId

END
