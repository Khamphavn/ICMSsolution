CREATE PROCEDURE [dbo].[SpCalibData_GetByCertificateId]
      @CertificateId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT calib.[CalibDataId], calib.[CertificateId], calib.[MachineReading], calib.[MachineUnit], calib.[RefValue], calib.[RefUnit], calib.[CF], calib.[CF_reUnc],
	       calib.[RadQuantityId], rad.[NameVN], rad.[NameEN], rad.[Energy], rad.[ReUnc], rad.[IsActive]
	
	FROM dbo.CalibData  AS calib

	LEFT JOIN [dbo].[RadQuantity] AS rad
	ON calib.[RadQuantityId] = rad.[RadQuantityId]

	where [CertificateId] = @CertificateId

END
