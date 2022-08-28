CREATE PROCEDURE [dbo].[SpCity_Insert]
	@Name nvarchar(50),
	@PhoneCode nvarchar(10),
	@IsActive bit,
	@CityId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.City([Name],[PhoneCode],[IsActive]) 
	values (@Name, @PhoneCode, @IsActive)

	SELECT @CityId = SCOPE_IDENTITY();
END
