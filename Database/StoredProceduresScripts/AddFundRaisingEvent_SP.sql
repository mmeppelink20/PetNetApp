USE [PetNet_db_am]

print '' print '*** creating sp_select_all_fundraising_hosts'
GO
CREATE PROCEDURE [dbo].[sp_select_all_fundraising_hosts]
AS
	BEGIN
		SELECT [InstitutionalEntityId] ,[CompanyName], [GivenName], [FamilyName],
		[Email], [Phone], [Address], [AddressTwo], [Zipcode], [ContactType]
		FROM [InstitutionalEntity]
		WHERE [ContactType] = 'Host'
		ORDER BY [CompanyName]
	END
GO

print '' print '*** creating sp_select_all_fundraising_contacts'
GO
CREATE PROCEDURE [dbo].[sp_select_all_fundraising_contacts]
AS
	BEGIN
		SELECT [InstitutionalEntityId] ,[CompanyName], [GivenName], [FamilyName],
		[Email], [Phone], [Address], [AddressTwo], [Zipcode], [ContactType]
		FROM [InstitutionalEntity]
		WHERE [ContactType] = 'Contact'
		ORDER BY [CompanyName]
	END
GO

print '' print '*** creating sp_insert_fundrasing_event'
GO
CREATE PROCEDURE [dbo].[sp_insert_fundrasing_event]
(
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
		INSERT INTO [FundraisingEvent]
			([UsersId], [ImageId], [CampaignId], [ShelterId], [Title], [StartTime], [EndTime],		
			[Hidden], [Description], [AdditionalInfo], [NumOfAttendees])
		VALUES
			(@UsersId, @ImageId, @CampaignId, @ShelterId, @Title, @StartTime, @EndTime,		
			@Hidden, @Description, @AdditionalInfo, @NumOfAttendees)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_insert_fundraiser_animal'
GO
CREATE PROCEDURE [dbo].[sp_insert_fundraiser_animal]
(
	@FundraisingEventId				[int],
	@AnimalId						[int]
)
AS
	BEGIN
		INSERT INTO [FundraiserAnimal]
			([FundraisingEventId], [AnimalId])
		VALUES
			(@FundraisingEventId, @AnimalId)
		SELECT @@ROWCOUNT
	END
GO

print '' print '*** creating sp_insert_fundraising_event_entity'
GO
CREATE PROCEDURE [dbo].[sp_insert_fundraising_event_entity]
(
	@EventId				[int],
	@ContactId				[int]
)
AS
	BEGIN
		INSERT INTO [FundraisingEventEntity]
			([EventId], [ContactId])
		VALUES
			(@EventId, @ContactId)
		SELECT @@ROWCOUNT
	END
GO

