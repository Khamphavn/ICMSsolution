CREATE PROCEDURE [dbo].[SpRadQuantity_GetAll]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

AS
BEGIN 
	SET NOCOUNT ON

	SELECT radQ.[RadQuantityId], radQ.[NameVN],radQ.[NameEN], radQ.[Energy], radQ.[ReUnc], radQ.[IsActive] 

	FROM [dbo].[RadQuantity] AS radQ

	ORDER BY radQ.[RadQuantityId] ASC


end 
