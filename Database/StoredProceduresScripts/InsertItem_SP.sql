USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_item;  
GO

print '' print '*** sp_insert_item'
GO
CREATE PROCEDURE [dbo].[sp_insert_item]
(
	@ItemId		nvarchar(50)
)
AS 
	BEGIN
		INSERT INTO [dbo].[Item]
		([ItemId])
		VALUES
		(@ItemId)
		RETURN @@ROWCOUNT
	END
GO