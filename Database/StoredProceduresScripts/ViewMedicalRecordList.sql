/* Created By: Molly Meister */ 

USE [PetNet_db_am]

print '' print '*** creating sp_select_medical_records_by_animal_id'

GO
CREATE PROCEDURE [dbo].[sp_select_medical_records_by_animal_id]
(
	@AnimalId	[int]
)
AS
	BEGIN
		SELECT MedicalRecordId, Date, MedicalNotes, MedProcedure, Test, Vaccination, Prescription, Images, 
				QuarantineStatus, Diagnosis
		FROM MedicalRecord
		WHERE AnimalId = @AnimalId
	END
GO


