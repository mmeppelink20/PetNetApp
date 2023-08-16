USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/04/01

Description:
File containing the stored procedures for AddReply use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_reply'
GO
Create procedure [dbo].[sp_insert_reply]
(
	@ReplyAuthor	[int],
    @ReplyContent	[nvarchar](250),
    @ReplyDate		[datetime],
    @PostId			[int]
)
AS
	BEGIN
		Insert into [Reply]
			([ReplyAuthor], [ReplyContent], [ReplyDate], [PostId])
		Values
			(@ReplyAuthor, @ReplyContent, @ReplyDate, @PostId)
		SELECT SCOPE_IDENTITY()
    END
GO