print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO

/* Created By: Asa Armstrong */
print '' print '*** Creating sp_insert_animal_death'
GO
CREATE PROCEDURE [dbo].[sp_insert_animal_death]
(
	@UsersId 				int,
	@AnimalId 				int,
	@DeathDate 				datetime,
	@DeathCause 			nvarchar(100),
	@DeathDisposal 			nvarchar(100),
	@DeathDisposalDate 		datetime,
	@DeathNotes 			nvarchar(500)
)
AS
	BEGIN
		INSERT INTO [dbo].[Death]
			([UsersId], [AnimalId], [DeathDate], [DeathCause], [DeathDisposal], [DeathDisposalDate], [DeathNotes])
		VALUES
			(@UsersId, @AnimalId, @DeathDate, @DeathCause, @DeathDisposal, @DeathDisposalDate, @DeathNotes)
		RETURN @@ROWCOUNT
	END
GO


/* Created By: Asa Armstrong  */
print '' print '*** Creating sp_select_animal_death'
GO
CREATE PROCEDURE [dbo].[sp_select_animal_death]
(
	@AnimalId				int
)
AS
	BEGIN
		SELECT DeathId, Death.UsersId, Death.AnimalId, DeathDate, DeathCause, DeathDisposal, DeathDisposalDate, DeathNotes, Animal1.AnimalName
		FROM [dbo].[Death]
			JOIN Animal Animal1 ON Death.AnimalId = Animal1.AnimalId
		WHERE @AnimalId = Death.AnimalId
	END
GO


/* Created By: Asa Armstrong */
print '' print '*** Creating sp_update_animal_death'
GO
CREATE PROCEDURE [dbo].[sp_update_animal_death]
(
	@DeathId				int,
	@AnimalId				int,
	@UsersId				int,
	
	@NewDeathDate 			datetime,
	@NewDeathCause			nvarchar(100),
	@NewDeathDisposal		nvarchar(100),
	@NewDeathDisposalDate	dateTime,
	@NewDeathNotes			nvarchar(500),
	
	@OldDeathDate			datetime,
	@OldDeathCause			nvarchar(100),
	@OldDeathDisposal		nvarchar(100),
	@OldDeathDisposalDate	datetime,
	@OldDeathNotes			nvarchar(500)
)
AS
	BEGIN
		UPDATE [dbo].[Death]
		SET
			Death.DeathDate			= @NewDeathDate,
			Death.DeathCause			= @NewDeathCause,
			Death.DeathDisposal		= @NewDeathDisposal,
			Death.DeathDisposalDate	= @NewDeathDisposalDate,
			Death.DeathNotes			= @NewDeathNotes
		WHERE
			Death.DeathId					= @DeathId
			AND Death.AnimalId			= @AnimalId
			AND Death.UsersId				= @UsersId
			AND Death.DeathDate			= @OldDeathDate
			AND Death.DeathCause			= @OldDeathCause
			AND Death.DeathDisposal		= @OldDeathDisposal
			AND Death.DeathDisposalDate	= @OldDeathDisposalDate
			AND Death.DeathNotes			= @OldDeathNotes  OR (Death.DeathNotes IS NULL AND @OldDeathNotes IS NULL)
		RETURN @@ROWCOUNT
	END
GO







































