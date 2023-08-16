USE [PetNet_db_am]
GO

print '' print 'Creating sp_select_all_fundraising_events_by_campaignId (Stephen)'
GO

CREATE PROCEDURE [dbo].[sp_select_all_active_fundraising_events_by_campaignId]
(
	@CampaignId			[int]
)
AS
	BEGIN
		SELECT [FundraisingEventId], [UsersId], [ImageId], [CampaignId], [ShelterId], [Title], [StartTime], [EndTime], [Hidden], [Complete], [Description], [AdditionalInfo], [Cost], [NumOfAttendees], [NumAnimalsAdopted], [UpdateNotes]
		FROM [FundraisingEvent]
		WHERE @CampaignId = [CampaignId]
		AND [Hidden] = 0
	END
GO