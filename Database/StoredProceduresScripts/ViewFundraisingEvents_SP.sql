USE [PetNet_db_am]
GO

print '' print 'Creating sp_select_all_fundraising_events_by_shelterId (Barry)'
GO

CREATE PROCEDURE [dbo].[sp_select_all_fundraising_events_by_shelterId]
(
	@ShelterId			[int]
)
AS
	BEGIN
		SELECT [FundraisingEventId], [UsersId], [ImageId], [CampaignId], [ShelterId], [Title], [StartTime], [EndTime], [Hidden], [Complete], [Description], [AdditionalInfo], [Cost], [NumOfAttendees], [NumAnimalsAdopted], [UpdateNotes]
		FROM [FundraisingEvent]
		WHERE @ShelterId = [ShelterId]
	END
GO

print '' print '*** creating sp_select_fundraising_institutional_entities_by_eventId_contactType (Barry)'
GO
CREATE PROCEDURE [dbo].[sp_select_fundraising_institutional_entities_by_eventId_contactType]
(
	@FundraisingEventId	[int],
	@ContactType		[nvarchar](15)
)
AS
	BEGIN
		SELECT 	[InstitutionalEntityId], [CompanyName], [GivenName],			
				[FamilyName], [Email], [Phone], [Address],
				[AddressTwo], [Zipcode], [ContactType]
		FROM 	[dbo].[InstitutionalEntity]
		JOIN	[dbo].[FundraisingEventEntity]
		ON		[InstitutionalEntity].[InstitutionalEntityId] = 
				[FundraisingEventEntity].[ContactId]
		WHERE	[FundraisingEventEntity].[EventId] = @FundraisingEventId
		AND		[InstitutionalEntity].[ContactType] = @ContactType
	END
GO

print '' print '*** creating sp_select_institutional_entity_by_event_id_and_contact_type (Barry)'
GO
CREATE PROCEDURE [dbo].[sp_select_institutional_entity_by_event_id_and_contact_type]
(
	@FundraisingEventId	[int],
	@ContactType		[nvarchar](15)
)
AS
	BEGIN
		SELECT 	TOP(1) [InstitutionalEntityId], [CompanyName], [GivenName],			
				[FamilyName], [Email], [Phone], [Address],
				[AddressTwo], [Zipcode], [ContactType]
		FROM 	[dbo].[InstitutionalEntity]
		JOIN	[dbo].[FundraisingEventEntity]
		ON		[InstitutionalEntity].[InstitutionalEntityId] = 
				[FundraisingEventEntity].[ContactId]
		WHERE	[FundraisingEventEntity].[EventId] = @FundraisingEventId
		AND		[InstitutionalEntity].[ContactType] = @ContactType
	END
GO

print '' print '*** creating sp_select_sum_donation_amount_by_event_id (Barry)'
GO
CREATE PROCEDURE [dbo].[sp_select_sum_donation_amount_by_event_id]
(
	@FundraisingEventId	[int]
)
AS
	BEGIN
		SELECT COALESCE(SUM([dbo].[Donation].[Amount]),0)
		FROM [dbo].[Donation]
		WHERE  [dbo].[Donation].[FundraisingEventId] = @FundraisingEventId
	END
GO

print '' print '*** creating sp_select_donations_by_event_id (Barry)'
GO
CREATE PROCEDURE [dbo].[sp_select_donations_by_event_id]
(
	@FundraisingEventId	[int]
)
AS
	BEGIN
		Select 	[Donation].[DonationId], [Donation].[UsersId], [Users].[GivenName], [Users].[FamilyName], [Donation].[ShelterId],
				[Amount], [Message], [Date], [Donation].[GivenName], [Donation].[FamilyName], [HasInKindDonation],
                [Anonymous], [Target], [PaymentMethod], [ScheduledDonationId], [FundraisingEventId]
		From 	[Donation] left join [Users]
					on [Donation].[UsersId] = [Users].[UsersId]
		Where	[Donation].[FundraisingEventId] = @FundraisingEventId
	END
GO

print '' print '*** creating sp_select_all_animals_by_fundraising_event_id'

GO
CREATE PROCEDURE [dbo].[sp_select_all_animals_by_fundraising_event_id]
(
	@FundraisingEventId	[int]
)
AS
	BEGIN
		SELECT [Animal].[AnimalId], [Animal].[AnimalName], [Animal].[AnimalTypeId], [Animal].[AnimalBreedId], [Animal].[Personality],
				[Animal].[Description], [Animal].[AnimalStatusId], [Animal].[RecievedDate], [Animal].[MicrochipSerialNumber], [Animal].[Aggressive], 
				[Animal].[AggressiveDescription], [Animal].[ChildFriendly], [Animal].[NeuterStatus], [Animal].[Notes], [Animal].[AnimalShelterId]
		FROM [Animal]
		JOIN [FundraiserAnimal]
			ON [Animal].[AnimalId] = [FundraiserAnimal].[AnimalId]
			WHERE [FundraiserAnimal].[FundraisingEventId] = @FundraisingEventId
		ORDER BY [AnimalName]
	END
GO
