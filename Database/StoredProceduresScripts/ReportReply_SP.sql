USE [PetNet_db_am]
GO

PRINT '' PRINT '*** creating sp_select_user_reply_reported_by_replyId_and_userId'
GO

CREATE PROCEDURE [dbo].[sp_select_user_reply_reported_by_replyId_and_userId]
(
	@ReplyId	[INT],
	@UserId	[INT]
)
AS
	BEGIN
		SELECT COUNT(*)
		FROM	[dbo].[ReplyReport]
		WHERE	[ReplyId] = @ReplyId
		AND		[ReplyReporter] = @UserId
	END
GO


PRINT '' PRINT '*** creating sp_delete_reply_report'
GO

CREATE PROCEDURE [dbo].[sp_delete_reply_report]
(
	@ReplyReporter	[INT],
	@ReplyId	[INT]
)
AS
	BEGIN
		DELETE
		FROM	[dbo].[ReplyReport]
		WHERE	[ReplyId] = @ReplyId
		AND		[ReplyReporter] = @ReplyReporter
	END
GO

PRINT '' PRINT '*** creating sp_insert_reply_report'
GO

CREATE PROCEDURE [dbo].[sp_insert_reply_report]
(
	@ReplyReporter	[INT],
	@ReplyId	[INT],
	@ReportMessageId	[INT]
)
AS
	BEGIN
		INSERT INTO [dbo].[ReplyReport]
		( [ReplyId], [ReplyReporter], [ReportMessageId])
		VALUES
		(@ReplyId, @ReplyReporter, @ReportMessageId)
	END
GO