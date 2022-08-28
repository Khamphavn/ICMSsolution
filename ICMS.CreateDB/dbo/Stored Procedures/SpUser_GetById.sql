/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

CREATE PROCEDURE [dbo].[SpUser_GetById]
	@UserId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT u.[UserId], u.[LoginName], u.[FullName], u.[Password], u.[IsActive], u.[RoleId],
		   r.[Name]

	FROM [dbo].[User] AS u
	LEFT JOIN [dbo].[Role] AS r

	ON  u.RoleId = r.RoleId	

	WHERE [UserId] = @UserId

END 

