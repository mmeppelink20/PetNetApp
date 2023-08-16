USE [PetNet_db_am]
GO

/***************************************************************
Chris Dreismeier
Created: 2023/04/13

Description:
File containing the stored procedures for Edit users shelter use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_Schedule'
GO
Create procedure [dbo].[sp_update_usershelter]
(
	@Usersid						[int],
    @OldShelterid					[int],
	@NewShelterid					[int]
)
AS
	BEGIN
		UPDATE [Users]
		SET [ShelterId] = 	@NewShelterid
		WHERE 	[UsersId] = 	@UsersId
		AND		([ShelterId] = 	@OldShelterid
			OR	(@OldShelterid IS NULL AND ShelterId IS NULL))

		Return @@ROWCOUNT
    END
GO