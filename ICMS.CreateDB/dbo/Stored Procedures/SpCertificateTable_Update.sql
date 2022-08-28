CREATE PROCEDURE [dbo].[SpCertificateTable_Update]
	@CertificateNumber nvarchar(127),
	@DoseQuantityId int,
	@CalibDate date,
	@MachineId int,
	@CustomerId int,
	@Temperature float,
	@Humidity float,
	@Pressure float,
	@PerformedBy nvarchar(127),
	@TM nvarchar(127),
	@Note nvarchar(1023),

	@CertificateId int 
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[Certificate] 
    SET [CertificateNumber] = @CertificateNumber,
	    [DoseQuantityId] = @DoseQuantityId,
		[CalibDate] = @CalibDate,
		[MachineId] = @MachineId,
		[CustomerId] = @CustomerId,
		[Temperature] = @Temperature,
		[Humidity] = @Humidity,
		[Pressure] = @Pressure,
		[PerformedBy] = @PerformedBy,
		[TM] = @TM,
		[Note] = @Note
       
    WHERE [CertificateId] = @CertificateId

END
