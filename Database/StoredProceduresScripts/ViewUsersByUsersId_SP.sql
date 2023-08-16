USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_users_by_users_id;  
GO
print '' print '*** Creating sp_select_users_by_users_id'
GO
CREATE PROCEDURE [dbo].[sp_select_users_by_users_id]
(
	@UsersId					int
)
AS
	BEGIN
		SELECT 	[UsersId],
				[GenderId],
				[PronounId],
				[ShelterId],
				[GivenName],
				[FamilyName],
				[Email],
				[PasswordHash],
				[Address],
				[AddressTwo],
				[Zipcode],
				[Phone],
				[CreationDate],
				[Active],
				[Suspended]
				
				
		
		FROM [dbo].[Users]
		WHERE [UsersId] = @UsersId	
		
	END
GO

