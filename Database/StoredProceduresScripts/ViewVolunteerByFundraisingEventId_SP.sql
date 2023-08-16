/***************************************************************
Oleksiy Fedchuk
Created: 2023/03/03
Description:
File containing the stored procedure to view volunteers by a fundraising event id

Update:
Added a where clause so it doesn't just grab every single volunteer
****************************************************************/
USE [PetNet_db_am]
GO

/* View Volunteers By Fundraising Event Id*/
/* Oleksiy Fedchuk */

print '' print '*** creating sp_select_volunteers_by_fundraising_event_id'
GO
CREATE PROCEDURE [dbo].[sp_select_volunteers_by_fundraising_event_id]
(
	@FundraisingEventId [int]
)
AS
	BEGIN
		SELECT 	[FundraiserVolunteerUser].[FundraisingEventId], 
				[FundraiserVolunteerUser].[UsersId], [Users].[GivenName], [Users].[FamilyName]
		FROM 	[FundraiserVolunteerUser] JOIN [Users]
		ON 		[FundraiserVolunteerUser].[UsersId] = [Users].[UsersId]
		WHERE	[FundraiserVolunteerUser].[FundraisingEventId] = @FundraisingEventId
	END
GO