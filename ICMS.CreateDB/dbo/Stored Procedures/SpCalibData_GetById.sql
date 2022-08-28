CREATE PROCEDURE [dbo].[SpCalibData_GetById]
/*
Thứ tự cac column output RẤT quan trọng
Thứ tự này dụng để mapping
*/
	@CalibDataId int
AS
BEGIN 
	SET NOCOUNT ON

	SELECT calib.[CalibDataId], calib.[CertificateId],calib.[MachineReading],calib.[MachineUnit], calib.[RefValue],  calib.[RefUnit], calib.[CF], calib.[CF_reUnc],  
		    calib.[RadQuantityId],rad.[NameVN], rad.[NameEN], rad.[Energy], rad.[ReUnc], rad.[IsActive]
		   

	FROM [dbo].CalibData as calib

	LEFT JOIN [dbo].[RadQuantity] AS rad
	ON  calib.[RadQuantityId] = rad.[RadQuantityId]


	WHERE [CalibDataId] = @CalibDataId


END 
