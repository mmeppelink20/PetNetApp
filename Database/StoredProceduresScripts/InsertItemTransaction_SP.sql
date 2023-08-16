USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_item_transaction;  
GO

print '' print '*** sp_insert_item_transaction'
GO
CREATE PROCEDURE [dbo].[sp_insert_item_transaction]
(
	@ShelterId					int,			
	@ItemId						nvarchar(50),
	@ChangedByUsersId			int,		
	@InventoryChangeReasonId	nvarchar(50),
	@QuantityIncrement			int,
	@DateChanged                datetime
)
AS 
	BEGIN
		INSERT INTO [dbo].[ShelterItemTransaction]
		([ShelterId], [ItemId], [ChangedByUsersId], 
		[InventoryChangeReasonId], [QuantityIncrement], [DateChanged])
		VALUES
		(@ShelterId, @ItemId, @ChangedByUsersId, 
		@InventoryChangeReasonId, @QuantityIncrement, @DateChanged)
		RETURN @@ROWCOUNT
	END
GO