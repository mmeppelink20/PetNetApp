USE [PetNet_db_am]
GO

/***************************************************************
Andrew Cromwell
Created: 2023/04/20

Description:
File containing the stored procedures for Delete Reply use case
****************************************************************/

print '' print '*** creating sp_insert_ResourceAddRequest (Andrew Cromwell)'
GO
Create procedure [dbo].[sp_insert_ResourceAddRequest]
(
	@shelterid		int,
	@usersid		int,
	@title			nvarchar(100),
	@note			nvarchar(2500),
    @active			bit
)
AS
	BEGIN
		INSERT INTO [dbo].[ResourceAddRequest]
			([ShelterId], [UsersId], [Title], [Note], [Active])
		VALUES
			(@shelterid, @usersid, @title, @note, @active)
    END
GO