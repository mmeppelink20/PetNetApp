USE [PetNet_db_am]
GO

/***************************************************************
Andrew Schneider
Created: 2023/03/05

Description:
File containing the stored procedures for Edit Animal Profile use cases
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/



/* UpdateAnimal stored procedure */
/* Created by Andrew Schneider on 2/8/23 */
print '' print '*** creating sp_update_animal (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_update_animal]
(
	@AnimalId					[int],
	@AnimalShelterId			[int],
	@OldAnimalName				[nvarchar](50),
	@OldAnimalGender			[nvarchar](50),
	@OldAnimalTypeId			[nvarchar](50),
	@OldAnimalBreedId			[nvarchar](50),
	@OldPersonality				[nvarchar](500),
	@OldDescription				[nvarchar](500),
	@OldAnimalStatusId			[nvarchar](50),
	@OldReceivedDate			[date],
	@OldMicrochipSerialNumber	[char](15),
	@OldAggressive				[bit],
	@OldAggressiveDescription	[nvarchar](500),
	@OldChildFriendly			[bit],
	@OldNeuterStatus			[bit],
	@OldNotes					[nvarchar](500),
	@NewAnimalName				[nvarchar](50),
	@NewAnimalGender			[nvarchar](50),
	@NewAnimalTypeId			[nvarchar](50),
	@NewAnimalBreedId			[nvarchar](50),
	@NewPersonality				[nvarchar](500),
	@NewDescription				[nvarchar](500),
	@NewAnimalStatusId			[nvarchar](50),
	@NewReceivedDate			[date],
	@NewMicrochipSerialNumber	[char](15),
	@NewAggressive				[bit],
	@NewAggressiveDescription	[nvarchar](500),
	@NewChildFriendly			[bit],
	@NewNeuterStatus			[bit],
	@NewNotes					[nvarchar](500)
)
AS
	BEGIN
		UPDATE	[Animal]
		SET		[AnimalName] 			= @NewAnimalName,
				[AnimalGender] 			= @NewAnimalGender,
				[AnimalTypeId] 			= @NewAnimalTypeId,
				[AnimalBreedId] 		= @NewAnimalBreedId,
				[Personality] 			= @NewPersonality,
				[Description]			= @NewDescription,
				[AnimalStatusId] 		= @NewAnimalStatusID,
				[RecievedDate] 			= @NewReceivedDate,
				[MicrochipSerialNumber] = @NewMicrochipSerialNumber,
				[Aggressive] 			= @NewAggressive,
				[AggressiveDescription] = @NewAggressiveDescription,
				[ChildFriendly]			= @NewChildFriendly,
				[NeuterStatus]          = @NewNeuterStatus,
				[Notes]          		= @NewNotes
		WHERE	[AnimalId] 				= @AnimalId
		  AND	[AnimalShelterId]		= @AnimalShelterId
		  AND	[AnimalName] 			= @OldAnimalName
		  AND	[AnimalGender] 			= @OldAnimalGender
		  AND	[AnimalTypeId] 			= @OldAnimalTypeId
		  AND	[AnimalBreedId] 		= @OldAnimalBreedId
		  AND	([Personality] 			= @OldPersonality
				OR ([Personality] IS NULL AND @OldPersonality IS NULL))
		  AND	([Description]			= @OldDescription
				OR ([Description] IS NULL AND @OldDescription IS NULL))
		  AND	[AnimalStatusId] 		= @OldAnimalStatusID
		  AND	[RecievedDate] 			= @OldReceivedDate
		  AND 	([MicrochipSerialNumber] = @OldMicrochipSerialNumber
				OR ([MicrochipSerialNumber] IS NULL AND @OldMicrochipSerialNumber IS NULL))
		  AND 	[Aggressive] 			= @OldAggressive
		  AND	([AggressiveDescription] = @OldAggressiveDescription
				OR ([AggressiveDescription] IS NULL AND @OldAggressiveDescription IS NULL))
		  AND	[ChildFriendly]			= @OldChildFriendly
		  AND 	[NeuterStatus]          = @OldNeuterStatus		
		  AND 	([Notes]          		= @OldNotes
				OR ([Notes] IS NULL AND @OldNotes IS NULL))
		RETURN @@ROWCOUNT
	END
GO