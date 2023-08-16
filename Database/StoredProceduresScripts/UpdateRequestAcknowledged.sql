USE [PetNet_db_am]
GO

/***************************************************************
Matthew Meppelink
Created: 2023/04/13

Description:
File containing the stored procedures for AcknowledgeRequest-351 use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/

print '' print '*** sp_update_request_acknowledged'
GO
CREATE PROCEDURE [dbo].[sp_update_request_acknowledged]
(
	@RequestId                     [int],
    @OldRequestAcknowledgment      [bit],
    @NewRequestAcknowledgment      [bit]
)

AS
	BEGIN
		UPDATE          [Request]
           SET      [Acknowledged]        = @NewRequestAcknowledgment
         WHERE      [RequestId]           = @RequestId
           AND      [Acknowledged]        = @OldRequestAcknowledgment
    RETURN @@ROWCOUNT
	END
GO
