/***************************************************************
Chris Dreismeier
Created: 02/16/2023
Description:
File containing stored procedure for a user with the volunteer role
****************************************************************/

/* Select user by roleid */
/* Chris Dreismeier */
USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_users_by_roleId;  
GO


print '' print '*** creating sp_select_Users_by_RoleId(Chris)'
GO
CREATE PROCEDURE [dbo].[sp_select_users_by_roleId]
(
	@RoleId			[NVARCHAR](50),
	@ShelterId		[int]
)
AS
	BEGIN
		SELECT 	[Users].[UsersId],
				[GenderId],
				[PronounId],
				[ShelterId],
				[GivenName],
				[FamilyName],
				[Email],
				[Address],
				[AddressTwo],
				[Zipcode],
				[Phone],
				[CreationDate],
				[Active],
				[Suspended]
		FROM	[Users] Join [UserRoles] on [Users].[UsersId] = [UserRoles].[UsersId] 
		WHERE	@RoleId = [UserRoles].[RoleId]
		AND		@ShelterId = [ShelterId]
	END
GO
