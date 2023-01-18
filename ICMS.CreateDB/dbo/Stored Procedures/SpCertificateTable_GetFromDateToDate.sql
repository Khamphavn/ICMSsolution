CREATE PROCEDURE [dbo].[SpCertificateTable_GetFromDateToDate]
	@FromDate nvarchar(50),
	@ToDate nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	SELECT  [CertificateId], [CertificateNumber], [DoseQuantityId], [CalibDate],  [MachineId], [CustomerId],[Temperature], [Humidity], [Pressure], [PerformedBy], [TM], [Note]
	       

	FROM [dbo].[Certificate] 

	WHERE [CalibDate] BETWEEN @FromDate AND  @ToDate
	ORDER BY [CalibDate] DESC
END
