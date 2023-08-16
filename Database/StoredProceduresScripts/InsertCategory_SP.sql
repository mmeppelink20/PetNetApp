/***************************************************************
Nathan
Created: 2023/04/08

Description:
File containing stored procedure for AddCategoryTag use case
**************************************************************/


USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_category;  
GO

print '' print '*** sp_insert_category'
GO
CREATE PROCEDURE [dbo].[sp_insert_category]
(
	@CategoryId     nvarchar(50)
)
AS 
	BEGIN
		INSERT INTO[dbo].[Category]
		([CategoryID])
		VALUES
		(@CategoryId)
		RETURN @@ROWCOUNT
	END
GO