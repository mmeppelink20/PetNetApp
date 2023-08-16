USE [PetNet_db_am]
GO

print '' print 'Creating sp_select_all_fundraising_campaigns_by_shelterId'
GO

CREATE PROCEDURE [dbo].[sp_select_all_fundraising_campaigns_by_shelterId]
(
	@ShelterId			int
)
AS
	BEGIN
		SELECT [FundraisingCampaignId], [UsersId], [ShelterId], [Title], [StartDate], [EndDate], [Description], [Complete],
				[Active], [AmountRaised], [NumOfAttendees], [NumAnimalsAdopted]
		FROM [FundraisingCampaign]
		WHERE @ShelterId = [ShelterId]
	END
GO

USE [PetNet_db_am]
GO

print '' print 'Creating sp_select_all_active_fundraising_campaigns_by_shelterId'
GO

CREATE PROCEDURE [dbo].[sp_select_all_active_fundraising_campaigns_by_shelterId]
(
	@ShelterId			int
)
AS
	BEGIN
		SELECT [FundraisingCampaignId], [UsersId], [FundraisingCampaign].[ShelterId], [Title], [StartDate], [EndDate], [Description], [Complete], [Active]
		FROM [FundraisingCampaign]
		JOIN [Shelter]
		ON [Shelter].[ShelterId] = [FundraisingCampaign].[ShelterId]
		WHERE @ShelterId = [FundraisingCampaign].[ShelterId]
		AND [Shelter].[ShelterActive] = 1
		AND	[FundraisingCampaign].[Active] = 1
	END
GO


print '' print 'Creating sp_select_all_active_fundraising_campaigns'
GO

CREATE PROCEDURE [dbo].[sp_select_all_active_fundraising_campaigns]
AS
	BEGIN
		SELECT [FundraisingCampaignId], [UsersId], [FundraisingCampaign].[ShelterId], [Title], [StartDate], [EndDate], [Description], [Complete], [Active]
		FROM [FundraisingCampaign]
		JOIN [Shelter]
		ON [Shelter].[ShelterId] = [FundraisingCampaign].[ShelterId]
		WHERE [Shelter].[ShelterActive] = 1
		AND	[FundraisingCampaign].[Active] = 1
	END
GO