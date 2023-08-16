/***************************************************************
Ethan Kline
Created: 2023/04/1

Description:
File containing stored procedure for AddExamNotes use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creting sp_view_all_events'
GO

CREATE PROCEDURE [dbo].[sp_select_all_events]
AS
	BEGIN
		SELECT [EventId],[EventTypeId],[ShelterId],[EventTitle],
		[EventDescription],[EventStart],[EventEnd],[EventAddress],[EventZipcode],
		[EventVisible]	
		FROM Event
	END
GO
