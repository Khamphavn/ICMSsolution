CREATE PROCEDURE [dbo].[SpRole_Update]
     @RoleId int,
     @Name nvarchar(256)
AS
Begin
    set nocount on

    update [dbo].[Role] 
    set [Name] = @Name 
    where RoleId = @RoleId

end