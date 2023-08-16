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

print '' print '*** creating sp_select_medical_notes'
GO
CREATE PROCEDURE [dbo].[sp_select_medical_notes]
(
	@AnimalId 					int
)
AS
	BEGIN
		SELECT 	[MedicalRecordId],[AnimalId],[Date],[MedicalNotes],[MedProcedure],[Test],[Vaccination],[Prescription],[Images],[QuarantineStatus],[Diagnosis]
		FROM 	[dbo].[MedicalRecord]
		WHERE [AnimalId] = @AnimalId
		ORDER BY [Date]desc
	END
GO