print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_update_adoption_application_status_from_response'
GO
CREATE PROCEDURE [dbo].[sp_update_adoption_application_status_from_response]
(
	@thisAdoptionApplicationId		int,
	@thisApproved					bit
)
AS

	IF(@thisApproved = 1)BEGIN
		UPDATE [dbo].[AdoptionApplication]
		SET
			ApplicationStatusId = 'Approved'
		WHERE
			AdoptionApplicationId	= @thisAdoptionApplicationId
		RETURN @@ROWCOUNT
	END
	ELSE IF(@thisApproved = 0)BEGIN
		UPDATE [dbo].[AdoptionApplication]
		SET
			ApplicationStatusId = 'Denied'
		WHERE
			AdoptionApplicationId	= @thisAdoptionApplicationId
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_insert_adoption_application_response_by_adoption_application_id'
GO
CREATE PROCEDURE [dbo].[sp_insert_adoption_application_response_by_adoption_application_id]
(
	@AdoptionApplicationId			int,
	@UsersId						int,
	@Approved						bit,
	@AdoptionApplicationResponseNotes	nvarchar(500)
)
AS
	BEGIN
		EXEC [dbo].[sp_update_adoption_application_status_from_response]
			@thisAdoptionApplicationId = @AdoptionApplicationId,
			@thisApproved = @Approved
		INSERT INTO [dbo].[AdoptionApplicationResponse]
			([AdoptionApplicationId], [UsersId], [Approved], [AdoptionApplicationResponseNotes])
		VALUES
			(@AdoptionApplicationId, @UsersId, @Approved, @AdoptionApplicationResponseNotes)
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_update_adoption_application_response'
GO
CREATE PROCEDURE [dbo].[sp_update_adoption_application_response]
(
	@AdoptionApplicationResponseId		int,
	@AdoptionApplicationId				int,
	@UsersId							int,
	
	@OldApproved						bit,
	@OldAdoptionApplicationResponseNotes	nvarchar(500),
	
	@NewApproved						bit,
	@NewAdoptionApplicationResponseNotes	nvarchar(500)
)
AS
	BEGIN
		EXEC [dbo].[sp_update_adoption_application_status_from_response]
			@thisAdoptionApplicationId = @AdoptionApplicationId,
			@thisApproved = @NewApproved
		UPDATE [dbo].[AdoptionApplicationResponse]
		SET
			Approved								= @NewApproved,
			AdoptionApplicationResponseNotes			= @NewAdoptionApplicationResponseNotes
		WHERE
			AdoptionApplicationResponseId				= @AdoptionApplicationResponseId
			AND AdoptionApplicationId					= @AdoptionApplicationId
			AND UsersId								= @UsersId
			
			AND Approved							= @OldApproved
			AND (AdoptionApplicationResponseNotes		IS NULL AND @OldAdoptionApplicationResponseNotes IS NULL) OR (AdoptionApplicationResponseNotes = @oldAdoptionApplicationResponseNotes)
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_select_adoption_application_response_by_adoption_application_id'
GO
CREATE PROCEDURE [dbo].[sp_select_adoption_application_response_by_adoption_application_id]
(
	@AdoptionApplicationId				int
)
AS
	BEGIN
		SELECT AdoptionApplicationResponseId, AdoptionApplicationResponse.AdoptionApplicationId, AdoptionApplicationResponse.UsersId, 
			Approved, AdoptionApplicationResponseDate, AdoptionApplicationResponseNotes,
			AdoptionApplication1.ApplicantId, Users1.GivenName, Users1.FamilyName
		FROM dbo.AdoptionApplicationResponse
			JOIN AdoptionApplication AdoptionApplication1 ON AdoptionApplicationResponse.AdoptionApplicationId = AdoptionApplication1.AdoptionApplicationId
			JOIN Users Users1 ON AdoptionApplication1.ApplicantId = Users1.UsersId
		WHERE @AdoptionApplicationId = AdoptionApplicationResponse.AdoptionApplicationId
	END
GO



/*
-- for testing
print '' print '*** EXEC sp_insert_adoption_application_response_by_adoption_application_id'
GO
EXEC sp_insert_adoption_application_response_by_adoption_application_id
	@AdoptionApplicationId				= 100000,
	@UsersId							= 100000,
	@Approved							= 1,
	@AdoptionApplicationResponseNotes		= "hello"
GO


print '' print '*** EXEC sp_update_adoption_application_response'
GO
EXEC sp_update_adoption_application_response
	@AdoptionApplicationResponseId			= 100000,
	@AdoptionApplicationId					= 100000,
    @UsersId								= 100000,
    
    @OldApproved							= 1,
    @OldAdoptionApplicationResponseNotes		= "hello",
    
    @NewApproved							= 0,
    @NewAdoptionApplicationResponseNotes		= "goodbye"
GO


print '' print '*** EXEC sp_select_adoption_application_response_by_adoption_application_id'
GO
EXEC sp_select_adoption_application_response_by_adoption_application_id
	@AdoptionApplicationId				= 100000
GO
*/