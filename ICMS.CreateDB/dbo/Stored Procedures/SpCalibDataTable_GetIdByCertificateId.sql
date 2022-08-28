CREATE PROCEDURE [dbo].[SpCalibDataTable_GetIdByCertificateId]
	@CertificateId int
AS
	SELECT CalibDataId FROM [CalibData]
	WHERE [CertificateId] = @CertificateId
RETURN 0
