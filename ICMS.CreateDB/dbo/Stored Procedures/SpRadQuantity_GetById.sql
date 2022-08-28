CREATE PROCEDURE [dbo].[SpRadQuantity_GetById]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

	@RadQuantityId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT radQ.[RadQuantityId], radQ.[NameVN], radQ.[NameEN], radQ.[Energy], radQ.[ReUnc], radQ.[IsActive] 

	FROM [dbo].[RadQuantity] AS radQ

	WHERE [RadQuantityId] = @RadQuantityId

 	ORDER BY radQ.[RadQuantityId] ASC


END 
