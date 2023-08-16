/***************************************************************
William Rients
Created: 2023/02/10
Description:
File containing stored procedure for adding an animal to a kennel
and viewing animals with no assigned kennel
****************************************************************/

USE [PetNet_db_am]
GO

/* Insert Animal into Kennel */
/* William Rients */
print '' print '*** creating sp_insert_animal_into_kennel_by_animalId_and_kennelId'
GO
CREATE PROCEDURE [dbo].[sp_insert_animal_into_kennel_by_animalId_and_kennelId]
(
	@KennelId		[int],
	@AnimalId		[int]
)
AS
	BEGIN
		INSERT INTO	[dbo].[AnimalKenneling]
			([KennelId], [AnimalId])
		VALUES	
			(@KennelId, @AnimalId)
	END
GO


/* Select All Animals without kennel */
/* William Rients */
print '' print '*** creating sp_select_all_animals_without_kennel'
GO
CREATE PROCEDURE [dbo].[sp_select_all_animals_without_kennel]
(
    @ShelterId    [int]
)
AS
    BEGIN
        SELECT  [Animal].[AnimalId], [AnimalName], [AnimalTypeId], [AnimalBreedId], 
                [MicrochipSerialNumber], [AnimalShelterId]
        FROM    [Animal] LEFT JOIN [AnimalKenneling] 
				ON [Animal].[AnimalId] = [AnimalKenneling].[AnimalId]
                LEFT JOIN [Death] 
				ON [Animal].[AnimalId] = [Death].[AnimalId]
        WHERE   [AnimalKenneling].[AnimalId] IS NULL
        AND     [Death].[AnimalId] IS NULL
        AND     [AnimalShelterId] = @ShelterId
    END
GO