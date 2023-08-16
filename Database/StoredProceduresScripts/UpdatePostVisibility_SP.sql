USE [PetNet_db_am]
GO

/***************************************************************
Matthew Meppelink
Created: 2023/30/23

Description:
File containing the stored procedures for DeletePost-303 use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/

print '' print '*** sp_update_post_visibility'
GO
CREATE PROCEDURE [dbo].[sp_update_post_visibility]
(
	@PostId             [int],
    @OldPostVisibility  [bit],
    @NewPostVisibility  [bit]
)

AS
	BEGIN
		UPDATE      [Post]
           SET      [PostVisibility] = @NewPostVisibility
         WHERE      [PostId]         = @PostId
           AND      [PostVisibility] = @OldPostVisibility
    RETURN @@ROWCOUNT
	END
GO
