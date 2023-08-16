/*
CREATE TABLE [dbo].[ShelterInventoryItem] (
	
	[ShelterId]					[int]			NOT NULL,
	[ItemId]					[nvarchar](50)	NOT NULL,
	
	[Quantity]					[int]			NOT NULL DEFAULT 0,
	[UseStatistic]				[decimal](4,2)	NULL,
	[LastUpdated]				[date]			NOT NULL DEFAULT GETDATE(),
	[LowInventoryThreshold]		[int]			NULL,
	[HighInventoryThreshold]	[int]			NULL,
	
	[InTransit]					[bit]			NOT NULL DEFAULT 0,
	[Urgent]					[bit]			NOT NULL DEFAULT 0,
	[Processing]				[bit]			NOT NULL DEFAULT 0,
	[DoNotOrder]				[bit]			NOT NULL DEFAULT 0,
	[CustomFlag]				[nvarchar](250)	NULL,
	
	CONSTRAINT	[fk_ShelterInventoryItem_ShelterId] FOREIGN KEY ([ShelterId])
		REFERENCES [dbo].[Shelter]([ShelterId]),
	CONSTRAINT	[fk_ShelterInventoryItem_ItemId] FOREIGN KEY ([ItemId])
		REFERENCES [dbo].[Item]([ItemId]) ON UPDATE CASCADE,
	CONSTRAINT	[pk_ShelterInventoryItem_ShelterId_ItemId] PRIMARY KEY ([ItemId], [ShelterId])
)
GO
*/
USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_shelterinventoryitem_by_shelterId;  
GO
print '' print '*** Creating sp_select_shelterinventoryitem_by_shelterId'
GO
CREATE PROCEDURE [dbo].[sp_select_shelterinventoryitem_by_shelterId]
(
	@ShelterId					[int]
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
				[ShelterName],
				[ItemDisabled]
				
		FROM [dbo].[ShelterInventoryItem]
		JOIN [Shelter]
			ON [Shelter].[ShelterId] = [ShelterInventoryItem].[ShelterId]
		WHERE [ShelterInventoryItem].[ShelterId] = @ShelterId
		
	END
GO
