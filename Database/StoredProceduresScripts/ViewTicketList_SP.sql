/***************************************************************
William Rients
Created: 2023/02/16
Description:
File containing stored procedure viewing all tickets
****************************************************************/

USE [PetNet_db_am]
GO

/* View all tickets */
/* William Rients */
print '' print '*** creating sp_select_all_tickets'
GO
CREATE PROCEDURE [dbo].[sp_select_all_tickets]
AS
	BEGIN
		SELECT [TicketTitle], [TicketContext], [Ticket].[UsersId], [TicketStatusId], [TicketId], [TicketDate], [TicketActive], [Users].[Email]
		FROM [Ticket] JOIN [Users]
		ON	 [Ticket].[UsersId] = [Users].[UsersId]
	END
GO