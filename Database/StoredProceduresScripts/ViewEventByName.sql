/* Created By: Ethan Kline */ 

USE [PetNet_db_am]

print '' print '*** creating sp_select_all_fundraising_sponsors_By_name'

GO
CREATE PROCEDURE [dbo].[sp_select_all_fundraising_sponsors_By_name]
(
	@CompanyName	[nvarchar](25)
)
AS
	BEGIN

SELECT  FundraisingEvent.FundraisingEventId, 
        FundraisingEvent.Title, 
        FundraisingEvent.StartTime, 
        InstitutionalEntity.InstitutionalEntityId,
        InstitutionalEntity.ContactType,
        InstitutionalEntity.CompanyName, 
        InstitutionalEntity.Address
    FROM InstitutionalEntity 
    JOIN FundraisingEventEntity
        ON  InstitutionalEntity.InstitutionalEntityId = FundraisingEventEntity.ContactId 
    JOIN FundraisingEvent
        ON FundraisingEventEntity.EventId = FundraisingEvent.FundraisingEventId
WHERE CompanyName = @CompanyName  AND ContactType = 'Sponsor';

	
	END
GO


