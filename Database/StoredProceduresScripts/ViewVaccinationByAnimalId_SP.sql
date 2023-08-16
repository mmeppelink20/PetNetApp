USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_vaccinations_by_animal_id;  
GO
print '' print '*** Creating sp_select_vaccinations_by_animal_id'
GO
CREATE PROCEDURE [dbo].[sp_select_vaccinations_by_animal_id]
(
	@AnimalId					int
)
AS
	BEGIN
		SELECT 	[VaccineId],
				[MedicalRecord].[MedicalRecordId],
				[UsersId],
				[VaccineName],
				[VaccineAdminsterDate]
				
		FROM [dbo].[Vaccination]
		JOIN [MedicalRecord]
			ON [Vaccination].[medicalRecordId] = [MedicalRecord].[medicalRecordId]
		WHERE [medicalRecord].[AnimalId] = @AnimalId
		-- AND	  [medicalRecord].[Vaccination] = 1
		
	END
GO
