CREATE PROCEDURE [dbo].[SpMachineTable_Insert]
	@Name nvarchar(50),
	@MachineTypeId int,
	@Model nvarchar(50),
	@Serial nvarchar(50),
	@Manufacturer nvarchar(50),
	@MadeIn nvarchar(50),

	@MachineId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.[Machine] (
		[Name],
		[MachineTypeId],
		[Model],
		[Serial],
		[Manufacturer],
		[MadeIn]
		)
	values
	(
		@Name,
		@MachineTypeId,
		@Model,
		@Serial,
		@Manufacturer,
		@MadeIn
	)

	SELECT @MachineId = SCOPE_IDENTITY();

END
