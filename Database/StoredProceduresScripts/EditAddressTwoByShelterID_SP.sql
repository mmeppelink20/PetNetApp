/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_addresstwo_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_addresstwo_by_shelter_id]
(
	@ShelterId			[int],
	@newAddressTwo		[nvarchar](50)
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET	[AddressTwo]	=	@newAddressTwo
		WHERE	@ShelterId		=	[ShelterId]
		RETURN	@@ROWCOUNT
	END
GO