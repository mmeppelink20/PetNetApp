USE [PetNet_db_am]
GO
print '' print '*** creating sp_select_all_volunteers'
GO
CREATE PROCEDURE [dbo].[sp_select_all_volunteers]
AS
	BEGIN
		SELECT		[FundraiserVolunteerUser].[UsersId]
		FROM		[FundraiserVolunteerUser]
		GROUP BY	[FundraiserVolunteerUser].[UsersId]
	END
GO