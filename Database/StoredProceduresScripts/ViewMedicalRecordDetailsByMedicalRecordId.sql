/* Created By: Molly Meister */ 

USE [PetNet_db_am]

/* based off sp_select_tests_by_animal_id by Stephen Jaurigue */
print '' print '*** creating sp_select_test_by_medical_record_id'

GO
CREATE PROCEDURE [dbo].[sp_select_test_by_medical_record_id]
(
	@MedicalRecordId	[int]
)
AS
	BEGIN
		SELECT 	[TestId], [MedicalRecord].[MedicalRecordId], [UsersId], [TestName], [TestAcceptableRange], [TestResult], [TestNotes], [TestDate]
		FROM	[Test] 
		JOIN 	[MedicalRecord] ON [Test].[MedicalRecordId] = [MedicalRecord].[MedicalRecordId]
		WHERE	[Test].[MedicalRecordId] = @MedicalRecordId
	END
GO

/* based off sp_select_vaccinations_by_animal_id by unknown */
print '' print '*** creating sp_select_vaccination_by_medical_record_id'

GO
CREATE PROCEDURE [dbo].[sp_select_vaccination_by_medical_record_id]
(
	@MedicalRecordId	[int]
)
AS
	BEGIN
		SELECT 	[VaccineId], [MedicalRecord].[MedicalRecordId], [UsersId], [VaccineName], [VaccineAdminsterDate]	
		FROM 	[Vaccination]
		JOIN 	[MedicalRecord] ON [Vaccination].[MedicalRecordId] = [MedicalRecord].[MedicalRecordId]
		WHERE 	[Vaccination].[MedicalRecordId] = @MedicalRecordId	
	END
GO

/* based off sp_select_procedures_by_animal_id by Andy Cromwell */
print '' print '*** creating sp_select_procedure_by_medical_record_id'

GO
CREATE PROCEDURE [dbo].[sp_select_procedure_by_medical_record_id]
(
	@MedicalRecordId	[int]
)
AS
	BEGIN
		SELECT 	[MedProcedureId], [MedicalRecord].[MedicalRecordId], [UsersId], [MedProcedureName], [MedicationsAdministered], [MedProcedureNotes], 
				[MedProcedureDate]
		FROM 	[MedicalRecord]
		JOIN 	[MedProcedure] ON [MedicalRecord].[MedicalRecordId] = [MedProcedure].[MedicalRecordId]
		WHERE 	[MedicalRecord].[MedicalRecordId] = @MedicalRecordId
		
	END
GO

