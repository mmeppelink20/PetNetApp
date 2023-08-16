USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_test_medical_record_by_animal_id;  
GO

print '' print '*** sp_insert_test_medical_record_by_animal_id'
GO
CREATE PROCEDURE [dbo].[sp_insert_test_medical_record_by_animal_id]
(
	@AnimalID				int,
	@MedicalNotes			nvarchar(250),
	@Test					bit,
	@Diagnosis			    nvarchar(250)
)
AS 
	BEGIN
		INSERT INTO [dbo].[MedicalRecord]
		([AnimalId],[MedicalNotes],[Test],[Diagnosis])
		VALUES
		(@AnimalID, @MedicalNotes, @Test, @Diagnosis)
		SELECT SCOPE_IDENTITY()
	END
GO