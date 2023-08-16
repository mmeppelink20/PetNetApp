USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_update_vaccination;  
GO
print '' print '*** creating sp_update_vaccination'
GO
CREATE PROCEDURE [dbo].[sp_update_vaccination]
(
	@VaccineId				[int],
	@MedicalRecordId		[int],
	@VaccineName			[nvarchar](100),
	@OldVaccineName			[nvarchar](100),
	@UsersId				[int],
	@OldUsersId				[int],
	@VaccineAdminsterDate	[datetime],
	@OldVaccineAdminsterDate	[datetime]
)
AS
	BEGIN
		UPDATE 	[Vaccination]
		  SET 	[VaccineName] = 			@VaccineName,
				[UsersId] = 				@UsersId,
				[VaccineAdminsterDate] = 	@VaccineAdminsterDate
		WHERE	@VaccineId = 					[VaccineId]
		  AND	@OldVaccineName = 				[VaccineName]
		  AND	@OldUsersId = 					[UsersId]
		  AND	@OldVaccineAdminsterDate = 		[VaccineAdminsterDate]
		  AND	@MedicalRecordId = 				[MedicalRecordId]
		  
		RETURN 	@@ROWCOUNT
	END
GO