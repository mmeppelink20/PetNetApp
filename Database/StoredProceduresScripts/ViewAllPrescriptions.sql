USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_prescriptions_by_MedicalRecordId;  
GO
print '' print '*** Creating sp_select_prescriptions_by_AnimalId (Tyler Hand)'
GO
CREATE PROCEDURE [dbo].[sp_select_prescriptions_by_AnimalId]
(
	@AnimalId					int
	
	
)
AS
	BEGIN
		SELECT 	
				[Prescription].[MedicalRecordId],		
				[UsersId],				
				[PrescriptionTypeId],	
				[PrescriptionName],		
				[PrescriptionDosage],	
				[PrescriptionFrequency],
				[PrescriptionDuration],	
				[PrescriptionNotes],	
				[DatePrescribed],		
				[EndDate]
				
		
		FROM [dbo].[Prescription] 
		join  [MedicalRecord]
		on	[Prescription].[MedicalRecordId] = [MedicalRecord].[MedicalRecordId]
		WHERE [AnimalId] = @AnimalId
		
	END
GO