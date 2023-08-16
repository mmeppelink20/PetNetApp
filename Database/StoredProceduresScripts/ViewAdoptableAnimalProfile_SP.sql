/***************************************************************
Hoang Chu
Created: 2023/02/16

Description:
File containing the stored procedures for ViewAdoptableAnimalProfile use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_animal_profile_if_adoptable (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_profile_if_adoptable]
(
    @AnimalId                   [int]
)
AS
	BEGIN
		SELECT [AnimalId], [AnimalName], [AnimalGender], [AnimalTypeId], [AnimalBreedId],		
            [Personality], [Description], [AnimalStatusId], [RecievedDate], [MicrochipSerialNumber], 	
            [Aggressive], [AggressiveDescription], [ChildFriendly], [NeuterStatus], [Notes], [AnimalShelterId]
        FROM [Animal]
        WHERE [AnimalId] = @AnimalId			
	END
GO

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_animal_image_by_animalId (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_image_by_animalId]
(
    @AnimalId                   [int]
)
AS
	BEGIN
		SELECT [Images].[ImageId], [Images].[ImageFileName]
        FROM [Images]
        JOIN [AnimalImage]
        ON [Images].[ImageId] = [AnimalImage].[ImageId]
        WHERE [AnimalImage].[AnimalId] = @AnimalId			
	END
GO
