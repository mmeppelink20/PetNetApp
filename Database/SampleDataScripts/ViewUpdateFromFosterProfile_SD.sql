/***************************************************************
Hoang Chu
Created: 2023/03/02
***************************************************************/
USE [PetNet_db_am]


print '' print '*** Inserting sample HomeType data'
GO
INSERT INTO [dbo].[HomeType]
	([HomeTypeID])
	VALUES
    ('Apartment')
GO

print '' print '*** Inserting sample HomeOwnership data'
GO
INSERT INTO [dbo].[HomeOwnership]
	([HomeOwnershipId])
	VALUES
    ('Renting')
GO

print '' print '*** Inserting sample Applicant data'
GO
INSERT INTO [dbo].[Applicant]
	([UsersId],[ApplicantGivenName],[ApplicantFamilyName], [ApplicantAddress], [ApplicantZipCode], [ApplicantPhoneNumber], 
    [ApplicantEmail], [HomeTypeId], [HomeOwnershipId], [NumberOfChildren], [NumberOfPets], [CurrentlyAcceptingAnimals])
	VALUES
    (100001, 'Stephen', 'Jaurige', '100 Eve', '50001', '123456789', 'userEmail@gmail.com', 'Apartment', 'Renting', '1', '3', 1)
GO

print '' print '*** Inserting sample FosterPlacement data'
GO
INSERT INTO [dbo].[FosterPlacement]
	([AnimalId],[ApplicantId],[FosterStartDate], [FosterEndDate])
	VALUES
    (100003, 100003, '03-02-2023', '03-02-2023'),
    (100001, 100003, '03-02-2023', '03-02-2023'),
    (100002, 100003, '03-02-2023', '03-02-2023')
GO

print '' print '*** Inserting sample FosterPlacementRecord data'
GO
INSERT INTO [dbo].[FosterPlacementRecord]
	([FosterPlacementId],[FosterPlacementRecordNotes],[FosterPlacementRecordDate])
	VALUES
    (100003, 'This is animal note', '03-02-2023'),
    (100004, 'this is animal note', '03-02-2023'),
    (100005, 'This is animal note', '03-02-2023')
GO