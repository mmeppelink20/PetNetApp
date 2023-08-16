print '' print '*** Using PetNet_db_am'
USE [PetNet_db_am]
GO

/*
This stored procedure returns all the items in the Inventory Library, with there tags concatenated together.
Item tags are concatenated with the | (pipe) character, so can be isolated with a String.Split(|)
Logic adapted from https://stackoverflow.com/questions/194852/how-to-concatenate-text-from-multiple-rows-into-a-single-text-string-in-sql-serv/42778050#42778050
Updated 2023/04/01 (April first 2023)
Stored Procedure now uses a left join in order to make sure that ItemIDs without any corresponding CategoryIDs show up as NULL.
*/
print '' print '*** Creating sp_select_all_library_items (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_select_all_library_items]
AS
	BEGIN
		SELECT	[Item].[ItemID], STRING_AGG([CategoryID], '|') WITHIN GROUP(ORDER BY [CategoryID] ASC) AS 'Tags'
		FROM	[dbo].[Item] LEFT JOIN [dbo].[ItemCategory]
			ON	[Item].[ItemID] = [ItemCategory].[ItemID]
		GROUP BY [Item].[ItemID];
	END
GO


/* Library <=> Shelter inventory interaction */

/*
Add a new Inventory Item to a shelter's inventory by creating an instance of a Library item
*/
print '' print '*** Creating sp_insert_shelterinventoryitem_from_library (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_insert_shelterinventoryitem_from_library]
(
	@ShelterId	[int],
	@ItemId		[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[ShelterInventoryItem]
			([ShelterId],	[ItemId],	[UseStatistic],	[LowInventoryThreshold],	[HighInventoryThreshold])
		VALUES
			(@ShelterId,	@ItemId,	0,				0,							0)
		RETURN	@@ROWCOUNT
	END
GO

/*
This stored procedures enables or disables a ShelterInventoryItem's visibility from a shelter's inventory.
ShelterInventoryItems must be disabled rather than deleted to avoid data integrity issues with things like inventory change histories.
*/
print '' print '*** Creating sp_update_shelterinventoryitem_enabled_disabled (Brian Collum)'
GO
CREATE PROCEDURE [dbo].[sp_update_shelterinventoryitem_enabled_disabled]
(
	@ShelterId					[int],
	@ItemId						[nvarchar](50),
	@ItemDisabled				[bit]
)
AS
	BEGIN
			UPDATE 	[ShelterInventoryItem]
			   SET 	[ItemDisabled]	=	@ItemDisabled
			WHERE	[ShelterId]		=	@ShelterId
			  AND	[ItemId]		=	@ItemId
	END
GO
