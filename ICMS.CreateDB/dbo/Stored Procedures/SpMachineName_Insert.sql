CREATE PROCEDURE [dbo].[SpMachineName_Insert]
	@Name nvarchar(512),
	@MachineNameId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.MachineName([Name]) 
	values (@Name)

	SELECT @MachineNameId = SCOPE_IDENTITY();
END
