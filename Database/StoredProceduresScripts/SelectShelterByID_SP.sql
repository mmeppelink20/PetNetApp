/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_select_shelter_by_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_select_shelter_by_id]
(
	@ShelterId		[int]
)
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
		FROM	[dbo].[Shelter]
		WHERE	@ShelterId		=	[ShelterId]
		ORDER BY [ShelterId] DESC
	END
GO