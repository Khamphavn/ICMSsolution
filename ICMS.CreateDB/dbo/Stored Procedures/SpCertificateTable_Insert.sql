CREATE PROCEDURE [dbo].[SpCertificateTable_Insert]
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


	@CertificateId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.Certificate([CertificateNumber], [DoseQuantityId], [CalibDate], [MachineId], [CustomerId], [Temperature], [Humidity], [Pressure], [PerformedBy], [TM], [Note] ) 
	VALUES (@CertificateNumber, @DoseQuantityId, @CalibDate, @MachineId, @CustomerId, @Temperature, @Humidity, @Pressure, @PerformedBy, @TM, @Note)

	SELECT @CertificateId = SCOPE_IDENTITY();
END
