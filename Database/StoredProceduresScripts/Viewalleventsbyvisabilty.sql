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

print '' print '*** creting sp_select_all_events_by_EventVisible'
GO

CREATE PROCEDURE [dbo].[sp_select_all_events_by_EventVisible]
AS

	BEGIN
		SELECT [EventId],[EventTypeId],[ShelterId],[EventTitle],
		[EventDescription],[EventStart],[EventEnd],[EventAddress],[EventZipcode],
		[EventVisible],[Zipcode]	
		FROM Event
		WHERE EventVisible = 1
	END
GO
