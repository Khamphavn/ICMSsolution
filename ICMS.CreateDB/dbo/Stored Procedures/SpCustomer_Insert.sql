CREATE PROCEDURE [dbo].[SpCustomer_Insert]
	@ShortName nvarchar(50),
	@FullName nvarchar(255),
	@Address nvarchar(512),
	@CityId int,
	@CustomerId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.Customer([ShortName], [FullName], [Address], [CityId] )
	values (@ShortName, @FullName, @Address, @CityId)

	SELECT @CustomerId = SCOPE_IDENTITY();
END
