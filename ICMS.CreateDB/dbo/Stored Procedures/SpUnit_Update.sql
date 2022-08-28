CREATE PROCEDURE [dbo].[SpUnit_UpDate]
     @UnitId int,
     @Name nvarchar(128),
     @Order int,
     @IsActive bit
AS
Begin
    set nocount on


    update [dbo].[Unit] 
    set [Name] = @Name , [IsActive] = @IsActive, [Order] = @Order
    where UnitId = @UnitId

end