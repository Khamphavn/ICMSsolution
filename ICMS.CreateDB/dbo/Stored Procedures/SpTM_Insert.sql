CREATE PROCEDURE [dbo].[SpTM_Insert]
	@Name nvarchar(127),
	@TMId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.TM([Name]) 
	values (@Name)

	SELECT @TMId = SCOPE_IDENTITY();
END
