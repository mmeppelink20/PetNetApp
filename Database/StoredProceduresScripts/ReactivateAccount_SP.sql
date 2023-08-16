USE [PetNet_db_am]

/*******************

CREATED BY:: Teft Francisco
CREATED ON:: 2023-02-03
DESCRIPTION:: This is the procedures for the reactivate account functionality.

*********************

EDITED BY::
EDITED ON::
DESCRIPTION::

*********************/

/* ADD PROCEDURES */

print '' print '*** sp_select_user_by_user_id'
/* Made by: Teft Francisco */
DROP PROCEDURE IF EXISTS dbo.sp_select_user_by_user_id;
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_user_id](
	@UsersId			[int]
)
AS
	BEGIN
    SELECT [UsersId], [GenderId], [PronounId], [ShelterId], [GivenName], [FamilyName], [Email],
 [Address], [AddressTwo], [Zipcode], [Phone], [CreationDate], [Active], [Suspended]
	FROM [Users]
	WHERE [UsersId] = @UsersId
	END
GO

print '' print '*** sp_select_user_by_email'
/* Made by: Teft Francisco */
DROP PROCEDURE IF EXISTS dbo.sp_select_user_by_email;
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email](
	@Email			[nvarchar](254)
)
AS
	BEGIN
    SELECT [UsersId], [GenderId], [PronounId], [ShelterId], [GivenName], [FamilyName], [Email], [Address], [AddressTwo], [Zipcode], [Phone], [CreationDate], [Active], [Suspended]
	FROM [Users]
	WHERE [Email] = @Email
	END
GO

print '' print '*** sp_suspend_user_by_user_id'
/* Made by: Teft Francisco */
DROP PROCEDURE IF EXISTS dbo.sp_suspend_user_by_user_id;
GO
CREATE PROCEDURE [dbo].[sp_suspend_user_by_user_id](
	@UsersId			[int]
)
AS
	BEGIN
    UPDATE [Users]
	SET [Suspended] = 1, [Active] = 0
	WHERE [UsersId] = @UsersId
	END
GO

print '' print '*** sp_unsuspend_user_by_user_id'
DROP PROCEDURE IF EXISTS dbo.sp_unsuspend_user_by_user_id;
GO
CREATE PROCEDURE [dbo].[sp_unsuspend_user_by_user_id](
	@UsersId			[int]
)
AS
	BEGIN
    UPDATE [Users]
	SET [Suspended] = 0, [Active] = 1
	WHERE [UsersId] = @UsersId
	END
GO

print '' print '*** sp_update_user_active_by_user_id'
/* Made by: Teft Francisco */
DROP PROCEDURE IF EXISTS dbo.sp_update_user_active_by_user_id;
GO
CREATE PROCEDURE [dbo].[sp_update_user_active_by_user_id](
	@UsersId		[int],
	@Active			[bit]
)
AS
	BEGIN
    UPDATE [Users]
	SET [Active] = @Active
	WHERE [UsersId] = @UsersId	
	END
GO

print '' print '*** sp_select_users_by_active_status'
/* Made by: Teft Francisco */
DROP PROCEDURE IF EXISTS dbo.sp_select_users_by_active_status;
GO
CREATE PROCEDURE [dbo].[sp_select_users_by_active_status](
	@Active			[bit]
)
AS
	BEGIN
    SELECT [UsersId], [GenderId], [PronounId], [ShelterId], [GivenName], [FamilyName], [Email],
 [Address], [AddressTwo], [Zipcode], [Phone], [CreationDate], [Active], [Suspended]
	FROM [Users]
	WHERE [Active] = @Active
	END
GO