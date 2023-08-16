/***************************************************************
Ethan Kline
Created: 2023/02/10

Description:
File containing stored procedure for AddExamNotes use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creting sp_add_medical_notes'
GO

CREATE PROCEDURE [dbo].[sp_add_medical_notes]
(
	@AnimalId			[int],
	@MedicalNotes		[nvarchar](250),
	@Diagnosis			[nvarchar](250)

)
AS
	BEGIN
		INSERT INTO [dbo].[MedicalRecord]
			([AnimalId],[MedicalNotes],[Diagnosis])
		VALUES
			(@AnimalId,@MedicalNotes,@Diagnosis)
		;
	END
GO
