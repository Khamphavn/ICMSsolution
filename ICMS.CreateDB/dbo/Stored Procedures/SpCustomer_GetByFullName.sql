CREATE PROCEDURE [dbo].[SpCustomer_GetByFullName]
	@FullName nvarchar(512)
AS
BEGIN 
	SET NOCOUNT ON

	SELECT c.[CustomerId], c.[ShortName], c.[FullName], c.[Address],c.[CityId],
		   city.[Name], city.[PhoneCode], city.[IsActive]

	FROM [dbo].[Customer] AS c
	
	LEFT JOIN [dbo].[City] AS city
	ON  c.[CityId] = city.[CityId]

	WHERE c.[FullName] = @FullName

END 