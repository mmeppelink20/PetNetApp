USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/03/01

Description:
File containing the stored procedures for ViewDonations use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_donations'
GO
Create procedure [dbo].[sp_select_donations]
(
	@ShelterId	[int]
)
AS
	BEGIN
		Select 	[Donation].[DonationId], [Donation].[UsersId], [Users].[GivenName], [Users].[FamilyName], [Donation].[ShelterId],
				[Amount], [Message], [Date], [Donation].[GivenName], [Donation].[FamilyName], [HasInKindDonation],
                [Anonymous], [Target], [PaymentMethod], [ScheduledDonationId], [FundraisingEventId], [ShelterName]
		From 	[Donation] left join [Users]
					on [Donation].[UsersId] = [Users].[UsersId]
					left join [Shelter]
                    on [Donation].[ShelterId] = [Shelter].[ShelterId]
		Where	[Donation].[ShelterId] = @ShelterId
    END
GO

print '' print '*** creating sp_select_all_donations'
GO
Create procedure [dbo].[sp_select_all_donations]
AS
	BEGIN
		Select 	[Donation].[DonationId], [Donation].[UsersId], [Users].[GivenName], [Users].[FamilyName], [Donation].[ShelterId],
				[Amount], [Message], [Date], [Donation].[GivenName], [Donation].[FamilyName], [HasInKindDonation],
                [Anonymous], [Target], [PaymentMethod], [ScheduledDonationId], [FundraisingEventId], [ShelterName]
		From 	[Donation] left join [Users]
					on [Donation].[UsersId] = [Users].[UsersId]
                    left join [Shelter]
                    on [Donation].[ShelterId] = [Shelter].[ShelterId]
    END
GO

