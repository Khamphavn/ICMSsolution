CREATE PROCEDURE [dbo].[SpMachineType_GetAll]
AS
BEGIN
	SET NOCOUNT ON
	SELECT * from dbo.MachineType 
	ORDER By MachineTypeId ASC
END

