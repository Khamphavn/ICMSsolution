CREATE PROCEDURE [dbo].[SpCalibData_DeleteByCertificateId]
	@CertificateId int
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE FROM [CalibData] WHERE [CertificateId] = @CertificateId

END
