USE [PetNet_db_am]
GO

/***************************************************************
Ethan Kline
Created: 2023/02/17

Description:
File containing the stored procedures for ViewTreatment-535 use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_edit_medical_notes_by_medical_record_id ***'
GO
CREATE PROCEDURE [dbo].[sp_edit_medical_notes_by_medical_record_id]
(

	@AnimalId			[int],
	@MedicalRecords		[nvarchar](250),
	@IsProcedure		[bit],
	@Test				[bit],
	@Vaccination		[bit],
	@Prescription		[bit],
	@Images				[bit],
	@QuarantineStatus	[bit],
	@Diagnosis			[nvarchar](250),
	@oldMedicalRecords		[nvarchar](50),
	@oldIsProcedure		[bit],
	@oldTest				[bit],
	@oldVaccination		[bit],
	@oldPrescription		[bit],
	@oldImages				[bit],
	@oldQuarantineStatus	[bit],
	@oldDiagnosis			[nvarchar](250),
	@MedicalRecordId		[int]
	
)
AS
	BEGIN
		UPDATE [MedicalRecord]
			SET 	[AnimalId] 		= @AnimalId,
					[MedicalNotes] = @MedicalRecords,
					[MedProcedure] 	= @IsProcedure,
					[Test] 		= 	@Test,
					[Vaccination] 	= @Vaccination,
					[Prescription] 	= @Prescription,
					[Images] 	= @Images,
					[QuarantineStatus] 		= @QuarantineStatus,
					[Diagnosis]	= @Diagnosis
			WHERE 	@AnimalId =[AnimalId]
			AND 	@oldMedicalRecords =[MedicalNotes]
			
			AND		@MedicalRecordId = [MedicalRecordId]
	END
GO