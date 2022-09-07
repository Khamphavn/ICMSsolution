CREATE PROCEDURE [dbo].[SpUser_Update_Password]
	@UserId int ,
	@Password nvarchar(512)
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[User] 
    SET [Password] = @Password

    WHERE [UserId] = @UserId

end
