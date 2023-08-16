USE [PetNet_db_am]
GO

print '' print'*** creating sp_select_all_foster_applications_by_users_id'
GO 

CREATE PROCEDURE [dbo].[sp_select_all_foster_applications_by_users_id]
(
	@usersId int
)
AS 
	BEGIN 
		SELECT 	FosterApplication.FosterApplicationId, FosterApplication.ApplicantId, ApplicationStatusId, FosterApplicationDate, FosterApplicationStartDate, FosterApplicationMaxAnimals,
				Applicant.ApplicantId, UsersId, ApplicantGivenName, ApplicantFamilyName, ApplicantAddress, ApplicantAddress2, ApplicantZipCode,
				ApplicantPhoneNumber, ApplicantEmail, HomeTypeId, HomeOwnershipId, NumberOfChildren, NumberOfPets, CurrentlyAcceptingAnimals, AnimalTypeId
		FROM 	FosterApplication
		JOIN	Applicant on FosterApplication.ApplicantId = Applicant.ApplicantId
		JOIN	FosterApplicationAnimalType on FosterApplication.FosterApplicationId = FosterApplicationAnimalType.FosterApplicationId
		WHERE	Applicant.UsersId = @usersId
	END
GO