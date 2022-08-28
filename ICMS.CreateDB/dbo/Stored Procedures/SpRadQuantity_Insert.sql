CREATE PROCEDURE [dbo].[SpRadQuantity_Insert]
	@NameVN nvarchar(256),
    @NameEN nvarchar(256),
    @Energy float,
    @ReUnc float,
    @IsActive bit,
	@RadQuantityId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.[RadQuantity]( [NameVN], [NameEN], [Energy], [ReUnc] ,[IsActive]) 
	VALUES (@NameVN, @NameEN, @Energy, @ReUnc, @IsActive )

	SELECT @RadQuantityId = SCOPE_IDENTITY();
END