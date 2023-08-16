/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_zipcode_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_zipcode_by_shelter_id]
(
	@ShelterId		[int],
	@oldZipcode		[char](9),
	@newZipcode		[char](9)
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET [Zipcode]		=	@newZipcode
		WHERE	@ShelterId		=	[ShelterId]
			AND	@oldZipcode		=	[Zipcode]
		RETURN	@@ROWCOUNT
	END
GO