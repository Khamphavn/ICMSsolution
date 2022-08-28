CREATE PROCEDURE [dbo].[SpTM_Update]
     @TMId int,
     @Name nvarchar(127)
AS
BEGIN
    set nocount on

    update [dbo].[TM] 
    set [Name] = @Name 
       
    where TMId = @TMId

end