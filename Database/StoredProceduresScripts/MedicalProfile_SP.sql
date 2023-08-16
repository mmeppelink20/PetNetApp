/***************************************************************
William Rients
Created: 2023/02/10
Description:
File containing stored procedure for viewing a specific animals
medical profile and viewing a kennel that an animal is assigned to
****************************************************************/

/* Select Animal Record by AnimalID */
/* William Rients */
USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_animal_record_by_animal_id'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_record_by_animal_id]
(
	@AnimalId		[int]
)
AS
	BEGIN
		SELECT	[AnimalId], [AnimalName], [AnimalGender], [AnimalTypeId], [AnimalBreedId], [Personality], [Description], [AnimalStatusId],
				[RecievedDate], [MicrochipSerialNumber], [Aggressive], [AggressiveDescription], [ChildFriendly], [NeuterStatus],[Notes]
		FROM	[Animal]
		WHERE	@AnimalId = [AnimalId]
	END
GO

/* Select KennelID by AnimalID */
/* William Rients */
print '' print '*** creating sp_select_kennelId_by_animal_Id'
GO
CREATE PROCEDURE [dbo].[sp_select_kennelId_by_animal_Id]
(
	@AnimalId		[int]
)
AS
	BEGIN
		SELECT	[KennelId], [AnimalId]
		FROM	[AnimalKenneling]
		WHERE	@AnimalId = [AnimalId]
	END
GO