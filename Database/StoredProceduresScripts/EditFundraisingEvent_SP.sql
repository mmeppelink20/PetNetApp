USE [PetNet_db_am]

print '' print '*** creating sp_select_fundrasing_event'
GO
CREATE PROCEDURE [dbo].[sp_select_fundrasing_event]
(
    @FundraisingEventId         [int]
)
AS
	BEGIN
		SELECT [UsersId], [ImageId], [CampaignId], [ShelterId], [Title], [StartTime], [EndTime],		
			[Hidden], [Description], [AdditionalInfo], [NumOfAttendees]
		FROM [FundraisingEvent]
		WHERE @FundraisingEventId = [FundraisingEventId]
	END
GO

print '' print '*** creating sp_select_instititional_entity_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_instititional_entity_by_id]
(
    @InstitutionalEntityId              [int]
)
AS
	BEGIN
		SELECT [InstitutionalEntityId] ,[CompanyName], [GivenName], [FamilyName],
		[Email], [Phone], [Address], [AddressTwo], [Zipcode], [ContactType]
		FROM [InstitutionalEntity]
		WHERE [InstitutionalEntityId] = @InstitutionalEntityId
	END
GO

print '' print '*** creating sp_select_contact_by_eventId'
GO
CREATE PROCEDURE [dbo].[sp_select_contact_by_eventId]
(
    @FundraisingEventId              [int]
)
AS
	BEGIN
		SELECT  [FundraisingEventEntity].[ContactId]
        FROM    [FundraisingEventEntity]
        LEFT JOIN    [InstitutionalEntity] 
        ON      [FundraisingEventEntity].[ContactId] = [InstitutionalEntity].[InstitutionalEntityId]
		WHERE   [EventId] = @FundraisingEventId
        AND     [InstitutionalEntity].[ContactType] = 'Contact'
	END
GO

print '' print '*** creating sp_select_sponsor_by_eventId'
GO
CREATE PROCEDURE [dbo].[sp_select_sponsor_by_eventId]
(
    @FundraisingEventId              [int]
)
AS
	BEGIN
		SELECT  [FundraisingEventEntity].[ContactId]
        FROM    [FundraisingEventEntity]
        LEFT JOIN    [InstitutionalEntity] 
        ON      [FundraisingEventEntity].[ContactId] = [InstitutionalEntity].[InstitutionalEntityId]
		WHERE   [EventId] = @FundraisingEventId
        AND     [InstitutionalEntity].[ContactType] = 'Sponsor'
	END
GO

print '' print '*** creating sp_select_animal_by_eventId'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_by_eventId]
(
    @FundraisingEventId              [int]
)
AS
	BEGIN
		SELECT  [AnimalId]
        FROM    [FundraiserAnimal]
		WHERE   [FundraisingEventId] = @FundraisingEventId
	END
GO

print '' print '*** creating sp_select_institutional_entity_by_institutionalId'
GO
CREATE PROCEDURE [dbo].[sp_select_institutional_entity_by_institutionalId]
(
	@InstitutionalEntityId			[int]
)
AS
	BEGIN
		SELECT [InstitutionalEntityId] ,[CompanyName], [GivenName], [FamilyName],
		[Email], [Phone], [Address], [AddressTwo], [Zipcode], [ContactType]
		FROM [InstitutionalEntity]
		WHERE [InstitutionalEntityId] = @InstitutionalEntityId
	END
GO

print '' print '*** creating sp_update_fundrasing_event'
GO
CREATE PROCEDURE [dbo].[sp_update_fundrasing_event]
(
	@FundraisingEventId	[int],
	@UsersId			[int],
	@ImageId			[nvarchar](36),
	@CampaignId			[int],
	@ShelterId			[int],
	@Title				[nvarchar](100),
	@StartTime			[datetime],
	@EndTime			[datetime],
	@Hidden				[bit],
	@Description		[nvarchar](100),
	@AdditionalInfo		[nvarchar](100),
	@NumOfAttendees		[int]
)
AS
	BEGIN
		UPDATE [FundraisingEvent]
		SET	[UsersId] = @UsersId, [ImageId] = @ImageId, [CampaignId] = @CampaignId, [ShelterId] = @ShelterId, 
			[Title] = @Title, [StartTime] = @StartTime, [EndTime] = @EndTime,		
			[Hidden] = @Hidden, [Description] = @Description, [AdditionalInfo] = @AdditionalInfo, [NumOfAttendees] = @NumOfAttendees
		WHERE [FundraisingEventId] = @FundraisingEventId
	END
GO

print '' print '*** creating sp_delete_fundraiser_animal'
GO
CREATE PROCEDURE [dbo].[sp_delete_fundraiser_animal]
(
	@FundraisingEventId				[int]
)
AS
	BEGIN
		DELETE FROM [FundraiserAnimal]
		WHERE [FundraisingEventId] = @FundraisingEventId
	END
GO

print '' print '*** creating sp_delete_fundraising_event_entity'
GO
CREATE PROCEDURE [dbo].[sp_delete_fundraising_event_entity]
(
	@EventId				[int]
)
AS
	BEGIN
		DELETE FROM [FundraisingEventEntity]
		WHERE [EventId] = @EventId
	END
GO

print '' print '*** creating sp_deactivate_fundrasing_event'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_fundrasing_event]
(
	@FundraisingEventId	[int]
)
AS
	BEGIN
		UPDATE [FundraisingEvent]
		SET	[Hidden] = 1
		WHERE [FundraisingEventId] = @FundraisingEventId
	END
GO
