USE [PetNet_db_am]
GO 

/***************************************************************
Alexis Oetken
Created: 2023/04/05

Description:
File containing the stored procedures for BookmarkAnimal, RemoveBookmark, GetAllBookmarks
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/

/* GetAllUsersBookmarks */
/* Created by Alexis Oetken */

print ''
print '*** creating sp_select_all_user_bookmarks Alex'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_user_bookmarks]
( 
	@UsersId		[int]
)
AS
	BEGIN 
		SELECT 
			[BookmarkAnimal].[AnimalId], [AnimalName]
		FROM [dbo].[BookmarkAnimal]
		INNER JOIN [Animal] ON [Animal].[AnimalId] = [BookmarkAnimal].[AnimalId]
		WHERE [UsersId] = @UsersId
	END 
GO

print ''
print '*** creating sp_bookmark_animal Alex'
GO 
CREATE PROCEDURE [dbo].[sp_bookmark_animal]
(
	@UsersId [int], 
	@AnimalId [int]
)
AS
	BEGIN 
		INSERT INTO [dbo].[BookmarkAnimal]
		([UsersId], [AnimalId])
		VALUES
		(@UsersId, @AnimalId)
	END
GO


print ''
print '*** creating sp_delete_animal_bookmark Alex'
GO 
CREATE PROCEDURE [dbo].[sp_delete_animal_bookmark]
(
	@UsersId [int],
	@AnimalId [int]
)
AS 
	BEGIN 
		DELETE FROM [dbo].[BookmarkAnimal]
		WHERE @AnimalId = [AnimalId]
		AND @UsersId = [UsersId]
END 
GO



