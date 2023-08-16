/***************************************************************
Hoang Chu
Created: 2023/03/02

Description:
File containing the stored procedures for ViewUpdatesFromFosterProfile use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
USE [PetNet_db_am]


print '' print '*** creating sp_select_animal_adopted_by_usersId (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_adopted_by_usersId]
(
    @UsersId                   [int]
)
AS
	BEGIN
		SELECT [AnimalId]	
        FROM [FosterPlacement]
        JOIN [Applicant]
        ON [FosterPlacement].[ApplicantId] = [Applicant].[ApplicantId]		
        WHERE [Applicant].[UsersId] = @UsersId 
	END
GO

print '' print '*** creating sp_select_foster_placement_record_notes (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_foster_placement_record_notes]
(
    @AnimalId                   [int]
)
AS
	BEGIN
		SELECT [FosterPlacementRecordId], [FosterPlacementRecord].[FosterPlacementId], [FosterPlacementRecordNotes], [FosterPlacementRecordDate]
        FROM [FosterPlacement]
        JOIN [FosterPlacementRecord]
        ON [FosterPlacement].[FosterPlacementId] = [FosterPlacementRecord].[FosterPlacementId]		
        WHERE [FosterPlacement].[AnimalId] = @AnimalId 
	END
GO