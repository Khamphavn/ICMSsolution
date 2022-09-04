CREATE PROCEDURE [dbo].[SpUserTable_GetAll]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

AS
BEGIN 
	SET NOCOUNT ON

	SELECT *

	FROM [dbo].[User] 

	ORDER BY [UserId] ASC


end 
