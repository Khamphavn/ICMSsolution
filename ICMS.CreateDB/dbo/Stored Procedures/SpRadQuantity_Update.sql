CREATE PROCEDURE [dbo].[SpRadQuantity_Update]
    @RadQuantityId int,
    @NameVN nvarchar(256),
    @NameEN nvarchar(256),
    @Energy float,
    @ReUnc float,
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[RadQuantity] 

    SET [NameVN] = @NameVN,
        [NameEN] = @NameEN,
        [Energy] = @Energy,
        [ReUnc] = @ReUnc,
        [IsActive] = @IsActive

    where [RadQuantityId] = @RadQuantityId

end