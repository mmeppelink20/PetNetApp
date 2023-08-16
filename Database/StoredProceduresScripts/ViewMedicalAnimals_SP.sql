USE [PetNet_db_am]
GO

/***************************************************************
Matthew Meppelink
Created: 2023/02/10

Description:
File containing the stored procedures for ViewMedicalAnimals-000 use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_all_animals'
DROP PROCEDURE IF EXISTS [dbo].[sp_select_all_animals]  
GO
CREATE PROCEDURE [dbo].[sp_select_all_animals]
(
	@AnimalName			[nvarchar](50)
)
AS
	BEGIN
		SELECT  [Animal].[AnimalId], [Animal].[AnimalName] AS 'Animal Name'
        FROM    [dbo].[Animal]
		WHERE   [AnimalName] LIKE '%' + @AnimalName + '%'
	END
GO