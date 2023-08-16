USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/03/08

Description:
File containing the stored procedures for ViewSpecificDonation use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_donation_by_donationId'
GO
Create procedure [dbo].[sp_select_donation_by_donationId]
(
	@DonationId	[int]
)
AS
	BEGIN
		Select 	[Donation].[DonationId], [Donation].[UsersId], [Users].[GivenName], [Users].[FamilyName], [Donation].[ShelterId],
				[Amount], [Message], [Date], [Donation].[GivenName], [Donation].[FamilyName], [HasInKindDonation],
                [Anonymous], [Target], [PaymentMethod], [ScheduledDonationId], [FundraisingEventId]
		From 	[Donation] left join [Users]
					on [Donation].[UsersId] = [Users].[UsersId]
		Where	[DonationId] = @DonationId
    END
GO

print '' print '*** creating sp_select_inkind_donations_by_donationId'
GO
Create procedure [dbo].[sp_select_inkind_donations_by_donationId]
(
	@DonationId	[int]
)
AS
	BEGIN
		Select 	[InKindId], [InKind].[DonationId], [Description], [Quantity], [Target], [Received]
		From 	[InKind]
		Where	[InKind].[DonationId] = @DonationId
    END
GO

