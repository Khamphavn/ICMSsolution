CREATE PROCEDURE [dbo].[SpCustomer_Update]
	@CustomerId int,
	@ShortName nvarchar(50),
	@FullName nvarchar(255),
	@Address nvarchar(512),
	@CityId int
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[Customer]
    SET [ShortName] = @ShortName,
		[FullName] = @FullName,
		[Address] = @Address

    WHERE [CustomerId] = @CustomerId

END