CREATE PROCEDURE [dbo].[SpRadQuantity_GetActive]
AS
BEGIN 
	SET NOCOUNT ON

	SELECT radQ.[RadQuantityId], radQ.[NameVN], radQ.[NameEN], radQ.[Energy], radQ.[ReUnc], radQ.[IsActive] 

	FROM [dbo].[RadQuantity] AS radQ

	WHERE [IsActive] = 1

 	ORDER BY radQ.[RadQuantityId] ASC


END 
