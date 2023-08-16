USE [PetNet_db_am]
GO

/***************************************************************
Andrew Schneider
Created: 2023/03/23

Description:
File containing the stored procedures for Update Campaign
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/



/* InsertCampaignUpdate stored procedure */
/* Created by Andrew Schneider on 03/23/23 */
print '' print '*** creating sp_insert_campaign_update (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_insert_campaign_update]
(
	@CampaignId					[int],
	@UpdateTitle				[nvarchar](50),
	@UpdateDescription			[nvarchar](500)
)

AS
	BEGIN
		INSERT INTO [dbo].[CampaignUpdate]
		([CampaignId], [UpdateTitle], [UpdateDescription])
		VALUES
		(@CampaignId, @UpdateTitle, @UpdateDescription)
	SELECT SCOPE_IDENTITY()
	END
GO


/* UpdateFundraisingCampaignResults stored procedure */
/* Created by Andrew Schneider on 03/23/23 */
print '' print '*** creating sp_update_fundraising_campaign_results (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_update_fundraising_campaign_results]
(
	@FundraisingCampaignId		[int],
	@ShelterId					[int],
	@OldComplete				[bit],
	@OldAmountRaised			[decimal](9,2),
	@OldNumOfAttendees			[int],
	@OldNumAnimalsAdopted		[int],
	@NewComplete				[bit],
	@NewAmountRaised			[decimal](9,2),
	@NewNumOfAttendees			[int],
	@NewNumAnimalsAdopted		[int]
)
AS
	BEGIN
		UPDATE	[FundraisingCampaign]
		SET		[Complete] 				= @NewComplete,
				[AmountRaised] 			= @NewAmountRaised,
				[NumOfAttendees] 		= @NewNumOfAttendees,
				[NumAnimalsAdopted] 	= @NewNumAnimalsAdopted
		WHERE	[FundraisingCampaignId] = @FundraisingCampaignId
		  AND	[ShelterId]				= @ShelterId
		  AND	[Complete]				= @OldComplete
		  AND	[AmountRaised] 			= @OldAmountRaised
		  AND	[NumOfAttendees] 		= @OldNumOfAttendees
		  AND	[NumAnimalsAdopted] 	= @OldNumAnimalsAdopted
		  
		RETURN @@ROWCOUNT
	END
GO	

