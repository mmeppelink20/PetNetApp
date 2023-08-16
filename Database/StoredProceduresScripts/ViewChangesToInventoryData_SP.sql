/***************************************************************
Andrew Cromwell
Created: 2023/03/02

Description:
File containing stored procedure for ViewChangesToInventoryData use case.
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creting sp_select_shelteritemtransaction_by_shelter_id (Andrew Cromwell)'
GO

CREATE PROCEDURE [dbo].[sp_select_shelteritemtransaction_by_shelter_id]
(
	@shelterid			int
)
AS
	BEGIN
		SELECT [ShelterItemTransaction].[ShelterItemTransactionId], [ShelterItemTransaction].[ShelterId], 
			[ShelterItemTransaction].[ItemId], [ShelterItemTransaction].[ChangedByUsersId],
			[ShelterItemTransaction].[InventoryChangeReasonId],[ShelterItemTransaction].[QuantityIncrement],
			[ShelterItemTransaction].[DateChanged], [Shelter].[ShelterName], [Users].[GivenName],
			[Users].[FamilyName], [InventoryChangeReason].[ReasonDescription]
		FROM [ShelterItemTransaction] JOIN [Shelter]
				ON [ShelterItemTransaction].[ShelterId] = [Shelter].[ShelterId]
			JOIN [Users]
				ON [ShelterItemTransaction].[ChangedByUsersId] = [Users].[UsersId]
			JOIN [InventoryChangeReason]
				ON [ShelterItemTransaction].[InventoryChangeReasonId] = [InventoryChangeReason].[InventoryChangeReasonId]
		WHERE [ShelterItemTransaction].[ShelterId] = @shelterid
	END
GO
