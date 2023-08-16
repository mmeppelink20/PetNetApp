USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_delete_item_category;  
GO

print '' print '*** sp_delete_item_category'
GO
CREATE PROCEDURE [dbo].[sp_delete_item_category]
(
	@ItemId			nvarchar(50),
	@CategoryId     nvarchar(50)
)
AS 
	BEGIN
		DELETE FROM [dbo].[ItemCategory]
		WHERE @ItemId = [ItemCategory].[ItemId]
		AND @CategoryId = [ItemCategory].[CategoryId]
		RETURN @@ROWCOUNT
	END
GO