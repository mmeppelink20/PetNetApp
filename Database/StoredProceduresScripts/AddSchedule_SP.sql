USE [PetNet_db_am]
GO

/***************************************************************
Chris Dreismeier
Created: 2023/02/24

Description:
File containing the stored procedures for AddSchedule use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_Schedule'
GO
Create procedure [dbo].[sp_insert_Schedule]
(
    @UsersId					[int],
	@StartTime				[datetime],
    @EndTime				[datetime]
)
AS
	BEGIN
		Insert into [Schedule]
			([UsersId],[StartTime],[EndTime])
        Values
			(@UsersId,@StartTime,@EndTime)
		Return @@ROWCOUNT
    END
GO