CREATE PROCEDURE [dbo].[SpRoleTable_GetAll]
AS
BEGIN
	SET NOCOUNT ON

	select * from [dbo].[Role] order by [RoleId] ASC

END