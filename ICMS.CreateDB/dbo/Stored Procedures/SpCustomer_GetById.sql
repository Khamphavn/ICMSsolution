CREATE PROCEDURE [dbo].[SpCustomer_GetById]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

	@CustomerId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT c.[CustomerId], c.[ShortName], c.[FullName], c.[Address],c.[CityId],
		   city.[Name], city.[PhoneCode], city.[IsActive]

	FROM [dbo].[Customer] AS c
	
	LEFT JOIN [dbo].[City] AS city
	ON  c.[CityId] = city.[CityId]

	WHERE c.[CustomerId] = @CustomerId

END 
