/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the ShelterNetwork use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_select_shelter_all (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_select_shelter_all]
AS
	BEGIN
		SELECT
			[ShelterId],
			[ShelterName],
			[Address],
			[AddressTwo],
			[Zipcode],
			[Phone],
			[Email],
			[Areasofneed],
			[ShelterActive]
		FROM [dbo].[Shelter]
		ORDER BY [ShelterId] DESC
	END
GO