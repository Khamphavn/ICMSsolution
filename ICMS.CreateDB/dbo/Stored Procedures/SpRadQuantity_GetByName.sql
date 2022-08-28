CREATE PROCEDURE [dbo].[SpRadQuantity_GetByNameVN]
	@Name nvarchar(255)
AS
BEGIN 
	SET NOCOUNT ON

	SELECT radQ.[RadQuantityId], radQ.[NameVN], radQ.[NameEN], radQ.[Energy], radQ.[ReUnc], radQ.[IsActive] 

	FROM [dbo].[RadQuantity] AS radQ

	WHERE radQ.[NameVN] = @Name

	ORDER BY radQ.[RadQuantityId] ASC


END 
