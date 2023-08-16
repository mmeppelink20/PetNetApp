print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_delete_role_by_user_id_and_role_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_role_by_user_id_and_role_id]
(
	@UsersId	int,
	@RoleId		nvarchar(50)
)
AS
		IF (@RoleId != 'Admin' OR 
			(@RoleId = 'Admin' AND ((SELECT COUNT(*) FROM [dbo].[UserRoles] WHERE RoleId = 'Admin') > 1))
		)
			BEGIN
				DELETE FROM [dbo].[UserRoles]
				WHERE
					UsersId 	= @UsersId
					AND RoleId	= @RoleId
				RETURN @@ROWCOUNT
			END
		
		ELSE
			BEGIN
				RETURN -1
			END
		
GO

/*
print '' print '*** Insert [dbo].[Role]'
GO
INSERT INTO [dbo].[Role] (RoleId, Description)
VALUES 	('Admin', 'desc'),
		('Vet', 'desc')
GO

print '' print '*** Insert [dbo].[UserRoles]'
GO
INSERT INTO [dbo].[UserRoles] (UsersId, RoleId)
VALUES 	(100000, 'Admin'),
	(100000, 'Vet'),
	(100001, 'Admin')
GO


print '' print '*** EXEC sp_delete_role_by_user_id_and_role_id 100001'
GO
EXEC sp_delete_role_by_user_id_and_role_id
	@UsersId	= 100001,
	@RoleId		= 'Admin'
GO


print '' print '*** EXEC sp_delete_role_by_user_id_and_role_id 100000 Vet'
GO
EXEC sp_delete_role_by_user_id_and_role_id
	@UsersId	= 100000,
	@RoleId		= 'Vet'
GO


print '' print '*** EXEC sp_delete_role_by_user_id_and_role_id 100000'
GO
EXEC sp_delete_role_by_user_id_and_role_id
	@UsersId	= 100000,
	@RoleId		= 'Admin'
GO
*/


























