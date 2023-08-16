/***************************************************************
Mads Rhea
Created: 2023/04/20

Description:
File contains 
    - a stored procedure to [sp_select_tickets_by_email].
    - a stored procedure to [sp_select_tickets_by_ticketstatusid].
    - a stored procedure to [sp_select_tickets_by_date].
    - a stored procedure to [sp_select_emails_by_tickets].
    - a stored procedure to [sp_select_all_ticketstatusid].

**************************************************************
Updated by:
Updated: 202-/--/--

Description: ---
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating [SP_SELECT_TICKETS_BY_EMAIL]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_tickets_by_email]
        (
            @Email              [nvarchar](254)
        )
        AS
            BEGIN
                SELECT 	[TicketId], [Ticket].[UsersId], [TicketStatusId], [TicketTitle], [TicketContext], [TicketDate], [TicketActive], [Users].[Email]
                FROM	[Ticket]
                JOIN    [Users]
                ON      [Ticket].[UsersId] = [Users].[UsersId]
                WHERE   [Users].[Email] = @Email
            END
        GO



/********************************/

print '' print '*** creating [SP_SELECT_TICKETS_BY_TICKETSTATUSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_tickets_by_ticketstatusid]
        (
            @TicketStatusId     [nvarchar](50)
        )
        AS
            BEGIN
                SELECT 	[TicketId], [Ticket].[UsersId], [Ticket].[TicketStatusId], [TicketTitle], [TicketContext], [TicketDate], [TicketActive], [Users].[Email]
                FROM	[Ticket]
                JOIN    [TicketStatus]
                ON      [Ticket].[TicketStatusId] = [TicketStatus].[TicketStatusId]
                JOIN    [Users]
                ON      [Ticket].[UsersId] = [Users].[UsersId]
                WHERE   [Ticket].[TicketStatusId] = @TicketStatusId
            END
        GO


/********************************/

print '' print '*** creating [SP_SELECT_TICKETS_BY_DATE]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_tickets_by_date]
        (
            @StartDate     [date],
            @EndDate       [date] = NULL
        )
        AS
            BEGIN
                SELECT 	[TicketId], [Ticket].[UsersId], [Ticket].[TicketStatusId], [TicketTitle], [TicketContext], [TicketDate], [TicketActive], [Users].[Email]
                FROM	[Ticket]
                JOIN    [TicketStatus]
                ON      [Ticket].[TicketStatusId] = [TicketStatus].[TicketStatusId]
                JOIN    [Users]
                ON      [Ticket].[UsersId] = [Users].[UsersId]
                WHERE   (@EndDate IS NULL AND (CONVERT(DATE,[TicketDate]) = @StartDate))
                OR      (CONVERT(DATE,[TicketDate]) BETWEEN @StartDate AND @EndDate)
            END
        GO


/********************************/

print '' print '*** creating [SP_SELECT_EMAILS_BY_TICKETS]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_emails_by_tickets]
        AS 
            BEGIN
            SELECT DISTINCT [Users].[Email], [Users].[UsersId]
            FROM [Ticket]
            JOIN [Users]
            ON [Users].[UsersId] = [Ticket].[UsersId]
            ORDER BY [Users].[UsersId]
            END
        GO

/********************************/

print '' print '*** creating [SP_SELECT_ALL_TICKETSTATUSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_all_ticketstatusid]
        AS
            BEGIN
                SELECT 	[TicketStatusId]
                FROM	[TicketStatus]
            END
        GO

