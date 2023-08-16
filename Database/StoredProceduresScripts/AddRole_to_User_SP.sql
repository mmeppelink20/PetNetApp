/***************************************************************
Barry Mikulas
Created: 2023/02/13

Description:
File containing the stored procedures for add role to user
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO

print '' print '*** creating sp_insert_role_by_usersId ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_role_by_usersId]
(
	@UsersId		[int],			
	@RoleId			[nvarchar](50)	
)
AS
	BEGIN
		INSERT 	INTO [dbo].[UserRoles]
		([RoleId], [UsersId])
		VALUES
		(@RoleId, @UsersId)
		RETURN 	@@ROWCOUNT
	END
GO