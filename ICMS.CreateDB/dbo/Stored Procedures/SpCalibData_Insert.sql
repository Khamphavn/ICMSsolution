CREATE PROCEDURE [dbo].[SpCalibData_Insert]
	@CertificateId int,
	@RadQuantityId int,
	@MachineReading nvarchar(255),
	@AvgReading float,
	@MachineUnit nvarchar(53),
	@RefValue float,
	@RefUnit nvarchar(53),
	@CF float,
	@CF_reUnc float,
	@CalibDataId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.CalibData([CertificateId],[RadQuantityId], [MachineReading], [AvgReading],  [MachineUnit], [RefValue], [RefUnit], [CF], [CF_reUnc]) 
	VALUES (@CertificateId ,@RadQuantityId, @MachineReading, @AvgReading, @MachineUnit, @RefValue, @RefUnit, @CF, @CF_reUnc)

	SELECT @CalibDataId = SCOPE_IDENTITY();
END
