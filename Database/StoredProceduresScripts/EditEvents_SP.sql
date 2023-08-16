USE [PetNet_db_am]
GO

/***************************************************************
Ethan Kline
Created: 2023/04/10

Description:
File containing the stored procedures for CancelEvent-301 use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_event_visibility_by_eventid ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_visibility_by_eventid]
(
	@Eventid			[int]	
)
AS
	BEGIN
		UPDATE [dbo].[Event]		
		SET [EventVisible] = 0
		WHERE [Eventid] = @Eventid

	END
GO


print '' print '*** creating sp_insert_event ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_event]
(

	@EventTypeId		[nvarchar](250),
	@ShelterId			[int],
	@EventTitle			[nvarchar](250),
	@EventDescription			[nvarchar](250),
	@EventStart			[datetime],
	@EventEnd			[datetime],
	@EventAddress			[nvarchar](250),
	@EventZipcode			[nvarchar](250),
	@Zipcode                [nvarchar](250)
)
AS
	BEGIN
		INSERT INTO [dbo].[Event]
			([EventTypeId],[ShelterId],[EventTitle],[EventDescription],[EventStart],[EventEnd],[EventAddress],[EventZipcode],[Zipcode])
		VALUES
			(@EventTypeId,@ShelterId,@EventTitle,@EventDescription,@EventStart,@EventEnd,@EventAddress,@EventZipcode,@Zipcode)
		;
	END
GO

print '' print '*** creting sp_select_all_event_types'
GO

CREATE PROCEDURE [dbo].[sp_select_all_event_types]
AS

	BEGIN
		SELECT [EventTypeId],[EventTypeDescription]	
		FROM EventType
	END
GO

print '' print '*** creting sp_select_all_shelter_id'
GO

CREATE PROCEDURE [dbo].[sp_select_all_shelter_id]
AS

	BEGIN
		SELECT [ShelterId],[ShelterName],[Address],[AddressTwo],[Zipcode],[Phone],[Email],[Areasofneed],[ShelterActive]
		FROM Shelter
	END
GO

print '' print '*** creating sp_update_event_by_eventid ***'
GO
CREATE PROCEDURE [dbo].[sp_update_event_by_eventid]
(


	@EventTypeId		[nvarchar](250),
	@ShelterId			[int],
	@EventTitle		[nvarchar](250),
	@EventDescription		[nvarchar](250),
	@EventStart		[datetime],
	@EventEnd				[datetime],
	@EventAddress	[nvarchar](250),
	@EventZipcode   [nvarchar](250),
	@EventVisible	[bit],
	@zipcode			[nvarchar](250),
	@oldEventId [int],
	@oldEventTypeId		[nvarchar](250),
	@oldEventShelterId [int],
	@oldEventTitle [nvarchar](250),
	@oldEventDescription [nvarchar](250),
	@oldEventStart [datetime],
	@oldEventEnd [datetime],
	@oldEventAddress [nvarchar](250),
	@oldEventZipcode [nvarchar](250),
	@oldEventVisible [bit],
	@oldzipcode [nvarchar](250)

)
AS
	BEGIN
		UPDATE [Event]
			SET 	[EventTypeId] 		= @EventTypeId,
					[ShelterId] = @ShelterId,
					[EventTitle] 	= @EventTitle,
					[EventDescription] 		= 	@EventDescription,
					[EventStart] 	= @EventStart,
					[EventEnd] 	= @EventEnd,
					[EventAddress] 	= @EventAddress,
					[EventZipcode] 		= @EventZipcode,
					[EventVisible] = @EventVisible,
					[zipcode] = @zipcode 
			WHERE 	@oldEventId =[EventId]
			AND     @oldEventTypeId=[EventTypeId]
			AND     @oldEventShelterId=[ShelterId]
			AND     @oldEventTitle=[EventTitle]
			AND     @oldEventDescription=[EventDescription]
			AND     @oldEventStart=[EventStart]
			AND     @oldEventEnd=[EventEnd]
			AND     @oldEventAddress=[EventAddress]
			AND     @oldEventZipcode=[EventZipcode]
			AND     @oldEventVisible=[EventVisible]
			AND     @oldzipcode=[zipcode]

	END
GO
