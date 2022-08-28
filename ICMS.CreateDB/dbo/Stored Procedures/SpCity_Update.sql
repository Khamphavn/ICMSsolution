CREATE PROCEDURE [dbo].[SpCity_Update]
     @CityId int,
     @Name nvarchar(50),
     @PhoneCode nvarchar(10),
     @IsActive bit
AS
BEGIN
    SET NOCOUNT ON


    UPDATE [dbo].[City] 
    SET [Name] = @Name ,
        [PhoneCode] = @PhoneCode,
        IsActive = @IsActive

    WHERE CityId = @CityId

END