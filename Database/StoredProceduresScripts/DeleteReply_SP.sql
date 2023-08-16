USE [PetNet_db_am]
GO

/***************************************************************
Andrew Cromwell
Created: 2023/04/14

Description:
File containing the stored procedures for Delete Reply use case
****************************************************************/

print '' print '*** creating sp_update_reply_visibility_by_replyid (Andrew Cromwell)'
GO
Create procedure [dbo].[sp_update_reply_visibility_by_replyid]
(
	@replyid		int,
	@reply_content	nvarchar(250),
    @reply_visiblility	bit
)
AS
	BEGIN
		UPDATE [Reply]
		SET [ReplyVisibility] = @reply_visiblility
		WHERE [ReplyId] = @replyid AND [ReplyContent] = @reply_content
    END
GO