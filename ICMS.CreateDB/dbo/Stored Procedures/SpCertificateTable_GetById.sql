CREATE PROCEDURE [dbo].[SpCertificateTable_GetById]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/
	@CertificateId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT  [CertificateId], [CertificateNumber], [DoseQuantityId], [CalibDate],  [MachineId], [CustomerId],[Temperature], [Humidity], [Pressure], [PerformedBy], [TM], [Note]
	       

	FROM [dbo].[Certificate] 

	WHERE [CertificateId] = @CertificateId

END 

