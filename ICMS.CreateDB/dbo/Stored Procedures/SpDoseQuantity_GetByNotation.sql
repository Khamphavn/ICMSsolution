CREATE PROCEDURE [dbo].[SpDoseQuantity_GetByNotation]
	@Notation nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM dbo.[DoseQuantity]  WHERE [Notation] = @Notation

END
