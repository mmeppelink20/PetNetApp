USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_update_quarantine_status_by_medical_record_id;  
GO

print '' print '*** sp_update_quarantine_status_by_medical_record_id'
GO
CREATE PROCEDURE [dbo].[sp_update_quarantine_status_by_medical_record_id]
(
	@MedicalRecordID		int,
	@QuarantineStatus		bit,
	@OldQuarantineStatus	bit
)
AS 
	BEGIN
		UPDATE [MedicalRecord]
			SET [QuarantineStatus] = @QuarantineStatus
		WHERE @MedicalRecordID = [MedicalRecordID]
			AND @OldQuarantineStatus = [QuarantineStatus]
		RETURN @@ROWCOUNT
	END
GO
