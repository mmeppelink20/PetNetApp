USE [PetNet_db_am]
GO

/***************************************************************
Teft Francisco
Created: 2023/03/22

Description:
Retrieves the hours of operation for a shelter ordered by the day of the week (Sunday - Saturday)
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_hours_of_operation_by_shelter_id'
GO
CREATE PROCEDURE [dbo].[sp_select_hours_of_operation_by_shelter_id]
(
	@ShelterId	[int]
)
AS
	BEGIN
		SELECT 	[OpenTime], [CloseTime]
		FROM 	[HoursOfOperation]
		WHERE	[ShelterId] = @ShelterId
		ORDER BY [DayOfWeek]
    END
GO

print '' print '*** creating sp_update_hours_of_operation_by_shelter_id'
GO
CREATE PROCEDURE [dbo].[sp_update_hours_of_operation_by_shelter_id]
(
	@ShelterId	[int],
	@DayOfWeek	[int],
	@OpenTime	[time],
	@CloseTime	[time]
)
AS
	BEGIN
		UPDATE 	[dbo].[HoursOfOperation]
		SET 	[OpenTime] = @OpenTime,
					[CloseTime] = @CloseTime
		WHERE 	[ShelterId] = @ShelterId
			AND
				[DayOfWeek] = @DayOfWeek
		RETURN @@ROWCOUNT
    END
GO