/***************************************************************
Matthew Meppelink
Created: 2023/02/16

Description:
File containing the stored procedures for EditTreatment use case
**************************************************************
Mathew Meppelink
Updated: 2023/03/22

Description: updated stored procedure to ensure data integrity
is upheld
****************************************************************/
USE [PetNet_db_am]
GO

print '' print '*** creating sp_update_medical_treatment'
DROP PROCEDURE IF EXISTS [dbo].[sp_update_medical_treatment]  
GO
CREATE PROCEDURE [dbo].[sp_update_medical_treatment]
(
	@recordId				           int,

    @newDiagnosis                      nvarchar(250),
    @newMedicalNotes                   nvarchar(250),

    @oldDiagnosis                      nvarchar(250),
    @oldMedicalNotes                   nvarchar(250)
)
AS
	BEGIN
		UPDATE  [MedicalRecord]
            SET [Diagnosis]     = @newDiagnosis,
                [MedicalNotes]  = @newMedicalNotes
        WHERE @recordId         = [MedicalRecordId]
          AND @oldDiagnosis     = [Diagnosis]
          AND @oldMedicalNotes  = [MedicalNotes]

          RETURN @@ROWCOUNT
	END
GO
