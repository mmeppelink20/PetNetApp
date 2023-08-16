USE [PetNet_db_am]
GO

/***************************************************************
Matthew Meppelink
Created: 2023/03/23

Description:
File containing the stored procedures for Close Ticket use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/

print '' print '*** sp_update_ticket'
GO
CREATE PROCEDURE [dbo].[sp_update_ticket]
(
	@TicketId               INT,
    @OldTicketStatusId      NVARCHAR (50),
    @NewTicketStatusId      NVARCHAR (50)
)
AS
	BEGIN
		UPDATE	[Ticket]
		   SET	[TicketStatusId]    = @NewTicketStatusId
         WHERE  [TicketId]          = @TicketId
           AND  [TicketStatusId]    = @OldTicketStatusId
		RETURN  @@ROWCOUNT
	END
GO