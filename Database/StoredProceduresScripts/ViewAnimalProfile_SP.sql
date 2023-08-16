USE [PetNet_db_am]
GO

/***************************************************************
Andrew Schneider
Created: 2023/03/05

Description:
File containing the stored procedures for View Animal Profile use cases
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/


/* SelectAnimalByAnimalId stored procedure */
/* Created by Andrew Schneider */
print '' print '*** creating sp_select_animal_by_animalId (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_by_animalId]
(
	@AnimalId					[int],
	@AnimalShelterId			[int]
)
AS
	BEGIN
		SELECT	[Animal].[AnimalId], [AnimalName], [AnimalGender], [Animal].[AnimalTypeId], [AnimalBreedId],
				[Kennel].[KennelName], [Personality], [Description], [Animal].[AnimalStatusId],
				[AnimalStatus].[AnimalStatusDescription], [RecievedDate], [MicrochipSerialNumber],
				[Aggressive], [AggressiveDescription], [ChildFriendly], [NeuterStatus], [Notes], [AnimalShelterId]
		FROM 	[Animal]
		JOIN 	[AnimalStatus]
			ON 	[Animal].[AnimalStatusID] = [AnimalStatus].[AnimalStatusID]
		LEFT JOIN 	[AnimalKenneling]
			ON	[Animal].[AnimalId] = [AnimalKenneling].[AnimalId]
		LEFT JOIN	[Kennel]
			ON	[AnimalKenneling].[KennelId] = [Kennel].[KennelId]
		WHERE	@AnimalId = [Animal].[AnimalId]
		AND		@AnimalShelterId = [Animal].[AnimalShelterId]
	END
GO


/* SelectAnimalImagesByAnimalId stored procedure */
/* Created by Andrew Schneider */
print '' print '*** creating sp_select_animal_profile_images_by_animal_id (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_profile_images_by_animal_id]
(
	@AnimalId		[int]
)
AS
	BEGIN
		SELECT	[AnimalImage].[ImageId], [ImageFileName]
		FROM	[AnimalImage]
		JOIN	[Images]
			ON	[AnimalImage].[ImageId] = [Images].[ImageId]
		WHERE 	[AnimalImage].[AnimalId] = @AnimalId
	END 
GO