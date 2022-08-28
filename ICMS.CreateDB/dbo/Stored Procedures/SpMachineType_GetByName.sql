CREATE PROCEDURE [dbo].[SpMachineType_GetByName]
	@Name nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * from dbo.MachineType  where [Name] = @Name

END

