/******************************************
Alexis Oetken
Created: 02/10/2023

These stored procedures are for new user creation
and for deactivating a prexisting account 

******************************************/
USE [PetNet_db_am]
GO

Drop Procedure IF EXISTS sp_insert_new_user

print '' print '***creating sp_insert_new_user'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_user]
(	
	@GenderId 		[nvarchar](50),
	@PronounId 		[nvarchar](50), 
	@GivenName 		[nvarchar](50),
	@FamilyName 	[nvarchar](50),
	@Email 			[nvarchar](254),
	@PasswordHash	[nvarchar](100),
	@Zipcode		[char](9),
	@Phone 			[nvarchar](13)
) 
AS 
BEGIN
INSERT INTO [dbo].[Users]
(	
	[GenderId],	
	[PronounId],	
	[GivenName],	
	[FamilyName],	
	[Email],	
	[PasswordHash],
	[Zipcode],
	[Phone]
) VALUES (
	@GenderId,
	@PronounId,
	@GivenName, 
	@FamilyName,
	@Email,
	@PasswordHash,
	@Zipcode, 
	@Phone 
)

END
GO

Drop Procedure IF EXISTS sp_deactivate_account

print '' print '*** creating sp_deactivate_account'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_account]
(
	@UsersId [int]
)
AS 
	BEGIN
		UPDATE [Users]
		SET [Active] = 0
		WHERE @UsersId = [UsersId] 
		RETURN @@ROWCOUNT
	END 
GO
