CREATE PROCEDURE [dbo].[SpCalibDataTable_GetById]
	@CalibDataTableId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT [CalibDataId], [CertificateId], [RadQuantityId], [MachineReading], [MachineUnit], [RefValue], [RefUnit], [CF], [CF_reUnc]
	FROM [dbo].[CalibData] 

	WHERE [CalibDataId] = @CalibDataTableId


END 
