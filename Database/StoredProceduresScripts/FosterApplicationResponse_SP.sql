print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_update_foster_application_status_from_response'
GO
CREATE PROCEDURE [dbo].[sp_update_foster_application_status_from_response]
(
	@thisFosterApplicationId		int,
	@thisApproved					bit
)
AS

	IF(@thisApproved = 1)BEGIN
		UPDATE [dbo].[FosterApplication]
		SET
			ApplicationStatusId = 'Approved'
		WHERE
			FosterApplicationId	= @thisFosterApplicationId
		RETURN @@ROWCOUNT
	END
	ELSE IF(@thisApproved = 0)BEGIN
		UPDATE [dbo].[FosterApplication]
		SET
			ApplicationStatusId = 'Denied'
		WHERE
			FosterApplicationId	= @thisFosterApplicationId
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_insert_foster_application_response_by_foster_application_id'
GO
CREATE PROCEDURE [dbo].[sp_insert_foster_application_response_by_foster_application_id]
(
	@FosterApplicationId			int,
	@UsersId						int,
	@Approved						bit,
	@FosterApplicationResponseNotes	nvarchar(500)
)
AS
	BEGIN
		EXEC [dbo].[sp_update_foster_application_status_from_response]
			@thisFosterApplicationId = @FosterApplicationId,
			@thisApproved = @Approved
		INSERT INTO [dbo].[FosterApplicationResponse]
			([FosterApplicationId], [UsersId], [Approved], [FosterApplicationResponseNotes])
		VALUES
			(@FosterApplicationId, @UsersId, @Approved, @FosterApplicationResponseNotes)
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_update_foster_application_response'
GO
CREATE PROCEDURE [dbo].[sp_update_foster_application_response]
(
	@FosterApplicationResponseId		int,
	@FosterApplicationId				int,
	@UsersId							int,
	
	@OldApproved						bit,
	@OldFosterApplicationResponseNotes	nvarchar(500),
	
	@NewApproved						bit,
	@NewFosterApplicationResponseNotes	nvarchar(500)
)
AS
	BEGIN
		EXEC [dbo].[sp_update_foster_application_status_from_response]
			@thisFosterApplicationId = @FosterApplicationId,
			@thisApproved = @NewApproved
		UPDATE [dbo].[FosterApplicationResponse]
		SET
			Approved								= @NewApproved,
			FosterApplicationResponseNotes			= @NewFosterApplicationResponseNotes
		WHERE
			FosterApplicationResponseId				= @FosterApplicationResponseId
			AND FosterApplicationId					= @FosterApplicationId
			AND UsersId								= @UsersId
			
			AND Approved							= @OldApproved
			AND (FosterApplicationResponseNotes		IS NULL AND @OldFosterApplicationResponseNotes IS NULL) OR (FosterApplicationResponseNotes = @oldFosterApplicationResponseNotes)
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_select_foster_application_response_by_foster_application_id'
GO
CREATE PROCEDURE [dbo].[sp_select_foster_application_response_by_foster_application_id]
(
	@FosterApplicationId				int
)
AS
	BEGIN
		SELECT FosterApplicationResponseId, FosterApplicationResponse.FosterApplicationId, FosterApplicationResponse.UsersId, 
			Approved, FosterApplicationResponseDate, FosterApplicationResponseNotes,
			FosterApplication1.ApplicantId, Users1.GivenName, Users1.FamilyName
		FROM dbo.FosterApplicationResponse
			JOIN FosterApplication FosterApplication1 ON FosterApplicationResponse.FosterApplicationId = FosterApplication1.FosterApplicationId
			JOIN Applicant Applicant1 ON FosterApplication1.ApplicantId = Applicant1.ApplicantId
			JOIN Users Users1 ON Applicant1.UsersId = Users1.UsersId
		WHERE @FosterApplicationId = FosterApplicationResponse.FosterApplicationId
	END
GO



/*
-- for testing
print '' print '*** EXEC sp_insert_foster_application_response_by_foster_application_id'
GO
EXEC sp_insert_foster_application_response_by_foster_application_id
	@FosterApplicationId				= 100000,
	@UsersId							= 100000,
	@Approved							= 1,
	@FosterApplicationResponseNotes		= "hello"
GO


print '' print '*** EXEC sp_update_foster_application_response'
GO
EXEC sp_update_foster_application_response
	@FosterApplicationResponseId			= 100000,
	@FosterApplicationId					= 100000,
    @UsersId								= 100000,
    
    @OldApproved							= 1,
    @OldFosterApplicationResponseNotes		= "hello",
    
    @NewApproved							= 0,
    @NewFosterApplicationResponseNotes		= "goodbye"
GO


print '' print '*** EXEC sp_select_foster_application_response_by_foster_application_id'
GO
EXEC sp_select_foster_application_response_by_foster_application_id
	@FosterApplicationId				= 100000
GO
*/