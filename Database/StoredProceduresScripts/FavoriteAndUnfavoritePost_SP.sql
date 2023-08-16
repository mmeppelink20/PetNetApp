/***************************************************************
Mads Rhea
Created: 2023/04/02

Description:
File contains 
    - a stored procedure to [sp_insert_favorite].
    - a stored procedure to [sp_select_favorites_by_usersId].
    - a stored procedure to [sp_delete_favorite].

**************************************************************
Updated by:
Updated: 202-/--/--

Description: ---
****************************************************************/
USE [PetNet_db_am]
GO

print '' print '*** creating [SP_INSERT_FAVORITE]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_insert_favorite]
        (
            @PostId         [int],
            @UsersId        [int]
        )
        AS
            BEGIN
                INSERT INTO [Favorite]
                ([PostId], [UsersId])
                VALUES
                (@PostId, @UsersId)
                SELECT SCOPE_IDENTITY()
            END
        GO

        print '' print '*** creating [SP_SELECT_FAVORITES_BY_USERSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_favorites_by_usersId]
        (
            @UsersId    [int]
        )
        AS
            BEGIN
                SELECT [Post].[PostId], [Post].[PostAuthor], [Post].[PostContent], [Post].[PostDate], [Post].[PostVisibility], [Users].[GivenName], [Users].[FamilyName]
                FROM [Favorite]
                JOIN [Users]
                ON   [Users].[UsersId] = [Favorite].[UsersId]
                JOIN [Post]
                ON   [Post].[PostId] = [Favorite].[PostId]
                WHERE @UsersId = [Users].[UsersId]
                AND [PostVisibility] = 1
                AND [PostAdminRemoved] = 0
            END
        GO

        print '' print '*** creating [SP_DELETE_FAVORITE]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_delete_favorite]
        (
            @PostId     [int],
            @UsersId    [int]
        )
        AS
            BEGIN
                DELETE FROM [Favorite]
                WHERE [PostId] = @PostId
                AND [UsersId] = @UsersId
                RETURN @@ROWCOUNT
            END
        GO