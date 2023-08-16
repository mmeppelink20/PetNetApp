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

print '' print '*** creating [SP_SELECT_ALL_GENDERS]'
		GO
		CREATE PROCEDURE [dbo].[sp_select_all_genders]
		AS
			BEGIN
				SELECT [GenderId]
				FROM [Gender]
			END
		GO

/********************************/

print '' print '*** creating [SP_SELECT_ALL_PRONOUNS]'
		GO
		CREATE PROCEDURE [dbo].[sp_select_all_pronouns]
		AS
			BEGIN
				SELECT [PronounId]
				FROM [Pronoun]
			END
		GO

        print '' print '*** creating [SP_UPDATE_USER_DETAILS]'
		GO
		CREATE PROCEDURE [dbo].[sp_update_user_details]
		(
			@UsersId			[int],
			@NewGivenName		[nvarchar](50),			
			@NewFamilyName		[nvarchar](50),
			@NewGenderId		[nvarchar](50),
			@NewPronounId		[nvarchar](50),
			@NewAddress			[nvarchar](50),
			@NewAddressTwo		[nvarchar](50),
			@NewPhone			[nvarchar](13),
			@NewZipcode			[char](9),
			@OldGivenName		[nvarchar](50),			
			@OldFamilyName		[nvarchar](50),
			@OldGenderId		[nvarchar](50),
			@OldPronounId		[nvarchar](50),
			@OldAddress			[nvarchar](50),
			@OldAddressTwo		[nvarchar](50),
			@OldPhone			[nvarchar](13),
			@OldZipcode			[char](9)
		)
		AS
			BEGIN
				UPDATE [Users]
				SET [GivenName] = @NewGivenName,
					[FamilyName] = @NewFamilyName,
					[GenderId] = @NewGenderId,
					[PronounId] = @NewPronounId,
					[Address] = @NewAddress,
					[AddressTwo] = @NewAddressTwo,
					[Phone] = @NewPhone, -- who this?
					[Zipcode] = @NewZipcode
				WHERE @UsersId = [UsersId]
				AND @OldGivenName = [GivenName]
				AND @OldFamilyName = [FamilyName]
				AND @OldGenderId = [GenderId]
				AND @OldPronounId = [PronounId]
				AND @OldAddress = [Address]
				AND @OldAddressTwo = [AddressTwo]
				AND @OldPhone = [Phone]
				AND @OldZipcode = [Zipcode]
			END
		GO


print '' print '*** creating sp_update_passwordHash'
GO
CREATE PROCEDURE [dbo].[sp_update_passwordHash]
(
	@Email				[nvarchar](50),
	@OldPasswordHash	[nvarchar](100),
	@NewPasswordHash	[nvarchar](100)
)
AS
	BEGIN
		UPDATE [Users]
		SET [PasswordHash] = @NewPasswordHash
		WHERE @Email = [Email]
		AND [PasswordHash] = @OldPasswordHash
		RETURN @@ROWCOUNT
	END
GO

/********************************/

print '' print '*** creating sp_update_user_email'
GO
CREATE PROCEDURE [dbo].[sp_update_user_email]
(
	@OldEmail				[nvarchar](50),
    @NewEmail				[nvarchar](50),
	@PasswordHash	        [nvarchar](100)
)
AS
	BEGIN
		UPDATE [Users]
		SET [Email] = @NewEmail
		WHERE @OldEmail = [Email]
		AND [PasswordHash] = @PasswordHash
		RETURN @@ROWCOUNT
	END
GO