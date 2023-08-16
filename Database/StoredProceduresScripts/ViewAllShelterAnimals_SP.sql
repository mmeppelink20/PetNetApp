

/* Created by: Molly Meister */
/* Edited by: Andrew Schneider (2/19/23) */

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_all_animals_by_shelter_id'

GO
CREATE PROCEDURE [dbo].[sp_select_all_animals_by_shelter_id]
(
	@AnimalShelterId	[int]
)
AS
	BEGIN
		SELECT AnimalId, AnimalName, AnimalTypeId, AnimalBreedId, Personality,
				Description, AnimalStatusId, RecievedDate, MicrochipSerialNumber, Aggressive, 
				AggressiveDescription, ChildFriendly, NeuterStatus, Notes, AnimalShelterId
		FROM Animal
		WHERE AnimalShelterId = @AnimalShelterId
	END
GO