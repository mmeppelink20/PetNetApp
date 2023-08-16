USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_item_category;  
GO

print '' print '*** sp_insert_item_category'
GO
CREATE PROCEDURE [dbo].[sp_insert_item_category]
(
	@ItemId			nvarchar(50),
	@CategoryId     nvarchar(50)
)
AS 
	BEGIN
		INSERT INTO [dbo].[ItemCategory]
		([ItemID], [CategoryID])
		VALUES
		(@ItemId, @CategoryId)
		RETURN @@ROWCOUNT
	END
GO