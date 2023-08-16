/***************************************************************
Barry Mikulas
Created: 2023/04/03
Description:
File containing stored procedure update fundraising event
****************************************************************/

USE [PetNet_db_am]
GO


print '' print '*** creating sp_select_fundraising_event_by_fundraising_event_id'

GO
CREATE PROCEDURE [dbo].[sp_select_fundraising_event_by_fundraising_event_id]
(
	@FundraisingEventId	[int]
)
AS
	BEGIN
		SELECT [FundraisingEventId], [UsersId], [ImageId], [CampaignId], [ShelterId], [Title], [StartTime], [EndTime], [Hidden], [Complete], [Description], [AdditionalInfo], [Cost], [NumOfAttendees], [NumAnimalsAdopted], [UpdateNotes]
		FROM [FundraisingEvent]
		WHERE @FundraisingEventId = [FundraisingEventId]
	END
GO


print '' print '*** creating sp_update_fundraising_event_results'

GO
CREATE PROCEDURE [dbo].[sp_update_fundraising_event_results]
(
	@FundraisingEventId		    [int],
	@OldComplete				[bit],
	@OldCost					[decimal](9,2),
	@OldNumOfAttendees			[int],
	@OldNumAnimalsAdopted		[int],
	@OldUpdateNotes				[nvarchar](max),
	@NewComplete				[bit],
	@NewCost					[decimal](9,2),
	@NewNumOfAttendees			[int],
	@NewNumAnimalsAdopted		[int],
	@NewUpdateNotes				[nvarchar](max)
)
AS
	BEGIN
		UPDATE	[FundraisingEvent]
		SET		[Complete] 				= @NewComplete,
				[Cost]		 			= @NewCost,
				[NumOfAttendees] 		= @NewNumOfAttendees,
				[NumAnimalsAdopted] 	= @NewNumAnimalsAdopted,
				[UpdateNotes]			= @NewUpdateNotes
		WHERE	[FundraisingEventId] = @FundraisingEventId
		  AND	[Complete]				= @OldComplete
		  AND	[Cost] 					= @OldCost
		  AND	[NumOfAttendees] 		= @OldNumOfAttendees
		  AND	[NumAnimalsAdopted] 	= @OldNumAnimalsAdopted
		  AND	(([UpdateNotes]			= @OldUpdateNotes) OR ([UpdateNotes] IS NULL AND @OldUpdateNotes IS NULL))
		  
		RETURN @@ROWCOUNT
	END
GO