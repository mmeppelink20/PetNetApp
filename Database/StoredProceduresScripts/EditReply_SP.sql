USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/04/01

Description:
File containing the stored procedures for EditReply use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_reply'
GO
Create procedure [dbo].[sp_update_reply]
(
	@ReplyId			[int],
    @ReplyContent		[nvarchar](250),
    @ReplyDate			[datetime],
    @OldReplyContent	[nvarchar](250)
)
AS
	BEGIN
		Update 	[Reply]
		Set	   	[ReplyContent] = @ReplyContent,
				[ReplyDate] = @ReplyDate
		Where	[ReplyId] = @ReplyId
				AND [ReplyContent] = @OldReplyContent
    END
    RETURN @@ROWCOUNT
GO

print '' print '*** creating sp_select_reply_by_replyId'
GO
Create procedure [dbo].[sp_select_reply_by_replyId]
(
	@ReplyId		[int]
)
AS
	BEGIN
		Select 	[Reply].[ReplyId], [ReplyAuthor], [ReplyContent], [ReplyDate], [ReplyVisibility], [GivenName], [FamilyName], [Reply].[PostId]
        From	[Reply] join [Users]
					on [Reply].[ReplyAuthor] = [Users].[UsersId]
		Where	[ReplyId] = @ReplyId
    END
GO