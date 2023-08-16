/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_shelter_active_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_shelter_active_by_shelter_id]
(
	@ShelterId			[int],
	@oldShelterActive	[bit],
	@newShelterActive	[bit]
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET [ShelterActive]		=	@newShelterActive
		WHERE	@ShelterId			=	[ShelterId]
			AND	@oldShelterActive	=	[ShelterActive]
		RETURN	@@ROWCOUNT
	END
GO