USE [PetNet_db_am]
GO

print '' print 'Creating Procedure sp_select_tests_by_animal_id (Stephen Jaurigue)'
GO

create procedure [dbo].[sp_select_tests_by_animal_id]
(
    @AnimalID [int]             
)
AS
	BEGIN
		SELECT 	[TestId],[MedicalRecord].[MedicalRecordId],[UsersId],[TestName],[TestAcceptableRange],[TestResult],[TestNotes],[TestDate]
		FROM	[Test] JOIN [MedicalRecord]
			ON [Test].[MedicalRecordId] = [MedicalRecord].[MedicalRecordId]
		WHERE	[MedicalRecord].[AnimalId] = @AnimalID
	END
GO