CREATE PROCEDURE [dbo].[SpCalibData_DeleteById]
	@CalibDataId int
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM dbo.[CalibData] WHERE [CalibDataId] = @CalibDataId

END
