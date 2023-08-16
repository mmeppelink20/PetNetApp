USE [PetNet_db_am]

/*******************

CREATED BY:: Teft Francisco
CREATED ON:: 2023-03-07
DESCRIPTION:: This is the procedures for the customer records (adoption records) functionality.

*********************

EDITED BY::
EDITED ON::
DESCRIPTION::

*********************/

/* ADD PROCEDURES */

/* Made by: Teft Francisco */

/* This is an awful way of doing this but currently there is no way to actually get the records by applicant id. */
DROP PROCEDURE IF EXISTS dbo.sp_select_adoption_records_by_user_id;
GO
CREATE PROCEDURE [dbo].[sp_select_adoption_records_by_user_id](
	@UsersId			[int]
)
AS
	BEGIN
    SELECT [AnimalName], [Animal].[AnimalTypeId], [Animal].[AnimalBreedId], [AdoptionPlacement].[AnimalId]
	FROM [AdoptionPlacement] JOIN [Animal] ON
		[AdoptionPlacement].[AnimalId] = [Animal].[AnimalId]
	JOIN [Applicant] ON
		[AdoptionPlacement].[ApplicantId] = [Applicant].[ApplicantId]
	WHERE [Applicant].[UsersId] = @UsersId
	END
GO

/* Made by: Teft Francisco */

/* If you want to get the records by applicant id use this one. */
DROP PROCEDURE IF EXISTS dbo.sp_select_adoption_records_by_applicant_id;
GO
CREATE PROCEDURE [dbo].[sp_select_adoption_records_by_applicant_id](
	@ApplicantId			[int]
)
AS
	BEGIN
    SELECT [AnimalName], [Animal].[AnimalTypeId], [Animal].[AnimalBreedId], [AdoptionPlacement].[AnimalId]
	FROM [AdoptionPlacement] JOIN [Animal] ON
		[AdoptionPlacement].[AnimalId] = [Animal].[AnimalId]
	JOIN [Applicant] ON
		[AdoptionPlacement].[ApplicantId] = [Applicant].[ApplicantId]
	WHERE [Applicant].[ApplicantId] = @ApplicantId
	END
GO