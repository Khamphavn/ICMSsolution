CREATE PROCEDURE [dbo].[SpUserTable_GetAuthenticatedUser]
	@LoginName nvarchar(64),
	@Password nvarchar(512)
AS
BEGIN 
	SET NOCOUNT ON

	SELECT *

	FROM [dbo].[User] 

	WHERE  [LoginName] = @LoginName AND [Password] = @Password

END 

