﻿CREATE PROCEDURE [dbo].[SpUser_GetAuthenticatedUser]
	@LoginName nvarchar(64),
	@Password nvarchar(512)
AS
BEGIN 
	SET NOCOUNT ON

	SELECT u.[UserId], u.[LoginName], u.[FullName], u.[Password], u.[IsActive], u.[RoleId],
		   r.[Name]

	FROM [dbo].[User] AS u
	LEFT JOIN [dbo].[Role] AS r

	ON  u.RoleId = r.RoleId	

	WHERE  u.[LoginName] = @LoginName AND u.[Password] = @Password

END 

