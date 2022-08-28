CREATE PROCEDURE [dbo].[SpMachineName_Update]
     @MachineNameId int,
     @Name nvarchar(512)
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[MachineName] 
    SET [Name] = @Name 
    WHERE [MachineNameId] = @MachineNameId

END