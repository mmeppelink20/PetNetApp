/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_deactivate_shelter_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_deactivate_shelter_by_shelter_id]
(
	@ShelterId		[int]
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET [ShelterActive]		=	0
		WHERE	@ShelterId			=	[ShelterId]
		RETURN	@@ROWCOUNT
	END
GO