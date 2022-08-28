/*
RoleListID:  
   + Greater value = more permission

*/



CREATE PROCEDURE [dbo].[SpRole_Insert]
	@Name nvarchar(256),
	
	@RoleId int = 0 output
AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.[Role]( [Name])
	values(@Name)

	SELECT @RoleId = SCOPE_IDENTITY();

END