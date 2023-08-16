/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_edit_email_by_shelter_id (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_edit_email_by_shelter_id]
(
	@ShelterId		[int],
	@newEmail		[nvarchar](254)
)
AS
	BEGIN
		UPDATE	[dbo].[Shelter]
			SET [Email]		=	@newEmail
		WHERE	@ShelterId	=	[ShelterId]
		RETURN	@@ROWCOUNT
	END
GO