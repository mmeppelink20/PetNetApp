/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_shelter_name_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_shelter_name_by_shelter_id]
(
	@ShelterId		[int],
	@oldName		[nvarchar](50),
	@newName		[nvarchar](50)
)
AS
	BEGIN
		UPDATE		[dbo].[Shelter]
			SET		[ShelterName]	=	@newName
		WHERE		@ShelterId		=	[ShelterId]
			AND		@oldName		=	[ShelterName]
		RETURN	@@ROWCOUNT
	END
GO