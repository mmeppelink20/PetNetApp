/***************************************************************
Andrew Cromwell
Created: 2023/04/06

Description:
File containing stored procedure for InNetworkRequestCreate use case
**************************************************************/


USE [PetNet_db_am]
GO

print '' print '*** creting sp_insert_inventoryitem_request (Andrew Cromwell)'
GO

CREATE PROCEDURE [dbo].[sp_insert_inventoryitem_request]
(
	@userid			int,
	@shelterid		int	
)
AS
	BEGIN
		INSERT INTO [dbo].[Request]
			([ReceivingShelterId], [RequestedByUserId], [Acknowledged])
		VALUES
			(@shelterid, @userid, 0)
		
		SELECT SCOPE_IDENTITY()
	END
GO



print '' print '*** creating sp_insert_requestresourceline_by_requestid (Andrew Cromwell)'
GO

CREATE PROCEDURE [dbo].[sp_insert_requestresourceline_by_requestid]
(
	@requestid			int,
	@itemid 			nvarchar(50),
	@quantityRequested	int,
	@notes				nvarchar(1000)
)

AS
	BEGIN
		INSERT INTO [dbo].[RequestResourceLine]
			([RequestId], [ItemId], [QuantityRequested], [notes])
		VALUES
			(@requestid, @itemid, @quantityRequested, @notes)
	END
GO