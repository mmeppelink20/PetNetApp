USE [PetNet_db_am]

print '' print '*** creating sp_select_fundraising_campaign_by_campaign_id'
GO
CREATE PROCEDURE [dbo].[sp_select_fundraising_campaign_by_campaignId]
(
	@FundraisingCampaignId	[int]
)
AS
	BEGIN
		SELECT [FundraisingCampaignId], [UsersId], [ShelterId], [Title], [StartDate], [EndDate], [Description], [Complete],
				[Active], [AmountRaised], [NumOfAttendees], [NumAnimalsAdopted]
		FROM [FundraisingCampaign]
		WHERE @FundraisingCampaignId = [FundraisingCampaignId]
	END
GO


print '' print '*** creating sp_delete_fundraising_campaign_by_campaignId'
GO
CREATE PROCEDURE [dbo].[sp_delete_fundraising_campaign_by_campaignId]
(
	@FundraisingCampaignId	[int]
)
AS
	BEGIN
		UPDATE [dbo].[FundraisingCampaign]
		SET [Active] = 0
		WHERE [FundraisingCampaignId] = @FundraisingCampaignId
	END
GO

print '' print '*** creating sp_update_fundraising_campaign_details_by_campaignId'
GO
CREATE PROCEDURE [dbo].[sp_update_fundraising_campaign_details_by_campaignId]
(
	@FundraisingCampaignId	[int],
	@Title					[nvarchar](50),
	@StartDate				[datetime],
	@EndDate				[datetime],
	@Description			[nvarchar](250),
	@Complete				[bit],
	@Active					[bit],
	@OldTitle				[nvarchar](50),
	@OldStartDate			[datetime],
	@OldEndDate				[datetime],
	@OldDescription			[nvarchar](250),
	@OldComplete			[bit],
	@OldActive				[bit]
)
AS
	BEGIN
		UPDATE [dbo].[FundraisingCampaign]
		SET	[Title] 	= @Title,
		[StartDate] 	= @StartDate,
		[EndDate]		= @EndDate,
		[Description]	= @Description,
		[Complete]		= @Complete,
		[Active]		= @Active
		WHERE [FundraisingCampaignId] = @FundraisingCampaignId
		AND		[Title] = @OldTitle
		AND		([StartDate] = @OldStartDate OR ([StartDate] IS NULL AND @OldStartDate IS NULL))
		AND		([EndDate] = @OldEndDate OR ([EndDate] IS NULL AND @OldEndDate IS NULL))
		AND		[Description] = @OldDescription
		AND		[Complete] = @OldComplete
		AND		[Active] = @OldActive
	END
GO

print '' print '*** creating sp_select_fundraising_sponsors_by_campaignId'
GO
CREATE PROCEDURE [dbo].[sp_select_fundraising_sponsors_by_campaignId]
(
	@FundraisingCampaignId	[int]
)
AS
	BEGIN
		SELECT 	[InstitutionalEntityId], [CompanyName], [GivenName],			
				[FamilyName], [Email], [Phone], [Address],
				[AddressTwo], [Zipcode], [ContactType]
		FROM 	[dbo].[InstitutionalEntity]
		JOIN	[dbo].[FundraisingCampaignEntity]
		ON		[InstitutionalEntity].[InstitutionalEntityId] = 
				[FundraisingCampaignEntity].[Institution]
		WHERE	[FundraisingCampaignEntity].[Fundraiser] = @FundraisingCampaignId
		AND		[InstitutionalEntity].[ContactType] = "Sponsor"
	END
GO

print '' print '*** creating sp_insert_fundraising_campaign'
GO
CREATE PROCEDURE [dbo].[sp_insert_fundraising_campaign]
(
	@Title			[nvarchar](50),
	@StartDate		[datetime],
	@EndDate		[datetime],
	@Description	[nvarchar](250),
	@UsersId		[int],
	@ShelterId		[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[FundraisingCampaign]
		([Title], [StartDate], [EndDate], [Description],[UsersId], [ShelterId])
		VALUES                                          
		(@Title, @StartDate, @EndDate, @Description, @UsersId, @ShelterId)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_select_all_fundraising_sponsors'
GO
CREATE PROCEDURE [dbo].[sp_select_all_fundraising_sponsors]
AS
	BEGIN
		SELECT 	[InstitutionalEntityId], [CompanyName], [GivenName],			
				[FamilyName], [Email], [Phone], [Address],
				[AddressTwo], [Zipcode], [ContactType]
		FROM 	[dbo].[InstitutionalEntity]
		WHERE	[InstitutionalEntity].[ContactType] = 'Sponsor'
		ORDER BY [CompanyName]
	END
GO

print '' print '*** creating sp_insert_campaign_sponsor'
GO
CREATE PROCEDURE [dbo].[sp_insert_campaign_sponsor]
(
	@FundraisingCampaignId	[int],
	@InstitutionalEntityId	[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[FundraisingCampaignEntity]
		([Institution],[Fundraiser])
		VALUES
		(@InstitutionalEntityId, @FundraisingCampaignId)
	END
GO

print '' print '*** creating sp_delete_campaign_sponsor'
GO
CREATE PROCEDURE [dbo].[sp_delete_campaign_sponsor]
(
	@FundraisingCampaignId	[int],
	@InstitutionalEntityId	[int]
	)
AS
	BEGIN
		DELETE FROM [dbo].[FundraisingCampaignEntity]
		WHERE	[Institution] = @InstitutionalEntityId
		AND		[Fundraiser]			= @FundraisingCampaignId
	END
GO
