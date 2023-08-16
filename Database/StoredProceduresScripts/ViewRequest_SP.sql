USE [PetNet_db_am]
GO

/***************************************************************
Andrew Cromwell
Created: 2023/03/16

Description:
File containing the stored procedures for ViewRequest-614 use case
**************************************************************/

print '' print '*** creating sp_select_request_by_shelter_sent_to (Andrew Cromwell)'
GO
CREATE PROCEDURE [dbo].[sp_select_request_by_shelter_sent_to]
(
	@shelterid		int
)
AS
	BEGIN
		SELECT [Request].[RequestId], [Request].[ReceivingShelterId], [Request].[RequestedByUserId],
			[Users].[GivenName], [Users].[FamilyName], [Shelter].[ShelterName], [Request].[RequestDate],
			[Request].[Acknowledged]
		FROM [Request] JOIN [Users]
				ON [Request].[RequestedByUserId] = [Users].[UsersId]
			JOIN [Shelter] ON [Users].[ShelterId] = [Shelter].[ShelterId]
		WHERE [Request].[ReceivingShelterId] = @shelterid
	END
GO

print '' print '*** creating sp_select_RequestResourceLine_by_RequestId (Andrew Cromwell)'
GO
CREATE PROCEDURE [dbo].[sp_select_RequestResourceLine_by_RequestId]
(
	@requestid		int
)
As
	BEGIN
		SELECT [RequestResourceLine].[RequestId], [RequestResourceLine].[ItemId], 
			[RequestResourceLine].[QuantityRequested], [ShelterInventoryItem].[Quantity],
			[RequestResourceLine].[Notes]
		FROM [RequestResourceLine] JOIN [Request]
				ON [RequestResourceLine].[RequestId] = [Request].[RequestId]
			LEFT JOIN [ShelterInventoryItem] ON [Request].[ReceivingShelterId] = [ShelterInventoryItem].[ShelterId] AND [RequestResourceLine].[ItemId] = [ShelterInventoryItem].[ItemId]
		WHERE [RequestResourceLine].[RequestId] = @requestid
	END
GO
		