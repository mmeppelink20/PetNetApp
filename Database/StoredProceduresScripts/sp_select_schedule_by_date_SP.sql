USE [PetNet_db_am]

print '' print '*** creating sp_select_schedule_by_date(Chris)'
GO
CREATE PROCEDURE [dbo].[sp_select_schedule_by_date]
(
	@selectedDate			[datetime]
)
AS
	BEGIN
		SELECT 	[ScheduleId],
				[Schedule].[UsersId],
				[JobId],
				[StartTime],
				[EndTime],	
				[Users].[GivenName],
				[Users].[FamilyName]
		FROM	[Schedule] Join [Users] on [Schedule].[UsersId] = [Users].[UsersId]
		WHERE DATEDIFF(day, @selectedDate, [StartTime]) = 0
	END
GO