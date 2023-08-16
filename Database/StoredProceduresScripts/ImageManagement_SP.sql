/***************************************************************
Mads Rhea
Created: 2023/02/01

Description:
File contains 
    - a stored procedure to select all genders from the database. [sp_select_all_genders]
    - a stored procedure to select all pronouns from the database. [sp_select_all_pronouns]
    - a stored procedure to update user details based on record old values. [sp_update_user_details]
    - a stored procedure to update user active to false. [sp_deactivate_account]
    - a stored procedure to update user password based off old password and email. [sp_update_passwordHash]
    - a stored procedure to update user email based off password and old email. [sp_update_user_email]
**************************************************************
Mads Rhea
Updated: 2023/02/24

Description: Merged all account setting stored procedures into one file.
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating sp_insert_image'
GO
CREATE PROCEDURE [dbo].[sp_insert_image]
(
	@ImageId			[nvarchar](36),
	@ImageFileName		[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[images]
		([ImageId],[ImageFileName])
		VALUES
		(@ImageId, @ImageFileName)
	END
GO