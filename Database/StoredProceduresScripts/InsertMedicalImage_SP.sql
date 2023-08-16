USE [PetNet_db_am]

/* created by Molly Meister */
print '' print '*** Creating sp_insert_animal_medical_images_by_animal_id'
GO

CREATE PROCEDURE [dbo].[sp_insert_animal_medical_images_by_animal_id]
(
	@AnimalId	[int],
	@ImageId			[nvarchar](36)
)
AS
	BEGIN
		DECLARE @MedicalRecordID	[int]
		INSERT INTO [dbo].[MedicalRecord]
		(
			[AnimalId], [MedicalNotes], [Images], [Diagnosis]	
		)
		VALUES
			(@AnimalID, "Uploaded Medical Images", 1, "")
			set @MedicalRecordID = SCOPE_IDENTITY()
			
		INSERT INTO [dbo].[AnimalMedicalImage]
		(
			[ImageId], [MedicalRecordId]
		)
		VALUES
		(@ImageID, @MedicalRecordID)
	END
GO