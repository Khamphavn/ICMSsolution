CREATE PROCEDURE [dbo].[SpUser_GetAll]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

AS
BEGIN 
	SET NOCOUNT ON

	SELECT u.[UserId], u.[LoginName], u.[FullName], u.[Password], u.[IsActive], u.[RoleId],
		   r.[Name]

	FROM [dbo].[User] AS u
	LEFT JOIN [dbo].[Role] AS r

	ON  u.RoleId = r.RoleId	

	ORDER BY [UserId] ASC


end 
