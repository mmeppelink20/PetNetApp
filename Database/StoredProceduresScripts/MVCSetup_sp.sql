/***************************************************************
Mads Rhea
Created: 2023/03/29

Description:
File contains 
    - a stored procedure to [sp_select_all_roles]

**************************************************************
Updated by:
Updated: 202-/--/--

Description: ---
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating [SP_SELECT_ALL_ROLES]'
GO
CREATE PROCEDURE [dbo].[sp_select_all_roles]
AS
	BEGIN
		SELECT 	[RoleID]
		FROM	[Role]
	END
GO

print '' print '*** Creating sp_insert_user_role'
GO
CREATE PROCEDURE [sp_insert_user_role]
(
	@UsersId			    [int],
	@RoleId				    [nvarchar](50)
)
AS
BEGIN
INSERT INTO [dbo].[UserRoles]
	([UsersId], [RoleId])
	VALUES
	(@UsersId, @RoleId)
END
GO

print '' print '*** Creating sp_delete_user_role'
GO
CREATE PROCEDURE [sp_delete_user_role]
(
	@UsersId 			[int],
	@RoleId				[nvarchar](50)
)
AS
BEGIN
	DELETE FROM [dbo].[UserRoles]
	WHERE [UsersId] =	@UsersId
	  AND [RoleId] = 	@RoleId
END
GO