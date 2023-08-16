USE [PetNet_db_am]
GO

/***************************************************************
Andrew Schneider
Created: 2023/04/12

Description:
File containing the stored procedures for View All Adoptable
Animals use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/


/* SelectAllAdoptableAnimals */
/* Created by Andrew Schneider on 04/12/2023 */
print '' print '*** creating sp_select_all_adoptable_animals (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_adoptable_animals]
AS
	BEGIN
		SELECT	[AnimalId], [AnimalShelterId], [AnimalName], [AnimalTypeId], [AnimalBreedId]
		FROM 	[Animal]
		WHERE	[Animal].[AnimalStatusId] = "Healthy"
	END
GO


