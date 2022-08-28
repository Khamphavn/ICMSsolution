CREATE PROCEDURE [dbo].[SpCalibDataTable_GetByCertificateId]
	@CertificateId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT * FROM [dbo].CalibData 

	WHERE [CertificateId] = @CertificateId


END 
