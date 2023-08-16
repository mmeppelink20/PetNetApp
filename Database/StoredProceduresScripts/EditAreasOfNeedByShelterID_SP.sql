/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_areas_of_need_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_areas_of_need_by_shelter_id]
(
	@ShelterId		[int],
	@newAreasofneed	[nvarchar](max)
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET [Areasofneed]		=	@newAreasofneed
		WHERE	@ShelterId			=	[ShelterId]
		RETURN	@@ROWCOUNT
	END
GO