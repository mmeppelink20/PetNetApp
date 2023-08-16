USE [PetNet_db_am]
GO

/***************************************************************
Andrew Schneider
Created: 2023/02/10

Description:
File containing the stored procedures for Add Animal Profile use cases
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/


/* SelectAllAnimalBreeds */
/* Created by Andrew Schneider on 2/8/23 */
print '' print '*** creating sp_select_all_animal_breeds (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_animal_breeds]
AS
	BEGIN
		SELECT [AnimalBreedId], [AnimalBreed].[AnimalTypeId]
		FROM [AnimalBreed]
	END
GO


/* SelectAllAnimalGenders */
/* Created by Andrew Schneider on 2/8/23 */
print '' print '*** creating sp_select_all_animal_genders (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_animal_genders]
AS
	BEGIN
		SELECT [GenderId]
		FROM [Gender]
	END
GO


/* SelectAllAnimalTypes */
/* Created by Andrew Schneider on 2/8/23 */
print '' print '*** creating sp_select_all_animal_types (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_animal_types]
AS
	BEGIN
		SELECT [AnimalTypeId]
		FROM [AnimalType]
	END
GO


/* SelectAllAnimalStatuses */
/* Created by Andrew Schneider on 2/8/23 */
print '' print '*** creating sp_select_all_animal_statuses (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_animal_statuses]
AS
	BEGIN
		SELECT [AnimalStatusId]
		FROM [AnimalStatus]
	END
GO


/* InsertAnimal stored procedure */
/* Created by John */
/* Updated by Andrew S. */
print '' print '*** creating sp_insert_animal (John)'
GO
CREATE PROCEDURE [dbo].[sp_insert_animal]
(
	@AnimalShelterId			[int],
	@AnimalName					[nvarchar](50),
	@AnimalGender				[nvarchar](50),
	@AnimalTypeId				[nvarchar](50),
	@AnimalBreedId				[nvarchar](50),
	@Personality				[nvarchar](500),
	@Description				[nvarchar](500),
	@AnimalStatusId				[nvarchar](50),
	@MicrochipSerialNumber		[char](15),
	@Aggressive					[bit],
	@AggressiveDescription		[nvarchar](500),	
	@ChildFriendly				[bit],
	@NeuterStatus				[bit],
	@Notes						[nvarchar](500)	
)

AS
	BEGIN
		INSERT INTO [dbo].[Animal]
		([AnimalName], [AnimalGender], [AnimalTypeId], [AnimalBreedId], [Personality], [Description]
		, [AnimalStatusId], [MicrochipSerialNumber], [Aggressive]
		, [AggressiveDescription], [ChildFriendly], [NeuterStatus], [Notes], [AnimalShelterId])
		VALUES
		(@AnimalName, @AnimalGender, @AnimalTypeId, @AnimalBreedId, @Personality, @Description
		, @AnimalStatusId, @MicrochipSerialNumber, @Aggressive, @AggressiveDescription, @ChildFriendly
		, @NeuterStatus, @Notes, @AnimalShelterId)
	SELECT SCOPE_IDENTITY()
	END
GO
	

/* InsertAnimalImages stored procedure */
/* Created by Andrew Schneider based on
   InsertMedicalImages created by Molly
*/
print '' print '*** Creating sp_insert_animal_profile_images_by_animal_id (Andrew S.)'
GO

CREATE PROCEDURE [dbo].[sp_insert_animal_profile_images_by_animal_id]
(
	@AnimalId			[int],
	@ImageId			[nvarchar](36)
)
AS
	BEGIN	
		INSERT INTO [dbo].[AnimalImage]
		(
			[ImageId], [AnimalId]
		)
		VALUES
		(@ImageID, @AnimalID)
	END
GO


/* Select all empty kennels */
/* William Rients and Andrew S.*/
print '' print '*** creating sp_select_all_empty_kennels'
GO
CREATE PROCEDURE [dbo].[sp_select_all_empty_kennels]
(
    @ShelterId    [int]
)
AS
    BEGIN
        SELECT  [Kennel].[KennelId], [Kennel].[KennelName], [Kennel].[AnimalTypeId]
        FROM    [Kennel] LEFT JOIN [AnimalKenneling] 
				ON [Kennel].[KennelId] = [AnimalKenneling].[KennelId]
        WHERE   [AnimalKenneling].[AnimalId] IS NULL
        AND     [ShelterId] = @ShelterId
    END
GO

