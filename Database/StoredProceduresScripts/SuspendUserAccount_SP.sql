/***************************************************************
Barry Mikulas
Created: 2023/02/26
Description:
File contains 
    - a stored procedure to retrieve the count of active, unsuspended users by roleId. [sp_select_all_genders]
	- a stored procedure to update suspend status by userId and suspend status [sp_update_user_suspend_by_user_id]
**************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_count_active_unsuspended_users_by_roleId (Barry)'
GO

CREATE PROCEDURE [dbo].[sp_select_count_active_unsuspended_users_by_roleId]
(
	@RoleId		[NVARCHAR](50)
)
AS
    BEGIN
		SELECT COUNT([dbo].[UserRoles].[UsersId]) AS users
		FROM  [dbo].[UserRoles]
		INNER JOIN [dbo].[Users] ON [dbo].[UserRoles].[UsersId] = [dbo].[Users].[UsersId]
		WHERE [dbo].[UserRoles].[RoleId] = @RoleId AND [dbo].[Users].[Active] = 'true' AND [dbo].[Users].[Suspended] = 'false' ;
	END
GO	

print '' print '*** creating sp_update_user_suspend_by_user_id (Barry)'
GO
CREATE PROCEDURE [dbo].[sp_update_user_suspend_by_user_id](
	@UsersId			[int],
	@Suspended			[bit]
)
AS
	BEGIN
    UPDATE [Users]
	SET [Suspended] = @Suspended
	WHERE [UsersId] = @UsersId
	END
GO
