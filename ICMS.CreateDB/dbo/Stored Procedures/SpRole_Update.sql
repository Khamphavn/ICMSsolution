CREATE PROCEDURE [dbo].[SpRoleTable_Update]
     @RoleId int,
     @Name nvarchar(256),
     @User int,
     @Permission int,
     @BackupDB int,
     @RestoreDB int,
     @RadQuantity int,
     @DoseQuantity int,
     @Unit int,
     @TM int,
     @Certificate int,
     @Customer int,
     @City int,
     @MachineName int,
     @MachineType int,
     @SensorType int
AS
Begin
    set nocount on

    update [dbo].[Role] 
    set [Name] = @Name ,
        [User] = @User,
        [Permission] = @Permission,
        [BackupDB] = @BackupDB,
        [RestoreDB] = @RestoreDB,
        [RadQuantity] = @RadQuantity,
        [DoseQuantity] = @DoseQuantity,
        [Unit] = @Unit,
        [TM] = @TM,
        [Certificate] = @Certificate,
        [Customer] = @Customer,
        [City] = @City,
        [MachineName] = @MachineName,
        [MachineType] = @MachineType,
        [SensorType] = @SensorType

    where RoleId = @RoleId

end