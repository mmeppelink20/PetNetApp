/***************************************************************
William Rients
Created: 2023/03/29
Description:
File containing stored procedure for viewing a list of pledgers
from a specific event, viewing a specific pledger from an event,
and inserting a new pledger for a specific event
****************************************************************/

/* Select Pledgers by EventId */
/* William Rients */

USE [PetNet_db_am]
GO

print '' print '*** creating sp_select_pledgers_by_fundraising_event_id'
GO
CREATE PROCEDURE [dbo].[sp_select_pledgers_by_fundraising_event_id]
(
	@EventId	int
)
AS
	BEGIN
		SELECT 	[Pledge].[PledgeId], [Pledge].[DonationId], [Pledge].[UsersId], [Pledge].[FundraisingEventId], 
		[Pledge].[Date], [Pledge].[Amount], [Pledge].[Message], [Pledge].[Target], 
		[Pledge].[GivenName], [Pledge].[FamilyName], [Pledge].[Phone], [Pledge].[Email], 
		[Donation].[Amount] AS 'DonationAmount'
		FROM	[Pledge] LEFT JOIN [Donation]
				ON	[Pledge].[DonationId] = [Donation].[DonationId]
		WHERE	@EventId = [Pledge].[FundraisingEventId]
	END
GO

/* Select Pledger by UserId */
/* William Rients */

print '' print '*** creating sp_select_pledger_by_fundraising_user_id'
GO
CREATE PROCEDURE [dbo].[sp_select_pledger_by_fundraising_user_id]
(
	@UserId	int
)
AS
	BEGIN
		SELECT 	[Pledge].[PledgeId], [Pledge].[DonationId], [Pledge].[UsersId], [Pledge].[FundraisingEventId], 
		[Pledge].[Date], [Pledge].[Amount], [Pledge].[Message], [Pledge].[GivenName], 
		[Pledge].[FamilyName], [Pledge].[Phone], [Pledge].[Email], 
		[Donation].[Amount] AS 'DonationAmount', [Donation].[Date] AS 'DonationDate'
		FROM	[Pledge] LEFT JOIN [Donation]
				ON	[Pledge].[DonationId] = [Donation].[DonationId]
		WHERE	@UserId = [Pledge].[UsersId]
	END
GO

USE [PetNet_db_am]
GO

/* Insert pledger */
/* William Rients */
print '' print '*** creating sp_insert_pledger'
GO
CREATE PROCEDURE [dbo].[sp_insert_pledger]
(
	@UsersId					[int],
	@FundraisingEventId			[int],
	@Amount						[decimal](7,2),
	@Target						[nvarchar](255),
	@Requirement				[nvarchar](255),
	@Message					[nvarchar](255),
	@GivenName					[nvarchar](50),
	@FamilyName					[nvarchar](50),
	@Phone						[nvarchar](13),
	@Email						[nvarchar](254),
	@IsContactPreferencePhone	[bit]
	
)
AS
	BEGIN
		INSERT INTO [Pledge]
			([UsersId], [FundraisingEventId], [Amount], [Target], [Requirement], 
			 [Message], [GivenName], [FamilyName], [Phone], [Email], [IsContactPreferencePhone])
		VALUES
			(@UsersId, @FundraisingEventId, @Amount, @Target, @Requirement, @Message, 
			@GivenName, @FamilyName, @Phone, @Email, @IsContactPreferencePhone)
		RETURN @@ROWCOUNT
	END
GO