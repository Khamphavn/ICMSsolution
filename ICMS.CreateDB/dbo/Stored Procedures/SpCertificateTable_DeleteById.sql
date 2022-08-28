CREATE PROCEDURE [dbo].[SpCertificateTable_DeleteById]
	@CertificateId int
AS
BEGIN

	SET NOCOUNT ON
	
	DELETE FROM [Certificate] WHERE [CertificateId] = @CertificateId

END