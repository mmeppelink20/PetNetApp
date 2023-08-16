USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_test_by_medical_record_id;  
GO

print '' print '*** sp_insert_test_by_medical_record_id'
GO
CREATE PROCEDURE [dbo].[sp_insert_test_by_medical_record_id]
(
	@MedicalRecordID		int,
	@UserID					int,
	@TestName				nvarchar(50),
	@TestAcceptableRange    nvarchar(50),
	@TestResult             nvarchar(50),
	@TestNotes				nvarchar(500),
	@TestDate				datetime
)
AS 
	BEGIN
		INSERT INTO [dbo].[Test]
		([MedicalRecordId],[UsersId],[TestName],
		[TestAcceptableRange],[TestResult],[TestNotes],[TestDate])
		VALUES
		(@MedicalRecordID, @UserID, @TestName, @TestAcceptableRange, @TestResult,
		@TestNotes, @TestDate)
		RETURN @@ROWCOUNT
	END
GO