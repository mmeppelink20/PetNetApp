USE [PetNet_db_am]
GO

PRINT '' PRINT '*** creating sp_select_user_post_reported_by_postId_and_userId'
GO

CREATE PROCEDURE [dbo].[sp_select_user_post_reported_by_postId_and_userId]
(
	@PostId	[INT],
	@UserId	[INT]
)
AS
	BEGIN
		SELECT COUNT(*)
		FROM	[dbo].[PostReport]
		WHERE	[PostId] = @PostId
		AND		[PostReporter] = @UserId
	END
GO

PRINT '' PRINT '*** creating sp_select_report_messages'
GO

CREATE PROCEDURE [dbo].[sp_select_report_messages]
AS
	BEGIN
		SELECT [ReportMessageId], [ReportMessageDescription]
		FROM	[dbo].[ReportMessage]
	END
GO


PRINT '' PRINT '*** creating sp_delete_post_report'
GO

CREATE PROCEDURE [dbo].[sp_delete_post_report]
(
	@PostReporter	[INT],
	@PostId	[INT]
)
AS
	BEGIN
		DELETE
		FROM	[dbo].[PostReport]
		WHERE	[PostId] = @PostId
		AND		[PostReporter] = @PostReporter
	END
GO

PRINT '' PRINT '*** creating sp_insert_post_report'
GO

CREATE PROCEDURE [dbo].[sp_insert_post_report]
(
	@PostReporter	[INT],
	@PostId	[INT],
	@ReportMessageId	[INT]
)
AS
	BEGIN
		INSERT INTO [dbo].[PostReport]
		( [PostId], [PostReporter], [ReportMessageId])
		VALUES
		(@PostId, @PostReporter, @ReportMessageId)
	END
GO