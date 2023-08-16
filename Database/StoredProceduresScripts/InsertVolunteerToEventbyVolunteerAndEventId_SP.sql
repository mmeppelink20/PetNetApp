USE [PetNet_db_am]
GO

print '' print '*** creating sp_insert_volunteer_into_event_by_volunteerID_and_eventID'
GO
CREATE PROCEDURE [dbo].[sp_insert_volunteer_into_event_by_volunteerID_and_eventID]
(
	@UsersId				int,
	@FundraisingEventId		int
)
AS
	BEGIN
		INSERT INTO [dbo].[FundraiserVolunteerUser]
						([UsersId], [FundraisingEventId])
					VALUES
						(@UsersId, @FundraisingEventId)
		RETURN @@ROWCOUNT
	END
GO