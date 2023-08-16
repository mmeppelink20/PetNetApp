Use [PetNet_db_am]

-- print '' print '*** adding Applicant records Asa ***'
-- GO
-- INSERT INTO dbo.Applicant
		-- (
		-- [UsersId],
		-- [ApplicantGivenName],
        -- [ApplicantFamilyName],
        -- [ApplicantAddress],
        -- [ApplicantZipCode],
        -- [ApplicantPhoneNumber],
        -- [ApplicantEmail],
        -- [HomeTypeId],
        -- [HomeOwnershipId],
        -- [NumberOfChildren],
        -- [NumberOfPets],
        -- [CurrentlyAcceptingAnimals]
		-- )
	-- VALUES
		-- (
		-- 100000, 'Mads', 'Rhea', '1234 1st St', 50001, 123456789, 'email@mail.com',
		-- 'House', 'Own', 0, 0, 1
		-- ),
		-- (
		-- 100001, 'Stephen', 'Jaurigue', '1234 2nd St', 50001, 123456789, 'email@mail.com',
		-- 'House', 'Own', 0, 0, 1
		-- ),
		-- (
		-- 100002, 'Molly', 'Meister', '1234 1st St', 50001, 123456789, 'email@mail.com',
		-- 'House', 'Own', 0, 0, 1
		-- )
-- GO

print '' print '*** adding FosterApplication records Asa ***'
GO
INSERT INTO dbo.FosterApplication
		(
		[ApplicantId],
		[ApplicationStatusId],
		[FosterApplicationStartDate],
		[FosterApplicationMaxAnimals]
		)
	VALUES
		(100004, 'Pending', GETDATE(), 5),
		(100005, 'Pending', GETDATE(), 5),
		(100006, 'Pending', GETDATE(), 5)
GO

print '' print '*** adding FosterApplicationAnimalType records Asa ***'
GO
INSERT INTO [dbo].[FosterApplicationAnimalType]
		(
        [AnimalTypeId],
		[FosterApplicationId]
		)
	VALUES
		('Dog', 100003),
        ('Cat', 100004),
        ('Snake', 100005)
GO