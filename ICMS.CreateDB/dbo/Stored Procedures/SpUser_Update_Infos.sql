CREATE PROCEDURE [dbo].[SpUser_Update_Infos]
	@UserId int ,
	@LoginName nvarchar(64),
	@FullName nvarchar(128),
	@RoleId int,
	@IsActive bit

AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[User] 
    SET [LoginName] = @LoginName,
		[FullName] = @FullName,
		[RoleId] = @RoleId,
		[IsActive] = @IsActive
       
    WHERE [UserId] = @UserId

end
