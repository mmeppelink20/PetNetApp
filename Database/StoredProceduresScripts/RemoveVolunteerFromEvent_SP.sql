print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO

/* Created by Oleksiy Fedchuk */
print '' print '*** Creating sp_delete_user_from_event_by_user_id_and_event_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_from_event_by_user_id_and_event_id]
(
	@UsersId			int,
	@FundraisingEventId	int
)
AS
	BEGIN
		DELETE FROM [dbo].[FundraiserVolunteerUser]
		WHERE
			UsersId = @UsersId
			AND FundraisingEventId = @FundraisingEventId
		RETURN @@ROWCOUNT
	END
GO