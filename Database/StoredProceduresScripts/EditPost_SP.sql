USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/03/24

Description:
File containing the stored procedures for EditPost use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_update_post'
GO
Create procedure [dbo].[sp_update_post]
(
	@PostId				[int],
    @PostContent		[nvarchar](250),
    @PostDate			[datetime],
    @OldPostContent		[nvarchar](250)
)
AS
	BEGIN
		Update 	[Post]
		Set	   	[PostContent] = @PostContent,
				[PostDate] = @PostDate
		Where	[PostId] = @PostId
				AND [PostContent] = @OldPostContent
    END
GO