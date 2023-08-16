USE [PetNet_db_am]
GO

/***************************************************************
Teft Francisco
Created: 2023/03/14

Description:
Adds functionality for viewing donor's donations in their donor profile
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_donations_by_user_id'
GO
Create procedure [dbo].[sp_select_donations_by_user_id]
(
	@UsersId	[int]
)
AS
	BEGIN
		SELECT 	[Donation].[DonationId], [Donation].[UsersId], [Users].[GivenName], [Users].[FamilyName], [Donation].[ShelterId],
				[Amount], [Message], [Date], [Donation].[GivenName], [Donation].[FamilyName], [HasInKindDonation],
                [Anonymous], [Target], [PaymentMethod], [ScheduledDonationId], [FundraisingEventId]
		FROM 	[Donation] JOIN [Users]
					on [Donation].[UsersId] = [Users].[UsersId]
		WHERE	[Donation].[UsersId] = @UsersId
    END
GO