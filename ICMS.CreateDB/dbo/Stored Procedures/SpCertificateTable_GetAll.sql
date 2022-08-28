CREATE PROCEDURE [dbo].[SpCertificateTable_GetAll]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/

AS
BEGIN 
	SET NOCOUNT ON

	SELECT  [CertificateId], [CertificateNumber], [DoseQuantityId],  [MachineId], [CustomerId], [CalibDate], [Temperature], [Humidity], [Pressure], [PerformedBy], [TM], [Note]
	       

	FROM [dbo].[Certificate] 

	ORDER BY [CalibDate] DESC, [CertificateNumber] DESC


END 
