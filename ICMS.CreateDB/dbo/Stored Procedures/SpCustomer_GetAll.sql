CREATE PROCEDURE [dbo].[SpCustomer_GetAll]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

AS
BEGIN 
	SET NOCOUNT ON

	SELECT c.[CustomerId], c.[ShortName], c.[FullName], c.[Address], c.[CityId],
		   city.[Name], city.[PhoneCode], city.[IsActive]

	FROM [dbo].[Customer] AS c
	LEFT JOIN [dbo].[City] AS city

	ON  c.[CityId] = city.[CityId]

	ORDER BY c.[CustomerId] ASC


END 
