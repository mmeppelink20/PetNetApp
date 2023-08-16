/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_phone_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_phone_by_shelter_id]
(
	@ShelterId		[int],
	@newPhone		[nvarchar](13)
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET [Phone]		=	@newPhone
		WHERE	@ShelterId	=	[ShelterId]
		RETURN	@@ROWCOUNT
	END
GO