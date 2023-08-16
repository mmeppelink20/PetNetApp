/***************************************************************
Andrew Cromwell
Created: 2023/02/16

Description:
File containing stored procedures for the EditProcedureUseCase
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creting sp_select_procedures_by_animal_id'
GO
CREATE PROCEDURE [dbo].[sp_select_procedures_by_animal_id]
(
	@AnimalId 					int
)
AS
	BEGIN
		SELECT [MedProcedure].[MedProcedureId],[MedProcedure].[MedicalRecordId],[MedProcedure].[UsersId],
			[Users].[GivenName],[Users].[FamilyName],[MedProcedure].[MedProcedureName],
			[MedProcedure].[MedicationsAdministered],[MedProcedure].[MedProcedureNotes],
			[MedProcedure].[MedProcedureDate],[MedProcedure].[MedProcedureTime]
		FROM [Animal] JOIN [MedicalRecord]
			ON [Animal].[AnimalId] = [MedicalRecord].[AnimalId]
			JOIN [MedProcedure] ON [MedicalRecord].[MedicalRecordId] = [MedProcedure].[MedicalRecordId]
			JOIN [Users] ON [MedProcedure].[UsersId] = [Users].[UsersId]
		WHERE [Animal].[AnimalId] = @AnimalId
		
	END
GO	

print '' print '*** creting sp_update_procedure_by_procedure_id'
GO
CREATE PROCEDURE [dbo].[sp_update_procedure_by_procedure_id]
(
	@ProcedureId				int,
	@OldUserID					int,
	@ProcedureName		    	nvarchar(50),
	@OldProcedureName			nvarchar(50),
	@MedicationsAdministered 	nvarchar(100),
	@OldMedicationsAdministered	nvarchar(100),
	@ProcedureNotes				nvarchar(500),
	@OldProcedureNotes			nvarchar(500),
	@ProcedureDate				datetime,
	@OldProcedureDate			datetime,
	@ProcedureTime				time,
	@OldProcedureTime			time
)
AS
	BEGIN
		UPDATE [MedProcedure]
			SET [MedProcedureName] = @ProcedureName,
				[MedicationsAdministered] = @MedicationsAdministered,
				[MedProcedureNotes] = @ProcedureNotes,
				[MedProcedureDate] = @ProcedureDate,
				[MedProcedureTime] = @ProcedureTime
		WHERE @ProcedureId = [MedProcedureId]
			AND @OldUserID = [UsersId]
			AND @OldProcedureName = [MedProcedureName]
			AND @OldMedicationsAdministered = [MedicationsAdministered]
			AND @OldProcedureNotes = [MedProcedureNotes]
			AND @OldProcedureDate = [MedProcedureDate]
			AND @OldProcedureTime = [MedProcedureTime]
			
	END
GO
