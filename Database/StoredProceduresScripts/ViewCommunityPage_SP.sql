USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/03/24

Description:
File containing the stored procedures for ViewCommunityPage use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/
print '' print '*** creating sp_select_all_posts'
GO
Create procedure [dbo].[sp_select_all_posts]
AS
	BEGIN
		Select 	[Post].[PostId], [PostAuthor], [PostContent], [PostDate], [PostVisibility], [GivenName], [FamilyName]
        From	[Post] join [Users]
					on [Post].[PostAuthor] = [Users].[UsersId]
		Where	[PostAdminRemoved] IS NULL OR [PostAdminRemoved] = 1
		Order By	[PostDate] DESC
    END
GO

print '' print '*** creating sp_select_active_posts'
GO
Create procedure [dbo].[sp_select_active_posts]
AS
	BEGIN
		Select 	[Post].[PostId], [PostAuthor], [PostContent], [PostDate], [PostVisibility], [GivenName], [FamilyName]
        From	[Post] join [Users]
					on [Post].[PostAuthor] = [Users].[UsersId]
        Where	[PostVisibility] = 1
        Order By	[PostDate] DESC
    END
GO

print '' print '*** creating sp_select_all_replies_by_postid'
GO
Create procedure [dbo].[sp_select_all_replies_by_postid]
(
	@PostId		[int]
)
AS
	BEGIN
		Select 	[ReplyId], [PostId], [ReplyAuthor], [ReplyContent], [ReplyDate], [ReplyVisibility], [GivenName], [FamilyName]
        From	[Reply] join [Users]
					on [Reply].[ReplyAuthor] = [Users].[UsersId]
        Where	[PostId] = @PostId AND ([ReplyAdminRemoved] IS NULL OR [ReplyAdminRemoved] = 1)
        Order By	[ReplyDate] DESC
    END
GO

print '' print '*** creating sp_select_active_replies_by_postid'
GO
Create procedure [dbo].[sp_select_active_replies_by_postid]
(
	@PostId		[int]
)
AS
	BEGIN
		Select 	[ReplyId], [PostId], [ReplyAuthor], [ReplyContent], [ReplyDate], [ReplyVisibility], [GivenName], [FamilyName]
        From	[Reply] join [Users]
					on [Reply].[ReplyAuthor] = [Users].[UsersId]
        Where	[ReplyVisibility] = 1 AND [PostId] = @PostId
        Order By	[ReplyDate] DESC
    END
GO

print '' print '*** creating sp_select_post_by_postId'
GO
Create procedure [dbo].[sp_select_post_by_postId]
(
	@PostId		[int]
)
AS
	BEGIN
		Select 	[Post].[PostId], [PostAuthor], [PostContent], [PostDate], [PostVisibility], [GivenName], [FamilyName]
        From	[Post] join [Users]
					on [Post].[PostAuthor] = [Users].[UsersId]
		Where	[PostId] = @PostId
    END
GO

print '' print '*** creating sp_select_count_active_replies_by_postId'
GO
Create procedure [dbo].[sp_select_count_active_replies_by_postId]
(
	@PostId		[int]
)
AS
	BEGIN
		Select 	Count([ReplyId])
        From	[Reply]
        Where	[ReplyVisibility] = 1 AND [PostId] = @PostId
    END
GO

print '' print '*** creating sp_select_count_replies_by_postId'
GO
Create procedure [dbo].[sp_select_count_replies_by_postId]
(
	@PostId		[int]
)
AS
	BEGIN
		Select 	Count([ReplyId])
        From	[Reply]
        Where	[PostId] = @PostId
    END
GO
