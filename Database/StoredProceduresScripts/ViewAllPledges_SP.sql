/***************************************************************
Zaid Rachman
Created: 2023/04/13

Description:
File containing the stored procedure for ViewAllPledges
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_all_pledges;  
GO
print '' print '*** Creating sp_select_all_pledges'
GO
CREATE PROCEDURE [dbo].[sp_select_all_pledges]
AS
	BEGIN
		SELECT 	[PledgeId],
				[DonationId],
				[Pledge].[UsersId],
				[FundraisingEventId],
				[Date],
				[Amount],
				[Message],
				[Target],
				[Requirement],
				[RequirementMet],
				[Pledge].[GivenName],
				[Pledge].[FamilyName],
				[Pledge].[Phone],
				[Pledge].[Email],
				[IsContactPreferencePhone],
				[ReminderSent],
				[ReminderDate]
				
		FROM [dbo].[Pledge]
		JOIN [users]
			ON [users].[UsersId] = [Pledge].[UsersId]
	END
GO
