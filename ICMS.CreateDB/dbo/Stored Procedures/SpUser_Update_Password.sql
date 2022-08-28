CREATE PROCEDURE [dbo].[SpUser_Update_Password]
	@UserId int ,
	@LoginName nvarchar(64),
	@FullName nvarchar(128),
	@Password nvarchar(512),
	@RoleId int,
	@IsActive bit

AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[User] 
    SET [LoginName] = @LoginName,
		[FullName] = @FullName,
		[Password] = @Password,
		[RoleId] = @RoleId,
		[IsActive] = @IsActive
       
    WHERE [UserId] = @UserId

end
