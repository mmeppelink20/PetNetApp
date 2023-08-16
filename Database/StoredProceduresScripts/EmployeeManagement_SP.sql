/***************************************************************
Hoang Chu
Created: 2023/01/10

Description:
File containing the stored procedures for Add_Event use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_all_employees (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
    BEGIN
        SELECT [Users].[UsersId], [GenderId], [PronounId], [ShelterId], [GivenName], [FamilyName],
                [Email], [Address], [AddressTwo], [Zipcode], [Phone], [CreationDate], 
                [Active], [Suspended]    
        FROM [Users]
        WHERE NOT 0 = CAST((SELECT COUNT(RoleId) 
            FROM UserRoles 
            WHERE [UserRoles].[UsersId] = [Users].[UsersId]) AS INT)
        -- JOIN [UserRoles]
        --   ON [Users].[UsersId] = [UserRoles].[UsersId]
        -- WHERE NOT [RoleId] = 'Employee'
    END
GO

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_non_employee_users (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_non_employee_users]
AS
    BEGIN
        SELECT [Users].[UsersId], [GenderId], [PronounId], [ShelterId], [GivenName], [FamilyName],
                [Email], [Address], [AddressTwo], [Zipcode], [Phone], [CreationDate], 
                [Active], [Suspended]    
        FROM [Users]
        WHERE 0 = CAST((SELECT COUNT(RoleId) 
            FROM UserRoles 
            WHERE [UserRoles].[UsersId] = [Users].[UsersId]) AS INT)
        -- JOIN [UserRoles]
        --   ON [Users].[UsersId] = [UserRoles].[UsersId]
        -- WHERE NOT [RoleId] = 'Employee'
    END
GO