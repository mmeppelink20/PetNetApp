/***************************************************************
Mads Rhea
Created: 2023/02/01

Description:
File contains 
    - a stored procedure to authenticate the user logging in. [sp_authenticate_user]
    - a stored procedure to select a users roles by their usersId. [sp_select_roles_by_userid]
    - a stored procedure to select user by email. [sp_select_user_by_email]
**************************************************************
Mads Rhea
Updated: 2023/02/24

Description: Merged all log-in stored procedures into one file.
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating [SP_AUTHENTICATE_USER]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_authenticate_user]
        (
            @Email          [nvarchar](100),
            @PasswordHash   [nvarchar](100)
        )
        AS 
            BEGIN
                SELECT  COUNT([UsersId]) AS 'Authenticated'
                FROM    [Users]
                WHERE   @Email = [Email]
                AND     @PasswordHash = [PasswordHash]
                AND     [Active] = 1
            END
        GO

/********************************/

print '' print '*** creating [SP_SELECT_ROLES_FROM_USERSID]'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_userid]
(
	@UsersId		[int]	
)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM	[UserRoles]
		WHERE	@UsersId = [UsersId]
	END
GO