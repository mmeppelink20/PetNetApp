
USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_shelterinventoryitem_by_shelterId_and_itemId;  
GO
print '' print '*** Creating sp_select_shelterinventoryitem_by_shelterId_and_itemId'
GO
CREATE PROCEDURE [dbo].[sp_select_shelterinventoryitem_by_shelterId_and_itemId]
(
	@ShelterId					[int],
	@ItemId						[nvarchar](50)
	
)
AS
	BEGIN
		SELECT 	[ShelterInventoryItem].[ShelterId],
				[ShelterInventoryItem].[ItemId],
				[Quantity],
				[UseStatistic],
				[LastUpdated],
				[LowInventoryThreshold],
				[HighInventoryThreshold],
				[InTransit],
				[Urgent],
				[Processing],
				[DoNotOrder],
				[CustomFlag],
				[ShelterName]
				
				
				
		FROM [dbo].[ShelterInventoryItem]
		JOIN [Shelter]
			ON [Shelter].[ShelterId] = [ShelterInventoryItem].[ShelterId]
		WHERE [ShelterInventoryItem].[ShelterId] = @ShelterId
		AND	  [ShelterInventoryItem].[ItemId] = @ItemId
		
	END
GO
