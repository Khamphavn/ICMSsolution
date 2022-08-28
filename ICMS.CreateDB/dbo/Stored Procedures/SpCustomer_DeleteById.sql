CREATE PROCEDURE [dbo].[SpCustomer_DeleteById]
	@CustomerId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [Customer] WHERE [CustomerId] = @CustomerId

END