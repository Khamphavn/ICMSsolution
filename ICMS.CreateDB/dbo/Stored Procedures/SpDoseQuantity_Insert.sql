CREATE PROCEDURE [dbo].[SpDoseQuantity_Insert]
	@NameVN nvarchar(256),
	@NameEN nvarchar(256),
	@Notation nvarchar(32),
	@IsActive bit,
	@DoseQuantityId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.[DoseQuantity] ( [NameVN], [NameEN], [Notation], [IsActive] )
	values (@NameVN, @NameEN, @Notation, @IsActive)

	SELECT @DoseQuantityId = SCOPE_IDENTITY();
END
