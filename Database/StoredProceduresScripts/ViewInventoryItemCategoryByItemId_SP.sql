USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_categoryId_by_itemId;  
GO
print '' print '*** Creating sp_select_categoryid_by_itemId'
GO
CREATE PROCEDURE [dbo].[sp_select_categoryid_by_itemId]
(
	@ItemId					[nvarchar](50)
)
AS
	BEGIN
		SELECT 	[ItemId],
				[CategoryId]		
		FROM [dbo].[ItemCategory]
		WHERE [ItemId] = @ItemId
		
	END
GO
