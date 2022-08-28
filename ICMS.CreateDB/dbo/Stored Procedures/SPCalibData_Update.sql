CREATE PROCEDURE [dbo].[SpCalibData_Update]
	@CertificateId int,
	@RadQuantityId int,
	@MachineReading nvarchar(255),
	@AvgReading float,
	@MachineUnit nvarchar(63),
	@RefValue float,
	@RefUnit nvarchar(63),
	@CF float,
	@CF_reUnc float,
	@CalibDataId int
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].CalibData
    SET [CertificateId] = @CertificateId,
		[RadQuantityId] = @RadQuantityId,
		[MachineReading] = @MachineReading,
		[AvgReading] = @AvgReading,
		[MachineUnit] = @MachineUnit,
		[RefValue] = @RefValue,
		[RefUnit] = @RefUnit,
		[CF] = @CF,
		[CF_reUnc] = @CF_reUnc
       
    WHERE [CalibDataId] = @CalibDataId

END
