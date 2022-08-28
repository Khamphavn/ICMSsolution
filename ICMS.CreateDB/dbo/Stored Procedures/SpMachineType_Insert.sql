CREATE PROCEDURE [dbo].[SpMachineType_Insert]
	@Name  nvarchar(256),
	@IsActive bit,
	@MachineTypeId int=0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.[MachineType]([Name], [IsActive]) values(@Name, @IsActive)

	SELECT @MachineTypeId = SCOPE_IDENTITY();
END
