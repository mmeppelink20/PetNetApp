/***************************************************************
Barry Mikulas
Created: 2023/04/15

Description:
File containing the stored procedures for ViewFundraisingEvents use case MVC version
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_all_active_fundraising_events_by_shelterId (Barry)'
GO

CREATE PROCEDURE [dbo].[sp_select_all_active_fundraising_events_by_shelterId]
(
	@ShelterId			[int]
)
AS
	BEGIN
		SELECT [FundraisingEventId], [UsersId], [ImageId], [CampaignId], [FundraisingEvent].[ShelterId], [Title], [StartTime], [EndTime], [Hidden], [Complete], 
				[Description], [AdditionalInfo], [Cost], [NumOfAttendees], [NumAnimalsAdopted], [UpdateNotes]
		FROM [FundraisingEvent]
		JOIN [Shelter]
		ON [Shelter].[ShelterId] = [FundraisingEvent].[ShelterId]
		WHERE @ShelterId = [FundraisingEvent].[ShelterId]
		AND [Shelter].[ShelterActive] = 1
		AND	[FundraisingEvent].[Hidden] = 0
	END
GO

print '' print '*** creating sp_select_all_active_fundraising_events (Barry)'
GO

CREATE PROCEDURE [dbo].[sp_select_all_active_fundraising_events]
AS
	BEGIN
		SELECT [FundraisingEventId], [UsersId], [ImageId], [CampaignId], [FundraisingEvent].[ShelterId], [Title], [StartTime], [EndTime], [Hidden], [Complete], 
				[Description], [AdditionalInfo], [Cost], [NumOfAttendees], [NumAnimalsAdopted], [UpdateNotes]
		FROM [FundraisingEvent]
		JOIN [Shelter]
		ON [Shelter].[ShelterId] = [FundraisingEvent].[ShelterId]
		WHERE [Shelter].[ShelterActive] = 1
		AND	[FundraisingEvent].[Hidden] = 0
	END
GO

