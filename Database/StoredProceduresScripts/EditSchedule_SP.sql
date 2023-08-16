USE [PetNet_db_am]
GO

/***************************************************************
Chris Dreismeier
Created: 2023/03/09

Description:
File containing the stored procedures for EditSchedule use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_Schedule'
GO
Create procedure [dbo].[sp_update_schedule]
(
    @UsersId					[int],
	@StartTime					[datetime],
    @EndTime					[datetime],
	@OldStartTime				[datetime],	
	@OldEndTime					[datetime],
	@ScheduleId					[int]
)
AS
	BEGIN
		UPDATE [Schedule]
		SET [StartTime] = 	@StartTime,
			[EndTime] = 	@EndTime
		WHERE 	[UsersId] = 	@UsersId
		AND		[ScheduleId] = 	@ScheduleId
		AND		[StartTime] = 	@OldStartTime
		AND		[EndTime] = 	@OldEndTime
		
		Return @@ROWCOUNT
    END
GO