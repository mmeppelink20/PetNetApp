/***************************************************************
Hoang Chu
Created: 2023/01/10

Description:
File containing the stored procedures for Add_Event use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating sp_insert_animalupdates_by_animalid (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_insert_animalupdates_by_animalid]
(
    @AnimalId                   [int],
    @AnimalRecordNotes          [nvarchar](500)
)
AS
	BEGIN
		INSERT INTO [AnimalUpdates] 
            ([AnimalId], [AnimalRecordNotes])
        VALUES
            (@AnimalId, @AnimalRecordNotes);
        RETURN 	@@ROWCOUNT
	END
GO

print '' print '*** creating sp_select_animalupdates_by_animalid (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_animalupdates_by_animalid]
(
    @AnimalId                   [int]
)
AS
	BEGIN
		SELECT TOP 1	[AnimalRecordNotes]
		FROM 			[AnimalUpdates]
        WHERE [AnimalId] = @AnimalId
		ORDER BY [AnimalRecordId] DESC
	END
GO

print '' print '*** creating sp_select_all_animal_updates_by_animalid (Hoang Chu)'
GO
CREATE PROCEDURE [dbo].[sp_select_all_animal_updates_by_animalid]
(
    @AnimalId                   [int]
)
AS
	BEGIN
		SELECT [AnimalRecordNotes], [AnimalRecordId], [AnimalRecordDate]
		FROM 			[AnimalUpdates]
        WHERE [AnimalId] = @AnimalId
		ORDER BY [AnimalRecordId] DESC
	END
GO