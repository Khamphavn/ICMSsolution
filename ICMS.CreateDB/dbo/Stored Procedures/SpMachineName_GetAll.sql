CREATE PROCEDURE [dbo].[SpMachineName_GetAll]
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].MachineName order by [MachineNameId] ASC

END
