/*Created by Zaid Rachman*/

USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_update_shelterInventoryItem;  
GO
print '' print '*** Creating sp_update_shelterInventoryItem'
GO
CREATE PROCEDURE [dbo].[sp_update_shelterInventoryItem]
(
	@ShelterId					[int],
	@ItemId						[nvarchar](50),
	@Quantity					[int],
	@UseStatistic				[decimal](4,2),
	@LastUpdated				[date],
	@LowInventoryThreshold		[int],
	@HighInventoryThreshold		[int],
	@InTransit					[bit],
	@Urgent						[bit],
	@Processing					[bit],
	@DoNotOrder					[bit],
	@CustomFlag					[nvarchar](250),
	
	@OldQuantity					[int],
	@OldUseStatistic				[decimal](4,2),
	@OldLastUpdated					[date],
	@OldLowInventoryThreshold		[int],
	@OldHighInventoryThreshold		[int],
	@OldInTransit					[bit],
	@OldUrgent						[bit],
	@OldProcessing					[bit],
	@OldDoNotOrder					[bit],
	@OldCustomFlag					[nvarchar](250)
	
	
	
	
)
AS
	BEGIN
			UPDATE 	[ShelterInventoryItem]
			   SET 	[Quantity] = 				@Quantity,
					[UseStatistic] =			@UseStatistic,
					[LastUpdated] = 			@LastUpdated,
					[LowInventoryThreshold] = 	@LowInventoryThreshold,
					[HighInventoryThreshold] = 	@HighInventoryThreshold,
					[InTransit] = 				@InTransit,
					[Urgent] = 					@Urgent,
					[Processing] = 				@Processing,
					[DoNotOrder] = 				@DoNotOrder,
					[CustomFlag] = 				@CustomFlag
					
			WHERE	@ShelterId =					[ShelterId] 			
			  AND	@ItemId =                   	[ItemId] 				
			  AND	@OldQuantity =             		[Quantity]			
			  AND	@OldUseStatistic =          	[UseStatistic]			
			  AND	@OldLastUpdated =           	[LastUpdated]		
			  AND	@OldLowInventoryThreshold =  	[LowInventoryThreshold]	
			  AND	@OldHighInventoryThreshold =	[HighInventoryThreshold]
			  AND	@OldInTransit =             	[InTransit]			
			  AND	@OldUrgent =              		[Urgent]			
			  AND	@OldProcessing =            	[Processing]			
			  AND	@OldDoNotOrder =             	[DoNotOrder]			
			  AND	(@OldCustomFlag =             	[CustomFlag]
					OR ([CustomFlag] IS NULL AND @OldCustomFlag IS NULL))
			  
			RETURN 	@@ROWCOUNT
			  
	END
GO
