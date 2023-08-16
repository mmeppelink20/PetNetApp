/***************************************************************
Mads Rhea
Created: 2023/03/14

Description:
File contains 
    - a stored procedure to [sp_select_number_of_animals_approved_by_usersId].
    - a stored procedure to [sp_select_number_of_animals_fosterer_has_by_usersId].
    - a stored procedure to [sp_select_currentlyacceptinganimals_by_usersId].
    - a stored procedure to [sp_update_currentlyacceptinganimals_by_usersId].

**************************************************************
Updated by:
Updated: 202-/--/--

Description: ---
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating [SP_SELECT_NUMBER_OF_ANIMALS_APPROVED_BY_USERSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_number_of_animals_approved_by_usersId]
        (
            @UsersId    [int]
        )
        AS 
            BEGIN
                SELECT  [Inspection].[InspectionAnimalCountApproved]
                FROM    [Inspection]
                JOIN    [Applicant]
                ON      [Inspection].[ApplicantId] = [Applicant].[ApplicantId]
                AND     [Applicant].[UsersId] = @UsersId
            END
        GO

/********************************/

print '' print '*** creating [SP_SELECT_NUMBER_OF_ANIMALS_FOSTERER_HAS_BY_USERSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_number_of_animals_fosterer_has_by_usersId]
        (
            @UsersId    [int]
        )
        AS 
            BEGIN
                SELECT  COUNT([FosterPlacement].[AnimalId])
                FROM    [FosterPlacement]
                JOIN    [Applicant]
                ON      [Applicant].[ApplicantId] = [FosterPlacement].[ApplicantId]
                WHERE   [Applicant].[UsersId] = @UsersId
                AND     [FosterPlacement].[FosterAnimalReturned] = 0
            END
        GO

/********************************/

print '' print '*** creating [SP_SELECT_CURRENTLYACCEPTINGANIMALS_BY_USERSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_select_currentlyacceptinganimals_by_usersId]
        (
            @UsersId    [int]
        )
        AS 
            BEGIN
                SELECT [CurrentlyAcceptingAnimals]
			    FROM [Applicant]
		        WHERE [UsersId] = @UsersId
            END
        GO

/********************************/

print '' print '*** creating [SP_UPDATE_CURRENTLYACCEPTINGANIMALS_BY_USERSID]' 
        GO 
        CREATE PROCEDURE [dbo].[sp_update_currentlyacceptinganimals_by_usersId]
        (
            @UsersId    [int],
            @CurrentlyAcceptingAnimals  [bit]
        )
        AS 
            BEGIN
                UPDATE [Applicant]
			    SET [CurrentlyAcceptingAnimals] = @CurrentlyAcceptingAnimals
		        WHERE [UsersId] = @UsersId
                AND NOT [CurrentlyAcceptingAnimals] = @CurrentlyAcceptingAnimals
		        RETURN @@ROWCOUNT
            END
        GO

