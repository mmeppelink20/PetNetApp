USE [PetNet_db_am]
GO

print '' print '*** creating sp_insert_adoption_application_response'

GO
CREATE PROCEDURE [dbo].[sp_add_adoption_application_response]
(
	@AdoptionApplicationId				[INT],
	@UsersId							[INT],
	@Approved							[BIT],
	@AdoptionApplicationResponseDate	[DATETIME],
	@AdoptionApplicationResponseNotes	[NVARCHAR](500) = NULL
)
AS
	BEGIN
		INSERT INTO [dbo].[AdoptionApplicationResponse]
		(AdoptionApplicationId, UsersId, Approved, AdoptionApplicationResponseDate, AdoptionApplicationResponseNotes)
		VALUES
		(@AdoptionApplicationId, @UsersId, @Approved, @AdoptionApplicationResponseDate, @AdoptionApplicationResponseNotes)
		
		UPDATE [dbo].[AdoptionApplication]
			SET ApplicationStatusId = 
			CASE WHEN (@Approved = 1) THEN 'Approved'
			ELSE 'Denied'
			END
			WHERE AdoptionApplicationId = @AdoptionApplicationId
		RETURN @@ROWCOUNT
	END
GO


print '' print'*** creating sp_select_all_adoptions_applicants_by_animal_id'
GO 

CREATE PROCEDURE [dbo].[sp_select_all_adoption_applications_by_animal_id]
(
	@animalId int
)
AS 
	BEGIN 
		SELECT 	AdoptionApplication.AdoptionApplicationId, AdoptionApplication.AnimalId, ApplicationStatusId, AdoptionApplicationDate, Applicant.ApplicantId, Applicant.UsersId, 
				ApplicantGivenName, ApplicantFamilyName, ApplicantAddress, ApplicantAddress2, ApplicantZipCode,
				ApplicantPhoneNumber, ApplicantEmail, HomeTypeId, HomeOwnershipId, NumberOfChildren, NumberOfPets, CurrentlyAcceptingAnimals,
				AnimalShelterId, AnimalName, AnimalGender, AnimalTypeId, AnimalBreedId, Personality, Description, AnimalStatusId, RecievedDate,
				MicrochipSerialNumber, Aggressive, AggressiveDescription, ChildFriendly, NeuterStatus, Notes
		FROM 	AdoptionApplication
		JOIN	Applicant on AdoptionApplication.ApplicantId = Applicant.ApplicantId
		JOIN	Animal on AdoptionApplication.AnimalId = Animal.AnimalId
		WHERE	AdoptionApplication.AnimalId = @animalId
	END
GO 


print '' print'*** creating sp_update_application_status_by_animal_id_for_approved_application'
GO 

CREATE PROCEDURE [dbo].[sp_update_application_status_by_animal_id_for_approved_application]
(
	@adoptionApplicationId 				int,
	@usersId 							int,
	@approved 							bit,
	@adoptionApplicationResponseDate 	datetime,
	@adoptionApplicationResponseNotes 	[nvarchar](500)
	
)
AS
	BEGIN
		INSERT INTO [dbo].[AdoptionApplicationResponse]
		(AdoptionApplicationId, UsersId, Approved, AdoptionApplicationResponseDate, AdoptionApplicationResponseNotes)
		VALUES
		(@adoptionApplicationId, @usersId, @approved, @adoptionApplicationResponseDate, @adoptionApplicationResponseNotes)
		
		UPDATE 	[dbo].[AdoptionApplication]
		SET 	ApplicationStatusId = 'Denied'
		WHERE 	AdoptionApplicationId = @adoptionApplicationId
		RETURN @@ROWCOUNT
	END
GO

print '' print'*** creating sp_select_all_adoption_applications_by_users_id'
GO 

CREATE PROCEDURE [dbo].[sp_select_all_adoption_applications_by_users_id]
(
	@usersId int
)
AS 
	BEGIN 
		SELECT 	AdoptionApplication.AdoptionApplicationId, AdoptionApplication.AnimalId, ApplicationStatusId, AdoptionApplicationDate, Applicant.ApplicantId, Applicant.UsersId, 
				ApplicantGivenName, ApplicantFamilyName, ApplicantAddress, ApplicantAddress2, ApplicantZipCode,
				ApplicantPhoneNumber, ApplicantEmail, HomeTypeId, HomeOwnershipId, NumberOfChildren, NumberOfPets, CurrentlyAcceptingAnimals,
				AnimalShelterId, AnimalName, AnimalGender, AnimalTypeId, AnimalBreedId, Personality, Description, AnimalStatusId, RecievedDate,
				MicrochipSerialNumber, Aggressive, AggressiveDescription, ChildFriendly, NeuterStatus, Notes
		FROM 	AdoptionApplication
		JOIN	Applicant on AdoptionApplication.ApplicantId = Applicant.ApplicantId
		JOIN 	Animal on AdoptionApplication.AnimalId = Animal.AnimalId
		WHERE	Applicant.UsersId = @usersId
	END
GO