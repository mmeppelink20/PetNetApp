USE [PetNet_db_am]

print '' print '*** creating sp_select_schedule_by_userId(Chris)'
GO
CREATE PROCEDURE [dbo].[sp_select_schedule_by_userId]
(
	@UserId			[int]
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
		WHERE @UserId = [Schedule].[UsersId]
	END
GO