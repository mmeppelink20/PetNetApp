USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_vaccination;  
GO
print '' print '*** Creating sp_insert_vaccination'
GO
CREATE PROCEDURE [dbo].[sp_insert_vaccination]
(
	@MedicalRecordID    		int,
    @UsersID 	         		int, 
    @VaccineName        		nvarchar(50),
    @VaccineAdminsterDate    	datetime
)
AS
	BEGIN
		INSERT INTO [dbo].[Vaccination]
			([MedicalRecordID],[UsersId],[VaccineName],[VaccineAdminsterDate])
		VALUES
			(@MedicalRecordID,@UsersID,@VaccineName,@VaccineAdminsterDate)
	END
GO
