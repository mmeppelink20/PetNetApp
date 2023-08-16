/***************************************************************
Nathan
Created: 2023/04/08

Description:
File containing stored procedure for AddCategoryTag use case
**************************************************************/


USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_select_all_categories;  
GO

print '' print '*** sp_select_all_categories'
GO
CREATE PROCEDURE [dbo].[sp_select_all_categories]
AS 
	BEGIN
		SELECT CategoryID
		FROM [dbo].[Category]
	END
GO