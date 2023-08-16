/***************************************************************
Barry Mikulas
Created: 2023/02/12

Description:
File containing the stored procedures for role management
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_roles ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles]
AS
	BEGIN
		SELECT 		[RoleId], [Description]
		FROM [Role] 
		ORDER BY [RoleId]
	END
GO


print '' print '*** creating sp_select_user_roles_by_usersId ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_roles_by_usersId]
(
	@UsersId	[int]
)
AS
	BEGIN
		SELECT [Role].[RoleId], [Role].[Description]
		FROM [UserRoles]
		INNER JOIN [Role] ON [Role].[RoleId] = [UserRoles].[RoleId]
		WHERE [UserRoles].[UsersId] = @UsersId
	END
GO

print '' print '*** creating sp_select_user_by_usersId ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_usersId]
(
	@UsersId	[int]
)
AS
	BEGIN
		SELECT [UsersId], [GenderId], [PronounId], [ShelterId],  [GivenName], [FamilyName], [Email], [Address], [AddressTwo],[Zipcode], [Phone], [CreationDate],[Active], [Suspended]
		FROM [Users]
		WHERE [UsersId] = @UsersId
	END
GO