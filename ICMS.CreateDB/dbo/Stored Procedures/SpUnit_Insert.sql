CREATE PROCEDURE [dbo].[SpUnit_Insert]
	@Name nvarchar(128),
	@IsActive bit,
	@Order int,
	@UnitId int  = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.[Unit] ([Name], [Order], [IsActive]) values (@Name,@Order, @IsActive)

	SELECT @UnitId = SCOPE_IDENTITY();
END