CREATE PROCEDURE [dbo].[SpDoseQuantity_Update]
	@doseQuantityId int,
	@NameVN nvarchar(256),
	@NameEN nvarchar(256),
	@Notation  nvarchar(32),
	@IsActive bit
AS
BEGIN
	set nocount on

	update [dbo].DoseQuantity

	set [NameVN] = @NameVN,
		[NameEN] = @NameEN,
		[Notation] = @Notation,
		[IsActive] = @IsActive

	where DoseQuantityId = @doseQuantityId

END

