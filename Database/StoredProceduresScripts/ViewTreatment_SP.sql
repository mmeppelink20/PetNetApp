USE [PetNet_db_am]
GO

/***************************************************************
Matthew Meppelink
Created: 2023/02/10

Description:
File containing the stored procedures for ViewTreatment-535 use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_medical_record_diagnosis_by_animalid'
DROP PROCEDURE IF EXISTS [dbo].[sp_select_medical_record_diagnosis_by_animalid]  
GO
CREATE PROCEDURE [dbo].[sp_select_medical_record_diagnosis_by_animalid]
(
	@AnimalId				int
)
AS
	BEGIN
		SELECT  [MedicalRecordId],
				[Diagnosis], 
				[QuarantineStatus], 
				[Prescription], 
				[MedicalNotes],
				[Date]
		FROM 	[dbo].[MedicalRecord]
		WHERE	@AnimalId = [AnimalId]
	END
GO