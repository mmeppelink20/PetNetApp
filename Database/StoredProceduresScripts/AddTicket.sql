/***************************************************************
William Rients
Created: 2023/02/16
Description:
File containing stored procedure inserting a new ticket
****************************************************************/

USE [PetNet_db_am]
GO

/* View all tickets */
/* William Rients */
print '' print '*** creating sp_insert_ticket'
GO
CREATE PROCEDURE [dbo].[sp_insert_ticket]
(
	@UserId				[int],
	@TicketStatusId		[nvarchar](50),
	@TicketTitle        [nvarchar](500),	
	@TicketContext      [nvarchar](500)	
)
AS
	BEGIN
		INSERT INTO [Ticket]
			([UsersId], [TicketStatusId], [TicketTitle], [TicketContext])
		VALUES
			(@UserId, @TicketStatusId, @TicketTitle, @TicketContext)
		RETURN @@ROWCOUNT
	END
GO