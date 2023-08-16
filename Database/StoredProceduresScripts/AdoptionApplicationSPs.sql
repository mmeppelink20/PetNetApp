USE [PetNet_db_am]
GO

print '' print '*** creating sp_insert_adoption_application'

GO
CREATE PROCEDURE [dbo].[sp_insert_adoption_application]
(
	@UsersId 					[int] = NULL,
	@ApplicantGivenName 		[nvarchar](50),
	@ApplicantFamilyName 		[nvarchar](50),
	@ApplicantAddress			[nvarchar](50),
	@ApplicantAddress2			[nvarchar](50) = NULL,
	@ApplicantZipCode			[char](9),
	@ApplicantPhoneNumber 		[nvarchar](13),
	@ApplicantEmail				[nvarchar](254),
	@HomeTypeId					[nvarchar](50),
	@HomeOwnershipId			[nvarchar](50),
	@NumberOfChildren			[int],
	@NumberOfPets				[int],
	@CurrentlyAcceptingAnimals	[bit] = 1,
	@AnimalId					[int],
	@ApplicationDate 			[datetime]
)
AS
	BEGIN
		DECLARE @ApplicantId [int]
		INSERT INTO [Applicant]
			(UsersId, ApplicantGivenName, ApplicantFamilyName, ApplicantAddress, ApplicantAddress2, ApplicantZipCode,
			ApplicantPhoneNumber, ApplicantEmail, HomeTypeId, HomeOwnershipId, NumberOfChildren, NumberOfPets, CurrentlyAcceptingAnimals)
			VALUES
			(@UsersId, @ApplicantGivenName, @ApplicantFamilyName, @ApplicantAddress, @ApplicantAddress2, @ApplicantZipCode,
			@ApplicantPhoneNumber, @ApplicantEmail, @HomeTypeId, @HomeOwnershipId, @NumberOfChildren, @NumberOfPets, @CurrentlyAcceptingAnimals)
		SET @ApplicantId = SCOPE_IDENTITY()
		
		INSERT INTO [AdoptionApplication]
			(ApplicantId, AnimalId, ApplicationStatusId, AdoptionApplicationDate)
			VALUES
			(@ApplicantId, @AnimalId, 'Pending', @ApplicationDate)
		RETURN @@ROWCOUNT
	END
GO


print '' print'*** creating sp_select_all_home_ownership_types'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_home_ownership_types]
AS
	BEGIN
		SELECT HomeOwnershipId
		FROM [HomeOwnership]
	END
GO

print '' print'*** creating sp_select_all_home_types'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_home_types]
AS
	BEGIN
		SELECT HomeTypeId
		FROM [HomeType]
	END
GO

