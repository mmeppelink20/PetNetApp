USE [PetNet_db_am]
GO

/***************************************************************
Chris Dreismeier
Created: 2023/03/24

Description:
File containing the stored procedures for Delete Schedule use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/


print '' print '*** creating sp_delete_schedule_by_scheduleid'
GO
Create procedure [dbo].[sp_delete_schedule_by_scheduleid]
(
    @ScheduleId					[int]
)
AS
	BEGIN
		Delete FROM [Schedule]
		WHERE		[ScheduleId] = 	@ScheduleId
		
		Return @@ROWCOUNT
    END
GO