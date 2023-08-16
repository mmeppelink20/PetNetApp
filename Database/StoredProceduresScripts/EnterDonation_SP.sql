print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO

/* Created By: Asa Armstrong */
print '' print '*** Creating sp_insert_donation'
GO
CREATE PROCEDURE [dbo].[sp_insert_donation]
	@UsersId				int,
	@ShelterId				int,
	@Amount					decimal(7,2),
	@Message				nvarchar(255),
	@GivenName				nvarchar(50),
	@FamilyName				nvarchar(50),
	@HasInKindDonation		bit,
	@Anonymous				bit,
	@Target					nvarchar(255),
	@PaymentMethod			nvarchar(50),
	@ScheduledDonationId	int,
	@FundraisingEventId		int,
	@Email					nvarchar(254),
	@Phone					nvarchar(13)
AS
	BEGIN
		INSERT INTO [dbo].[Donation]
			(UsersId, ShelterId, Amount, Message, GivenName, FamilyName, HasInKindDonation,
			Anonymous, Target, PaymentMethod, ScheduledDonationId, FundraisingEventId, Email, Phone)
		VALUES
			(@UsersId, @ShelterId, @Amount, @Message, @GivenName, @FamilyName, @HasInKindDonation,
			@Anonymous, @Target, @PaymentMethod, @ScheduledDonationId, @FundraisingEventId, @Email, @Phone)
		SELECT SCOPE_IDENTITY()
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_insert_in_kind'
GO
CREATE PROCEDURE [dbo].[sp_insert_in_kind]
	@DonationId				int,
	@Description			nvarchar(255),
	@Quantity				int,
	@Target					nvarchar(255),
	@Received				bit
AS
	BEGIN
		INSERT INTO [dbo].[InKind]
			(DonationId, Description, Quantity, Target, Received)
		VALUES
			(@DonationId, @Description, @Quantity, @Target, @Received)
		RETURN @@ROWCOUNT
	END
GO

/*
-- for testing
print '' print '*** EXEC sp_insert_donation'
GO
EXEC	[dbo].[sp_insert_donation]
		@UsersId = 100000,
		@ShelterId = 100000,
		@Amount = 86753.09,
		@Message = N'hi',
		@GivenName = N'Asa',
		@FamilyName = N'A',
		@HasInKindDonation = 0,
		@Anonymous = 1,
		@Target = N'me',
		@PaymentMethod = N'Card',
		@ScheduledDonationId = NULL,
		@FundraisingEventId = NULL,
		@Email = null,
		@Phone = null
GO

print '' print '*** EXEC sp_insert_in_kind'
GO
EXEC	[dbo].[sp_insert_in_kind]
		@DonationId = 100000,
		@Description = "desc",
		@Quantity = 5,
		@Target = "target",
		@Received = 1
GO
*/