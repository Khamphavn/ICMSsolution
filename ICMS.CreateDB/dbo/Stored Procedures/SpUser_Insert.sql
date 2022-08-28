CREATE PROCEDURE [dbo].[SpUser_Insert]
	@LoginName nvarchar(64),
	@FullName nvarchar(128),
	@Password nvarchar(512),
	@RoleId int,
	@IsActive bit,
	@UserId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.[User]([LoginName],[FullName], [Password], [RoleId], [IsActive])
	VALUES(@LoginName, @FullName, @Password, @RoleId, @IsActive)

	SELECT @UserId = SCOPE_IDENTITY();

END
