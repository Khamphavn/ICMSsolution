CREATE PROCEDURE [dbo].[SpMachineType_GetById]
	@MachineTypeId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.MachineType  where [MachineTypeId] = @MachineTypeId

END
