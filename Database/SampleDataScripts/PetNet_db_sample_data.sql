Use [PetNet_db_am]

print '' print '*** adding ExpenseCategory records ***'
GO
INSERT INTO dbo.ExpenseCategory
		([ExpenseCategoryName])
	VALUES
		('Animal food'),
		('Animal medicine'),
		('Supplies'),
		('Payroll'),
		('Insurance'),
		('Employee benefit program'),
		('Rent'),
		('Utilities'),
		('Marketing'),
		('Bank fees'),
		('Maintenance repairs'),
		('Legal expenses')
GO

print '' print '*** Inserting Job Records'
GO
INSERT INTO [dbo].[Job]
		(
		[JobDescription]
		)
	VALUES
		('Kennel Cleaning'),
        ('Feeding animals'),
        ('Checking on sick animals')
GO
	
	print '' print '*** Inserting Role Records'
GO
INSERT INTO [dbo].[Role]
		(
		[RoleId],
		[Description]
		)
	VALUES
        ('Admin','Someone who oversees Petnet'),
        ('Employee','A worker'),
		('Fosterer', 'Someone who cares for animals outside the shelter'),
		('Helpdesk', 'Someone who assists with PetNet app use'),
        ('Inspector', 'Someone who inspects'),
        ('Manager','Someone who oversees specific parts of shelters'),
		('Marketing','Someone who oversees marketing for the shelter'),
        ('Maintenance','Someone who oversees shelter facilities'),
		('Moderator','Someone who oversees shelter forums'),		
        ('Vet','Animal doctor'),
		('Volunteer','Someone helping our the shelters')
GO

print '' print '*** creating Pronoun test records' 
GO 
INSERT INTO [dbo].[Pronoun]
	([PronounId])
VALUES 
	('She/Her'),
	('He/Him'),
	('He/Her'),
	('They/Them'),
	('It/Its'),
	('She/Him'),
	('She/They'),
	('He/They'),
	('Any/All')
GO

/* Update by: Stephen Jaurigue" */
print '' print '*** inserting Images test records'
GO
INSERT INTO [dbo].[Images]
		(
		[ImageId],[ImageFileName]
		)
	VALUES
		('314a2539-5dee-40ce-ac5b-026d53750c80','MedicalImage1.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c81','MedicalImage2.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c82','MedicalImage3.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c83','MedicalImage4.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c84','MedicalImage5.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c85','MedicalImage6.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c86','InspectionImage1.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c87','InspectionImage2.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c88','InspectionImage3.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c89','InspectionImage4.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c90','InspectionImage5.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c91','InspectionImage6.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c92','AnimalImage1.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c93','AnimalImage2.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c94','AnimalImage3.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c95','AnimalImage4.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c96','AnimalImage5.png'),
        ('314a2539-5dee-40ce-ac5b-026d53750c97','AnimalImage6.png'),
		('02f42168-5f29-4f70-a6ab-3c0b4c64dc24', '02f42168-5f29-4f70-a6ab-3c0b4c64dc24'),
		('6efa448e-cead-4619-bb55-b9c078375404', '6efa448e-cead-4619-bb55-b9c078375404'),
		('0238caf0-9398-4c32-aeb8-bcf151f300ef', '0238caf0-9398-4c32-aeb8-bcf151f300ef'),
		('abacccd4-2844-4573-8c1d-5d668cc34953', 'abacccd4-2844-4573-8c1d-5d668cc34953'),
		('eecea43f-3749-4f62-b70b-4381530618d9', 'eecea43f-3749-4f62-b70b-4381530618d9'),
		('0c126019-3b6a-49a2-9103-d57ddaffaa69', 'DogImage1.png'),
		('0f19e696-2a27-41ce-b267-21ff8197cb4b', 'DogImage2.png')

        
GO

print '' print '*** Inserting ReportMessage Records'
GO
INSERT INTO [dbo].[ReportMessage]
		(
		[ReportMessageDescription]
		)
	VALUES
		('Spamming'),
		('Inappropriate Language'),
		('Harrassment'),
		('Nudity or Sexual Content')
GO

print '' print '*** Inserting EventType Records'
GO
INSERT INTO [dbo].[EventType]
		(
		[EventTypeId],
		[EventTypeDescription]
		)
	VALUES
		('Gala','Pet Gala'),
		('Park','Day of adoptable pets in the park'),
		('Pet day','Adoptable pets day')
GO

print '' print '*** inserting ApplicationStatus test records'
GO
INSERT INTO [dbo].[ApplicationStatus]
		(
		[ApplicationStatusId]
		)
	VALUES
		('Approved'),
		('Denied'),
		('Pending')
GO

print '' print '*** adding HomeOwnership records ***'
GO
INSERT INTO [dbo].[HomeOwnership]
		([HomeOwnershipId])
	VALUES
		("Own"),
		("Rent")
GO

print '' print '*** Inserting HomeType Records'
GO
INSERT INTO [dbo].[HomeType]
		(
		[HomeTypeID]
		)
	VALUES
		('House'),
        ('Apartment/Condo'),
        ('Trailer'),
		('Other')
GO

print '' print '*** Inserting BannedWords Records'
GO
INSERT INTO [dbo].[BannedWord]
		(
		[BannedWordId]
		)
	VALUES
		('Shit'),
        ('Fuck'),
        ('Cunt'),
        ('Bitch'),
        ('Cock'),
        ('Dick')
GO

print '' print '*** Inserting DonationFrequency Records'
GO
INSERT INTO [dbo].[DonationFrequency]
		(
		[DonationFrequencyId]
		)
	VALUES
		('Every 30 days'),
        ('Once a year'),
        ('One time')
GO

print '' print '*** Inserting ContactType Records'
GO
INSERT INTO [dbo].[ContactType]
		([ContactTypeId])
	VALUES
		('Host'),
		('Contact'),
		('Sponsor')	
GO

print '' print '*** inserting prescriptionType test records'
GO
INSERT INTO [dbo].[prescriptionType]
	([PrescriptionTypeId])
	VALUES
		('Medication'),
		('Food'),
		('Treatment')
GO

print '' print '*** Inserting sample Category (Inventory item tagging) records'
GO
INSERT INTO [dbo].[Category]
		(
		[CategoryID]
		)
	VALUES
		/* Pet types*/
		  ('Cat')
		, ('Dog')
		, ('Rabbit')
		, ('Rodent')
		/* Other */
		, ('Food')
		, ('Medicine')
GO

print '' print '*** Inserting Item Records'
GO
INSERT INTO [dbo].[Item]
		(
		[ItemId]
		)
	VALUES		
		 ('Cat Food'),
		 ('Dog Food'),
		 ('Rabbit Food'),
		 ('Rodent Food'),
		 ('Mysterious Item')
GO

print '' print '*** Inserting AnimalType Records'
GO
INSERT INTO [dbo].[AnimalType]
		([AnimalTypeId])
	VALUES
		('Dog'),
		('Cat'),
		('Snake')	
GO

print '' print '*** Inserting AnimalStatus Records'
GO
INSERT INTO [dbo].[AnimalStatus]
		(
		[AnimalStatusId],
		[AnimalStatusDescription]
		)
	VALUES
		('Sick', 'Animal is sick'),
        ('Healthy', 'Animal is healthy'),
        ('Deceased', 'Animal is no longer with us')
GO


print '' print '*** Inserting InventoryChangeReason Records'
GO
INSERT INTO [dbo].[InventoryChangeReason]
		(
		[InventoryChangeReasonId], 
		[ReasonDescription]
		)
	VALUES
		('Check-in', 'Item was checked in'),
        ('Check-out', 'Item was checked out')
GO

print '' print '*** creating AnimalBreed test records' 
GO 
INSERT INTO [dbo].[AnimalBreed]
		(
		[AnimalBreedId],
        [AnimalTypeId]
		)
	VALUES 
		('Lab', 'Dog'), 
		('Persian', 'Cat'),
		('Domestic Shorthair', 'Cat'),
		('German Shepard', 'Dog'),
        ('Viper', 'Snake'),
        ('Rattlesnake', 'Snake')
GO


-- ECHO print '' print '*** creating Zipcode sample data'
-- ECHO GO 
-- ECHO INSERT INTO [dbo].[Zipcode]
-- ECHO 		(
-- ECHO 		[Zipcode],
-- ECHO 		[City],
-- ECHO 		[State],
-- REM 		[Latitude],
-- REM 		[Longitude]
-- REM 		)
-- REM	VALUES
-- REM 		("50001",'Ackworth','Iowa', 41.3669, 93.4727),
-- REM 		("50002",'Adair','Iowa', 41.5004, 94.6434),
-- REM         ("52404", "Cedar Rapids", "Iowa", "41.9779", "91.6656")
-- REM GO


print '' print '*** creating TicketStatus sample data'
GO
INSERT INTO [dbo].[TicketStatus]
		(
		[TicketStatusId]
		)
	VALUES 
		('Open'), 
		('Closed')
GO


print '' print '*** creating AppealStatus sample data'
GO
INSERT INTO [dbo].[AppealStatus]
		(
		[AppealStatusId],
		[AppealDescription]
		)
	VALUES 
		('Approved','This is approved'), 
		('Denied','This is denied'),
		('Pending','This is pending')
GO

print '' print '*** creating Gender test records' 
GO 
INSERT INTO [dbo].[Gender]
		(
		[GenderId]
		)
	VALUES 
		('Female'), 
		('Male'),
		('Non-Binary'),
		('Unknown'),
		('Other')
GO

print '' print '*** creating Shelter sample data'
GO 
INSERT INTO [dbo].[Shelter]
		(
		[ShelterName],
		[Address],
		[Zipcode],
		[Phone],
		[Email],
		[Areasofneed],
		[ShelterActive]
		)
	VALUES
		("Shelter 1", "111 Shelter Drive", 50001, "1231231111", "shelter1@shelter.com", "Animal Food", 1),
		("Shelter 2", "112 Shelter Drive", 50002, "1231231112", "shelter2@shelter.com", "Animal Medicine", 1),
		("Shelter 3", "113 Shelter Drive", 50001, "1231231113", "shelter3@shelter.com", "Kitty Litter", 1)
GO

print '' print '*** creating test data for Users (Mads)'
GO
INSERT INTO [dbo].[Users]
		(
		[GenderId], 
		[PronounId], 
		[ShelterId], 
		[GivenName], 
		[FamilyName], 
		[Email],
		[Address], 
		[AddressTwo], 
		[Zipcode], 
		[Phone]
		)
    VALUES
		("Unknown", "She/Him", 100000, "Mads", "Rhea", "madsrhea@company.com", "811 Kirkwood Parkway", "Apt 207", '50001', "3195943138"),
		("Male", "He/Him", 100000, "Stephen", "Jaurigue", "stephenjaurigue@company.com", "123 Kirkwood Parkway", "Apt 210", "50001", "3195555555"),
		('Female', "She/Her", 100000, "Molly", "Meister", "mollymeister@company.com", "456 Kirkwood Parkway", "Apt 256", "50001", "3196666666"),
		('Male', 'He/Him', 100001, 'Tyler', 'Hand', 'tylerhand@company.com', '789 Kirkwood Parkway', 'Apt 240', '50002', '3197777777'),
		('Male', "He/Him", 100000, "Barry", "Mikulas", "bmikulas@company.com", "2 Kirkwood Parkway", "Apt 4", '50001', "3198675309"),
        ('Male', "He/Him", 100001, 'Chris','Dreismeier','Chris@gmail.com','4150 Riverview rd', "Apt 16", 50001, '3192948541'),
		('Male', "He/Him", 100001, 'Asa','Armstrong','Asa@gmail.com','1234 Chestnut rd', "Apt 420", 50001, '3191234321'),
		('Female', "She/Her", 100001, 'Amy','Smith','as@gmail.com','1234 Chestnut rd', "Apt 420", 50001, '7854254789'),
		('Male', "He/Him", 100001, 'Marc','Smith','ms@gmail.com','1234 Nutchest rd', "Apt 420", 50001, '8754142586'),
		('Male', "He/Him", 100001, 'John','Smith','js@gmail.com','1234 RiverRoad rd', "Apt 420", 50001, '8759860214')
GO

print '' print '*** creating Animal sample data'
GO 
INSERT INTO [dbo].[Animal]
		(
        [AnimalShelterId],
		[AnimalName],
		[AnimalGender],
		[AnimalTypeId],
		[AnimalBreedId],
        [Personality],
        [Description],
		[AnimalStatusId],
		[RecievedDate],
		[MicrochipSerialNumber],
		[Notes]
		)
	VALUES
		(100000, "Franny", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-15", "Microchip111111", "Very Cute"), 
		(100000, "Spots", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-01", "Microchip111112", "Lots of spots"), 
		(100000, "Ruffer", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111113", "Barks a lot"),
		(100000, "Buddy", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111114", "A cat in disguise"),
        (100000, "Buddy Senior", "Female", "Dog", "Lab", "Happy", "A good pet", "Deceased", "2022-12-03", "Microchip111115", "A cat in disguise"),
        (100000, "Ruffer Senior", "Female", "Dog", "Lab", "Happy", "A good pet", "Deceased", "2022-12-03", "Microchip111116", "Barks a lot"),
		(100000, "Penny", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111117", "A good dog"),
        (100000, "Chester", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111118", "A good dog"),
        (100000, "Ghost", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111119", "Doesnt bark a lot"),
		(100000, "Alexa", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111120", "A good dog"),
        (100000, "Anna", "Female", "Dog", "Lab", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111121", "Doesnt bark a lot"),
        (100000, "Purrfect", "Male", "Cat", "Persian", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111122", "Purrs a lot"),
		(100000, "Mittens", "Male", "Cat", "Persian", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111123", "Well behaved"),
        (100000, "Annie", "Male", "Cat", "Persian", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111124", "Very polite"),
        (100000, "Slithers", "Male", "Snake", "Viper", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111125", "Ssssss a lot"),
		(100000, "Leo", "Male", "Snake", "Viper", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111126", "Well behaved"),
        (100000, "Lucy", "Female", "Snake", "Rattlesnake", "Happy", "A good pet", "Healthy", "2022-12-03", "Microchip111127", "Oddly cute")
GO

print '' print '*** Creating Post sample data'

GO
INSERT INTO [dbo].[Post]
		(			
		[PostAuthor],	
		[PostContent],
        [PostVisibility],
        [PostAdminRemoved]
		)
	VALUES
		(100000,'Im happy', 1, null),
		(100001,'Im hungry', 1, null),
		(100002,'I like star wars', 1, null),
		(100003,'PETNET ROCKS!', 1, null),
        (100000,'I love this website', 1, null),
        (100000,'I found my pet through this website', 1, null),
        (100002,'Spamming', 0, 1),
        (100002,'Toxic Behavior', 0, 1),
        (100002,'Typo in my post', 0, 0)
GO

print '' print '*** Creating Event sample data'

GO
INSERT INTO [dbo].[Event]
		(
		[Zipcode],			
		[EventTypeId],		
		[ShelterId],			
		[EventTitle],		
		[EventDescription],
		[EventStart],		
		[EventEnd],			
		[EventAddress],	
		[EventZipcode],		
		[EventVisible]	
		)
	VALUES
		(50001,"Gala",100000,"Animal Adoptions","An Event to show people all of the animals for adoptions and let people adopt","03/26/2024","03/28/2024","123 maple dr",50001,1),
		(50001,"Park",100001,"Animal Adoptions","An Event to show people all of the animals for adoptions and let people adopt","04/26/2024","04/28/2024","123 maple dr",50001,1),
		(50002,"Pet day",100002,"Animal Adoptions","An Event to show people all of the animals for adoptions and let people adopt","05/26/2024","05/28/2024","1262 park place",50002,1)
		
GO

print '' print '*** Creating EventUpdate sample data'

GO
INSERT INTO [dbo].[EventUpdate]
		(
		[EventId],				
		[EventUpdateTitle],		
		[EventUpdateDescription],
		[EventUpdatePostTime],	
		[EventVisible]			
		)
	VALUES
		(100000,"Pet day","An Event to show people all of the animals for adoptions and let people adopt",GETDATE(),1),
		(100001,"Park","An Event to show people all of the animals for adoptions and let people adopt",GETDATE(),1),
		(100002,"Gala","An Event to show people all of the animals for adoptions and let people adopt",GETDATE(),1)
GO

print '' print '*** Creating Intake sample data'

GO
INSERT INTO [dbo].[Intake]
		(	
		[IntakeWorker]		
		)
	VALUES
		(100000),
		(100001),
		(100002)
GO

print '' print '*** Creating Time Clock sample data'

GO
INSERT INTO [dbo].[TimeClock]
		(
		[UsersId],   
		[StartTime],  
		[EndTime]   
		)
	VALUES
		(100000,GETDATE(), DATEADD(day, .25, GETDATE())),
		(100001,GETDATE(), DATEADD(day, 25, GETDATE())),
		(100002,GETDATE(), DATEADD(day, .25, GETDATE()))
GO

print '' print '*** Creating ResourceAddRequest sample data'

GO
INSERT INTO [dbo].[ResourceAddRequest]
		(
		[ShelterId],
		[UsersId], 
		[Title],   
		[Note],    
		[Active],  
		[Date]    
		)
	VALUES
		(100000, 100000,"Gourmet cat food","We need gourmet cat food",1,GETDATE()),
		(100000, 100001,"Gourmet dog food","We need gourmet dog food",1,GETDATE()),
		(100000, 100002,"Gourmet rabbit food","We need gourmet rabbit food",1,GETDATE()),
		(100000, 100002,"Gourmet snake food","We need gourmet snake food",1,GETDATE())
GO

print '' print '*** Creating Suspension sample data'

GO
INSERT INTO [dbo].[Suspension]
		(
		[SuspendedUser],   
		[SuspendingUser],    
		[DaysSuspended],
        [SuspendReason]
		)
	VALUES
		(100001,100000, 30, "Too damn helpful, spamming chat")
GO

print '' print '*** Creating Reply sample data'

GO
INSERT INTO [dbo].[Reply]
		(
		[PostId],   
		[ReplyAuthor],    
		[ReplyContent],
        [ReplyVisibility],
        [ReplyAdminRemoved]
		)
	VALUES
		(100000, 100000, "Wow that's cool", 0, 0),
        (100000, 100003, "Again?", 1, null),
        (100001, 100003, "Oh wow", 1, null),
        (100001, 100003, "Haha", 1, null),
        (100002, 100003, "Oh wow", 0, 0),
        (100002, 100003, "Haha", 1, null),
        (100002, 100000, "Toxic Behvaior over and over", 0, 1),
        (100002, 100000, "Spamming replies on the spot", 0, 1)
GO

print '' print '*** Creating Favorite sample data'

GO
INSERT INTO [dbo].[Favorite]
		(
		[PostId],
		[UsersId]
		)
	VALUES
		(100000, 100000),
		(100001, 100001),
		(100002, 100002),
		(100003, 100003)
			
GO

print '' print '*** Creating BookmarkAnimal sample data'

GO
INSERT INTO [dbo].[BookmarkAnimal]
		(
		[UsersId],
		[AnimalId]
		)
	VALUES
		(100000, 100000),
		(100001, 100001),
		(100002, 100002)
			
GO

print '' print '*** Creating Ticket sample data'

GO
INSERT INTO [dbo].[Ticket]
		(
		[UsersId],
		[TicketStatusId],
        [TicketTitle],
		[TicketContext]
		)
	VALUES
		(100000, 'Open', "My petnet stopped working", "I do not know how to use this app."),
		(100001, 'Open', "My petnet wont load animals", "I think there may be too many animals loading."),
		(100002, 'Open', "How do I use a computer?", "I can't find the on button."),
        (100000, 'Closed', "My keyboard wont type", "There are keys missing from my keyboard."),
		(100001, 'Closed', "My page isnt loading", "I have been waiting for the page to load all day."),
		(100003, 'Open', "How do I use a computer?", "My computer makes a loud noise when it is running.")
        
GO

print '' print '*** Creating HoursOfOperation sample data'

GO
INSERT INTO [dbo].[HoursOfOperation]
		(
		[ShelterId],
		[DayOfWeek],
        [OpenTime],
        [CloseTime]
		)
	VALUES
		(100000, 1, '08:00:00', '04:00:00'),
		(100001, 1, '08:00:00', '04:00:00'),
		(100002, 1, '08:00:00', '04:00:00')
        
GO

print '' print '*** Creating SuspensionAppeal sample data'

GO
INSERT INTO [dbo].[SuspensionAppeal]
		(
		[SuspensionId],
		[AppealStatusId],
        [AssignedAppealUser]
		)
	VALUES
		(100000, 'Pending', 100000)
        
GO

print '' print '*** Creating ShelterInventoryItem sample data'

GO
INSERT INTO [dbo].[ShelterInventoryItem]
		(
		[ShelterId],
		[ItemId],
        [Quantity],
        [UseStatistic],
        [LowInventoryThreshold],
        [HighInventoryThreshold]
		)
	VALUES
		(100000, 'Cat Food', 1, 50.99, 1, 99), 
        (100000, 'Dog Food', 5, 30.99, 1, 99),
        (100000, 'Rabbit Food', 10, 42.99, 1, 99),
		(100000, 'Mysterious Item', 1, 30.43, 1, 99),
        (100001, 'Cat Food', 1, 50.99, 1, 50), 
        (100001, 'Dog Food', 5, 30.99, 1, 50),
        (100001, 'Rabbit Food', 10, 42.99, 1, 50)
		
        
GO

print '' print '*** Creating FosterRequest sample data'

GO
INSERT INTO [dbo].[FosterRequest]
		(
		[FosterRequestShelterId],
		[FosterRequestUsersId],
        [FosterRequestMessage]
		)
	VALUES
		(100000, 100000, "Please send help"), 
        (100001, 100001, "Please send help"),
        (100002, 100002, "Please send help")
        
GO

print '' print '*** Creating Request sample data'

GO
INSERT INTO [dbo].[Request]
		(
		[ReceivingShelterId],
		[RequestedByUserId],
        [Acknowledged]
		)
	VALUES
		(100000, 100000, 0), 
        (100001, 100001, 0),
        (100002, 100002, 1)
        
GO

print '' print '*** Creating UserRoles sample data'

GO
INSERT INTO [dbo].[UserRoles]
		(
		[RoleId],
		[UsersId]
		)
	VALUES
		('Employee', 100000), 
        ('Admin', 100000), 
        ('Manager', 100000), 
        ('Vet', 100000),
        ('Volunteer', 100000),
        ('Inspector', 100000),
		('Fosterer', 100000),
        ('Volunteer', 100001),
        ('Vet', 100002),
        ('Inspector', 100003),
        ('Admin', 100004),
        ('Volunteer', 100005),
        ('Volunteer', 100006),
		('Manager', 100003)
        
GO

print '' print '*** Creating ScheduledDonation sample data'

GO
INSERT INTO [dbo].[ScheduledDonation]
		(
        [UsersId],
		[ShelterId],
        [Amount],
		[Message],
        [PaymentMethod],
        [StartDate]
		)
	VALUES
		(100000, 100000, 100.00, "Take my money", "visa", GETDATE()),
        (100001, 100000, 69.00, "Im too rich", "visa", GETDATE()),
        (100002, 100000, 420.00, "Everyone deserves to blaze it once in a while", "visa", GETDATE()),
        (100003, 100000, 666.00, "Do you have time to listen about our lord and savior?", "visa", GETDATE())
GO

print '' print '*** Creating FundraisingCampaign sample data'

GO
INSERT INTO [dbo].[FundraisingCampaign]
		(
        [UsersId],
		[ShelterId],
        [Title],
		[StartDate],
        [EndDate],
        [Description],
		[AmountRaised],		
		[NumOfAttendees],	
		[NumAnimalsAdopted]
		)
	VALUES
		(100001, 100000, 'Doggy Day', '2023-07-12', '2023-07-14', 'For the Doggies', 500, 45, 15),
        (100001, 100000, 'Kitty Day', '2023-07-16', '2023-07-18', 'For the Kitties', 1500, 52, 23),
        (100001, 100000, 'Snake Day', '2023-07-20', '2023-07-21', 'For the Snakies', 300, 14, 4),
        (100001, 100000, 'GIVE ME YOUR MONEY', '2023-07-12', '2023-07-14', 'A little too rich? We can help?', 5875.50, 40, 15),
        (100001, 100000, 'Fun Day!', '2023-07-16', '2023-07-18', 'For all animals!', 250, 25, 10),
        (100001, 100000, 'PETNET Ball', '2023-07-20', '2023-07-21', 'Time to play catch with your animals', 885, 73, 12)
GO

print '' print '*** Creating FundraisingEvent sample data'

GO
INSERT INTO [dbo].[FundraisingEvent]
		(
        [UsersId],
		[ShelterId],
		[CampaignId],
        [Title],
        [StartTime],
        [EndTime],
        [Description],
        [AdditionalInfo]
		)
	VALUES
		(100001, 100000,100000, 'Shelter in Need', '2023-07-12 10:00:00 AM', '2023-07-12 10:00:00 PM', 'You got a shelter in need', 'It will be fun!'),
        (100001, 100000,100000, 'Puppy Fun Day', '2023-07-16 10:30:00 AM', '2023-07-16 10:00:00 PM', 'Watch cute puppies play', 'It will be fun!'),
        (100001, 100000,100000, 'Give me your money', '2023-07-20 11:00:00 AM', '2023-07-20  05:00:00 PM', 'I want money', 'It will be fun!')
GO

print '' print '*** Creating Donation sample data'

GO
INSERT INTO [dbo].[Donation]
		(
        [UsersId],
		[ShelterId],
        [Amount],
        [Message],
        [HasInKindDonation],
        [Anonymous],
        [Target],
        [PaymentMethod],
		[FundraisingEventId]
		)
	VALUES
		(100001, 100000, 100.00, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmoident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0,'Dogs','Visa', 100000),
        (100002, 100000, 56.00, 'Because you helped me find my little guy', 0, 0, 'Trying to help', 'visa', NULL),
        (100003, 100000, 12.99, 'Daily good deed', 1, 0, 'Have a good day', 'Visa', 100000),
        (100002, 100000, 12.99, '', 1, 0, ':)', 'Visa', 100000),
        (100009, 100000, 12.99, 'Daily good deed', 1, 0, 'Yay', 'Visa', 100000),
        (100008, 100000, 99.99, 'Today was a good day', 0, 1, 'Im writing this off in my taxes', 'Visa', 100000),
        (100007, 100000, 150.00, 'I won at the Casino', 0, 1, 'Too much money for one person', 'Visa', 100000),
        (100007, 100001, 150.00, 'I won at the Casino', 0, 1, 'Too much money for one person', 'Visa', NULL)
GO

/* Insert donation records with dates and no messages */
INSERT INTO [dbo].[Donation]
		(
        [UsersId],
		[ShelterId],
        [Amount],
		[Date],
		[Message],
        [HasInKindDonation],
        [Anonymous],
        [Target],
        [PaymentMethod],
		[FundraisingEventId]
		)
	VALUES
		(100001, 100000, 100.00, '2023-03-18','Daily good deed', 0, 0,'I hope this helps the shelter','Visa', NULL),
        (100002, 100000, 56.00, '2023-02-26', 'Daily good deed',0, 0, 'Trying to help', 'visa', NULL),
        (100003, 100000, 12.99, '2022-10-15','Daily good deed', 1, 0, 'Have a good day', 'Visa', NULL),
        (100002, 100000, 12.99, '2022-05-14','Daily good deed', 1, 0, ':)', 'Visa', NULL),
        (100000, 100000, 12.99, '2022-08-14','Daily good deed', 1, 0, 'Yay', 'Visa', NULL),
        (100000, 100000, 99.99, '2022-10-10','Daily good deed', 0, 1, 'Im writing this off in my taxes', 'Visa', NULL),
        (100000, 100000, 150.00, '2023-03-17','Daily good deed', 0, 1, 'Too much money for one person', 'Visa', NULL),
		(100001, 100000, 100.00, '2023-03-17','Daily good deed', 0, 0,'I hope this helps the shelter','Visa', 100000),
        (100002, 100000, 56.00, '2023-03-17','Because you helped me find my little guy', 0, 0, 'Trying to help', 'visa', 100000),
        (100003, 100000, 12.99, '2023-03-17','Daily good deed', 1, 0, 'Have a good day', 'Visa', 100001),
        (100002, 100000, 12.99, '2023-03-17','Daily good deed', 1, 0, ':)', 'Visa', NULL),
        (100000, 100000, 12.99, '2023-03-17','Daily good deed', 1, 0, 'Yay', 'Visa', NULL),
        (100000, 100000, 99.99, '2023-03-17','Today was a good day', 0, 1, 'Im writing this off in my taxes', 'Visa', 100001),
        (100000, 100000, 150.00, '2023-03-17','I won at the Casino', 0, 1, 'Too much money for one person', 'Visa', NULL)
GO

print '' print '*** Creating InKind sample data'

GO
INSERT INTO [dbo].[InKind]
		(
        [DonationId],
		[Description],
        [Quantity],
        [Received]
		)
	VALUES
		(100002, 'Dog food', 1, 1),
        (100003, 'Cat toys', 5, 1),
        (100003, 'Cat food', 15, 1),
        (100003, 'Bird Cages', 3, 1),
        (100004, 'Cages', 5, 1)
GO

print '' print '*** Creating DonationAppointment sample data'

GO
INSERT INTO [dbo].[DonationAppointment]
		(
        [DateScheduled],
		[Note],
        [IsConfirmed],
        [IsFulfilled]
		)
	VALUES
		(GETDATE(), 'A payment of 100.00', 1, 0),
        (GETDATE(), 'A payment of 150.00', 1, 0),
        (GETDATE(), 'A payment of 23.00', 1, 0)
GO

print '' print '*** Creating AnimalAttendingEvent sample data'

GO
INSERT INTO [dbo].[AnimalAttendingEvent]
		(
        [AnimalAttendingId],
		[EventAttendingId]
		)
	VALUES
		(100000, 100000),
        (100001, 100001),
        (100002, 100002)
GO

print '' print '*** Creating ItemCategory sample data'

GO
INSERT INTO [dbo].[ItemCategory]
		(
        [ItemID],
		[CategoryID]
		)
	VALUES
		('Dog Food', 'Dog'),
		('Dog Food', 'Food'),
        ('Cat Food', 'Cat'),
		('Cat Food', 'Food'),
        ('Rabbit Food', 'Rabbit'),
		('Rabbit Food', 'Food'),
		('Rodent Food', 'Rodent'),
		('Rodent Food', 'Food')
GO

print '' print '*** Creating EventSubscription sample data'

GO
INSERT INTO [dbo].[EventSubscription]
		(
        [SubscriberId],
		[EventId]
		)
	VALUES
		(100000, 100000),
        (100001, 100001),
        (100002, 100002)
GO

print '' print '*** Creating Applicant sample data'

GO
INSERT INTO [dbo].[Applicant]
		(
		[UsersId],
		[ApplicantGivenName],
        [ApplicantFamilyName],
        [ApplicantAddress],
		[ApplicantAddress2],	
        [ApplicantZipCode],
        [ApplicantPhoneNumber],
        [ApplicantEmail],
        [HomeTypeId],
        [HomeOwnershipId],
        [NumberOfChildren],
        [NumberOfPets],
        [CurrentlyAcceptingAnimals]
		)
	VALUES
		(100000, 'Mads', 'Rhea', '1234 1st St', NULL, 50001, 123456789, 'email@mail.com',
		'House', 'Own', 0, 0, 1),
		(100001, 'Stephen', 'Jaurigue', '1234 2nd St', NULL, 50001, 123456789, 'email@mail.com',
		'House', 'Own', 0, 0, 1),
		(100002, 'Molly', 'Meister', '1234 1st St', NULL, 50001, 123456789, 'email@mail.com',
		'House', 'Own', 0, 0, 1),
		(100003, 'Tyler', 'Hand', '789 Kirkwood Pkwy', 'Apt 240', 52240, 3196666666,
		'tylerhand@company.com', 'Trailer', 'Own', 1, 0, 1),
		(100004, 'Barry', 'Mikulas', '2 Kirkwood Pkwy', 'Apt 4', 52405, 3195555555,
		'barrymikulas@company.com', 'Apartment/Condo', 'Rent', 0, 0, 1),
		(100005, 'Chris', 'Dreismeier', '4150 Riverview Rd', 'Apt 16', 51101, 3192948541,
		'chrisdreismeier@company.com', 'House', 'Rent', 0, 2, 1),
		(100006, 'Asa', 'Armstrong', '1234 Chestnut Rd', NULL, 52405, 3191234321,
		'asaarmstrong@company.com', 'House', 'Own', 2, 0, 1)
GO

print '' print '*** Creating FosterApplication sample data'

GO
INSERT INTO [dbo].[FosterApplication]
		(
        [ApplicantId],
		[ApplicationStatusId],
        [FosterApplicationStartDate],
		[FosterApplicationMaxAnimals]
		)
	VALUES
		(100000, 'Pending', GETDATE(), 3),
        (100001, 'Pending', GETDATE(), 2),
        (100002, 'Pending', GETDATE(), 1)
GO

print '' print '*** Creating FosterApplicationAnimalType sample data'

GO
INSERT INTO [dbo].[FosterApplicationAnimalType]
		(
        [AnimalTypeId],
		[FosterApplicationId]
		)
	VALUES
		('Dog', 100000),
        ('Cat', 100001),
        ('Snake', 100002)
GO

print '' print '*** Creating FosterRequestResponse sample data'

GO
INSERT INTO [dbo].[FosterRequestResponse]
		(
        [ApplicantId],
		[FosterRequestId],
        [FosterResponseAccepted]
		)
	VALUES
		(100000, 100000, 1),
        (100001, 100001, 1),
        (100002, 100002, 1)
GO

print '' print '*** Creating FosterRequestApplicant sample data'

GO
INSERT INTO [dbo].[FosterRequestApplicant]
		(
        [FosterRequestId],
		[ApplicantId]
		)
	VALUES
		(100000, 100000),
        (100001, 100001)
GO

print '' print '*** creating MedicalRecord sample data'
GO 
INSERT INTO [dbo].[MedicalRecord]
([AnimalId],[MedicalNotes],[MedProcedure],[Test],[Vaccination],[Prescription],[Images],[QuarantineStatus],[Diagnosis])
VALUES
(100000,"Routine Checkup",0,1,0,0,0,0,"Healthy"),
(100000,"Routine Checkup",0,1,0,0,0,0,"Healthy"),
(100001,"Wellness Checkup",0,1,0,0,0,0,"Scabies"),
(100000,"Wellness Checkup",0,1,0,0,0,1,"Slight Cough"),
(100002,"Coughing Fireballs",0,1,0,0,0,0,"Normal Dragon Stuff"),
(100002,"Stopped Coughing Fireballs",0,1,0,0,0,0,"Okay now we worry"),
(100002,"Leg surgery",1,0,0,0,0,0,"They can walk again!"),
(100003,"Jaw surgery",1,0,0,0,0,0,"They can smile again!"),
(100002,"Covid",0,0,1,0,0,0,"A shot for covid"),
(100003,"Flu shot",0,0,1,0,0,0,"A shot for the flu"),
(100003,"Covid shot",0,0,1,0,0,0,"A shot for covid"),
(100003,"Cant sleep",0,0,0,1,0,0,"A pill for sleeping"),
(100002,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100004,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100005,"Rash",0,0,0,1,0,0,"Some Ointment for skin"),
(100006,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100007,"Cant sleep",0,0,0,1,0,0,"A pill for sleeping"),
(100008,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100009,"Rash",0,0,0,1,0,0,"Some Ointment for skin"),
(100010,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100011,"Cant sleep",0,0,0,1,0,0,"A pill for sleeping"),
(100012,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100013,"Rash",0,0,0,1,0,0,"Some Ointment for skin"),
(100014,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain"),
(100015,"Cant sleep",0,0,0,1,0,0,"A pill for sleeping"),
(100016,"Tummy Hurts",0,0,0,1,0,0,"A pill for pain")

GO

print '' print '*** Creating MedProcedure sample data'

GO
INSERT INTO [dbo].[MedProcedure]
		(
        [MedicalRecordId],
		[UsersId],
        [MedProcedureName],
        [MedicationsAdministered],
		[MedProcedureNotes],
        [MedProcedureTime]
		)
	VALUES
		(100006, 100002, 'Surgery on leg', 'Scapel', 'Went well', '08:00'),
        (100007, 100002, 'Surgery on jaw', 'Scapel', 'Went well', '08:30')
GO

print '' print '*** creating Test sample data'
GO 
INSERT INTO [dbo].[Test]
	([MedicalRecordId],[UsersId],[TestName],[TestAcceptableRange],[TestResult],[TestNotes])
VALUES
	(100000,100002,"General Sickness Check","negative on all accounts","negative","Animal is healthy"),
	(100001,100002,"General Sickness Check","negative on all accounts","negative","Animal is still healthy"),
	(100002,100002,"General Sickness Check","negative on all accounts","negative","Animal is healthy"),
	(100003,100002,"General Sickness Check","negative on all accounts","Covid 19 tested positive","Animal is quaranteened"),
	(100004,100002,"Throat infection Check","negative on all accounts","negative","Animal is healthy"),
	(100005,100002,"Throat infection Check","negative on all accounts","Has Strep","Animal has been prescribed medicine")
GO

print '' print '*** Creating Vaccination sample data'

GO
INSERT INTO [dbo].[Vaccination]
		(
        [MedicalRecordId],
		[UsersId],
        [VaccineName]
		)
	VALUES
		(100008, 100002, 'Covid 19 Shot'),
        (100009, 100002, 'Flu Shot'),
        (100010, 100002, 'Covid 19 Shot')
GO

print '' print '*** Creating Prescriptions sample data'
go 

insert into [dbo].[Prescription] 
		(					
		[MedicalRecordId],			
		[UsersId],					
		[PrescriptionTypeId],		
		[PrescriptionName],			
		[PrescriptionDosage],		
		[PrescriptionFrequency],		
		[PrescriptionDuration],		
		[PrescriptionNotes],			
		[DatePrescribed],			
		[EndDate]
		)		
	
	VALUES 
		(100011,100002,"Treatment","malatoin","5mg","Once a day",10,"1 pill by mouth at bedtime",GETDATE(),GETDATE()),
		(100012,100002,"Treatment","asprin","20mg","Twice a day",30,"1 pill by mouth Every 8 hours",GETDATE(),GETDATE()),
		(100013,100002,"Treatment","Skin Care","100ml","Once a day",10,"Apply ointment to skin of infected area",GETDATE(),GETDATE()),
		(100014,100002,"Treatment","Joint Care","50mg","Twice a day",30,"1 pill by mouth every 12 hours",GETDATE(),GETDATE()),
		(100015,100002,"Treatment","Amoxicillin","500mg","Three times a day",20,"1 pill by mouth every 8 hours",GETDATE(),GETDATE()),
		(100016,100002,"Treatment","Rabies Vaccine","20mg","Once every 6 months",1,"Administer intramuscularly on left hind leg",GETDATE(),GETDATE()),
		(100017,100002,"Treatment","Ivermectin","1ml","Once a week",4,"Apply topically on back of neck",GETDATE(),GETDATE()),
		(100018,100002,"Treatment","Fluoxetine","20mg","Once a day",30,"1 pill by mouth every morning",GETDATE(),GETDATE()),
		(100019,100002,"Treatment","Lysine","500mg","Twice a day",60,"1 pill by mouth every 12 hours",GETDATE(),GETDATE()),
		(100020,100002,"Treatment","Prednisolone","10mg","Once a day",20,"1 pill by mouth every evening",GETDATE(),GETDATE()),
		(100021,100002,"Treatment","Doxycycline","100mg","Twice a day",30,"1 pill by mouth every 12 hours",GETDATE(),GETDATE()),
		(100022,100002,"Treatment","Famotidine","20mg","Once a day",60,"1 pill by mouth every evening",GETDATE(),GETDATE()),
		(100023,100002,"Treatment","Metronidazole","250mg","Three times a day",14,"1 pill by mouth every 8 hours",GETDATE(),GETDATE()),
		(100024,100002,"Treatment","Cephalexin","250mg","Four times a day",40,"1 pill by mouth every 6 hours",GETDATE(),GETDATE())
		


print '' print '*** creating Kennel sample data'
GO 
INSERT INTO [dbo].[Kennel]
		(
		[ShelterId],
		[KennelName],
		[AnimalTypeId],
		[KennelSpace],
		[KennelActive]
		)
	VALUES
		(100000, "Kennel 1", "Dog", 1, 1),
		(100000, "Kennel 2", "Dog", 1, 1),
		(100000, "Kennel 3", "Dog", 1, 1),
		(100000, "Kennel 4", "Dog", 1, 1),
		(100000, "Kennel 5", "Dog", 1, 1),
		(100000, "Kennel 6", "Dog", 1, 1),
		(100000, "Kennel 7", "Dog", 1, 1),
		(100000, "Kennel 8", "Dog", 1, 1),
		(100000, "Kennel 9", "Dog", 1, 1),
		(100000, "Kennel 10", "Dog", 1, 1)
GO

print '' print '*** creating AnimalKenneling sample data'
GO 
INSERT INTO [dbo].[AnimalKenneling]
		(
		[KennelId],
		[AnimalId]
		)
	VALUES
		(100000, 100000),
		(100001, 100001),
		(100008, 100002)
GO

print '' print '*** creating Inspection sample data'
GO 
INSERT INTO [dbo].[Inspection]
		(
		[ApplicantId],
		[InspectionInspectorId],
        [InspectionComments],
		[InspectionDateScheduled],
        [InspectionDateCompleted],
        [InspectionAnimalCountApproved]
		)
	VALUES
		(100000, 100003, 'Place was tidy', GETDATE(), GETDATE(), 2),
        (100001, 100003, 'Place was dirty', GETDATE(), GETDATE(), 0),
        (100000, 100003, 'Big yard', GETDATE(), GETDATE(), 4)
GO

print '' print '*** creating AnimalMedicalImage sample data'
GO 
INSERT INTO [dbo].[AnimalMedicalImage]
		(
		[ImageId],
		[MedicalRecordId]
		)
	VALUES
		('314a2539-5dee-40ce-ac5b-026d53750c80', 100000),
        ('314a2539-5dee-40ce-ac5b-026d53750c81', 100001),
        ('314a2539-5dee-40ce-ac5b-026d53750c82', 100002),
        ('314a2539-5dee-40ce-ac5b-026d53750c83', 100003),
        ('314a2539-5dee-40ce-ac5b-026d53750c84', 100004),
        ('314a2539-5dee-40ce-ac5b-026d53750c85', 100005)
GO

print '' print '*** creating RequestResourceLine sample data'
GO 
INSERT INTO [dbo].[RequestResourceLine]
		(
		[RequestId],
		[ItemId],
        [QuantityRequested],
        [Notes]
		)
	VALUES
		(100000, 'Cat Food', 50, 'Need More'),
        (100001, 'Dog Food', 99, 'Need More'),
        (100002, 'Rabbit Food', 10, 'Need More')
GO

print '' print '*** creating CampaignUpdate sample data'
GO 
INSERT INTO [dbo].[CampaignUpdate]
		(
		[CampaignId],
		[UpdateTitle],
        [UpdateDescription]
		)
	VALUES
		(100000, 'Animal Day', 'Animals having fun!'),
        (100000, 'Animal Babies Day', 'Baby animals having fun!')
GO

print '' print '*** creating PostReport sample data'
GO 
INSERT INTO [dbo].[PostReport]
		(
		[PostId],
		[PostReporter],
        [ReportMessageId]
		)
	VALUES
		(100000, 100001, 100000),
        (100001, 100002, 100001),
        (100002, 100000, 100002)
GO

print '' print '*** creating ReplyReport sample data'
GO 
INSERT INTO [dbo].[ReplyReport]
		(
		[ReplyId],
		[ReplyReporter],
        [ReportMessageId]
		)
	VALUES
		(100000, 100000, 100000),
        (100001, 100001, 100001)
GO

print '' print '*** creating Pledge sample data'
GO 
INSERT INTO [dbo].[Pledge]
		(
		[UsersId],
		[DonationId],
		[FundraisingEventId],
		[Date],
		[Amount],
        [Message],
        [GivenName],
        [FamilyName],
        [Phone],
        [Email]
		)
	VALUES
		(100007, 100000, 100000, '2023-04-21', 100.00, 'Giving back', 'John', 'Pearson', '6546546544', 'jp@gmail.com'),
		(100007, null, 100000, GETDATE(), 200.00, 'Take it', 'Asa', 'Smith', '6546546544', 'as@gmail.com'),
		(100007, 100002, 100000, '2023-03-22', 300.00, 'For dog', 'Chris', 'Northup', '6546546544', 'cn@gmail.com'),
		(100007, 100003, 100000, GETDATE(), 400.00, 'For cat', 'Will', 'Smith', '6546546544', 'ws@gmail.com'),
		(100007, 100004, 100000, GETDATE(), 500.00, 'For snake', 'John', 'Armstrong', '6546546544', 'ja@gmail.com'),
        (100008, 100005, 100000, GETDATE(), 50.00, 'Here you go', 'Marc', 'Benjamin', '6546546544', 'mb@gmail.com'),
		(100008, null, 100000, '2023-03-15', 50.00, 'Take it', 'Marc', 'Jones', '6546546544', 'mj@gmail.com'),
		(100008, null, 100000, '2023-05-01', 50.00, 'For cat', 'Mark', 'Williams', '6546546544', 'mw@gmail.com'),
		(100008, null, 100000, '2023-04-12', 50.00, 'For cat', 'Matthew', 'Smith', '6546546544', 'ms2@gmail.com'),
        (100009, null, 100000, '2023-03-30', 50.00, 'Here you go again', 'Amy', 'Dre', '6546546544', 'ad@gmail.com'),
		(100009, null, 100000, '2023-03-31', 50.00, 'For cat', 'Tonya', 'West', '6546546544', 'ta@gmail.com'),
		(100009, 100006, 100001, GETDATE(), 50.00, 'For rat', 'Lisa', 'Catz', '6546546544', 'lc@gmail.com'),
		(100009, null, 100001, GETDATE(), 50.00, 'For the animals', 'Anne', 'Jackson', '6546546544', 'aj@gmail.com')
GO

print '' print '*** creating AdoptionApplication sample data'
GO 
INSERT INTO [dbo].[AdoptionApplication]
		(
		[ApplicantId],
		[AnimalId],
		[ApplicationStatusId],
		[AdoptionApplicationDate]
		)
	VALUES
		(100000, 100000, 'Approved', DEFAULT),
		(100000, 100003, 'Pending', DEFAULT),
		(100001, 100003, 'Pending', DEFAULT),
        (100002, 100002, 'Approved', DEFAULT),
		(100003, 100010, 'Pending', DEFAULT),
		(100005, 100013, 'Pending', DEFAULT),
		(100005, 100008, 'Pending', DEFAULT),
		(100006, 100011, 'Pending', DEFAULT)
GO


print '' print '*** creating InstitutionalEntity sample data'
GO 
INSERT INTO [dbo].[InstitutionalEntity]
		(
		[CompanyName],
		[GivenName],
        [FamilyName],
		[Email],
        [Phone],
		[Address],
		[Zipcode],
        [ContactType],
        [ShelterId]
		)
	VALUES
		('US Animals', 'Bob', 'Doe', 'bd@gmail.com', '1231233333', '121 Place Street', 50001, 'Sponsor', 100000),
        ('CA Animals', 'Stephanie', 'Doe', 'sd@gmail.com', '1231233334', '122 Place Street', 50001, 'Sponsor', 100000),
        ('SA Animals', 'Jess', 'Doe', 'jd@gmail.com', '1231233335', '123 Place Street', 50001, 'Sponsor', 100000),
		('BA Animals', 'Cam', 'Doe', 'cd@gmail.com', '1231233335', '124 Place Street', 50001, 'Host', 100000),
		('DA Animals', 'Jack', 'Doe', 'jkd@gmail.com', '1231233335', '126 Place Street', 50001, 'Host', 100000),
		('LA Animals', 'River', 'Doe', 'rd@gmail.com', '1231233335', '127 Place Street', 50001, 'Host', 100000),
		('DF Animals', 'River', 'Doe', 'rd@gmail.com', '1231233335', '127 Place Street', 50001, 'Host', 100000),
		('WG Animals', 'River', 'Doe', 'rd@gmail.com', '1231233335', '127 Place Street', 50001, 'Contact', 100000),
		('PK Animals', 'River', 'Doe', 'rd@gmail.com', '1231233335', '127 Place Street', 50001, 'Contact', 100000),
		('NS Animals', 'River', 'Doe', 'rd@gmail.com', '1231233335', '127 Place Street', 50001, 'Contact', 100000),
		('ST Animals', 'River', 'Doe', 'rd@gmail.com', '1231233335', '127 Place Street', 50001, 'Contact', 100000),
		('US Animals', 'Bob', 'Doe', 'bd@gmail.com', '1231233333', '121 Place Street', 50001, 'Sponsor', 100000),
        ('CA Animals', 'Stephanie', 'Doe', 'sd@gmail.com', '1231233334', '122 Place Street', 50001, 'Sponsor', 100000),
        ('SA Animals', 'Jess', 'Doe', 'jd@gmail.com', '1231233335', '123 Place Street', 50001, 'Sponsor', 100000)
GO

print '' print '*** creating FundraisingCampaignEntity sample data'
GO 
INSERT INTO [dbo].[FundraisingCampaignEntity]
		(
		[Fundraiser],
		[Institution]
		)
	VALUES
		(100000, 100000),
        (100001, 100001),
        (100002, 100002)
GO

print '' print '*** creating FundraisingEventEntity sample data'
GO 
INSERT INTO [dbo].[FundraisingEventEntity]
		(
		[EventId],
		[ContactId]
		)
	VALUES
		(100000, 100000),
		(100000, 100001),
		(100000, 100008),
        (100001, 100001),
        (100002, 100002)
GO

print '' print '*** creating InspectionImage sample data'
GO 
INSERT INTO [dbo].[InspectionImage]
		(
		[InspectionId],
		[ImageId]
		)
	VALUES
		(100000, '314a2539-5dee-40ce-ac5b-026d53750c86'),
        (100001, '314a2539-5dee-40ce-ac5b-026d53750c87'),
        (100002, '314a2539-5dee-40ce-ac5b-026d53750c88')
GO

print '' print '*** creating AdoptionPlacement sample data'
GO 
INSERT INTO [dbo].[AdoptionPlacement]
		(
		[AnimalId],
		[ApplicantId],
        [AdoptionDate]
		)
	VALUES
		(100000, 100000, '2023-02-19'),
        (100001, 100001, '2023-02-15'),
        (100002, 100002, '2023-02-10')
GO

print '' print '*** creating ShelterItemTransaction sample data'
GO 
INSERT INTO [dbo].[ShelterItemTransaction]
		(
		[ShelterId],
		[ItemId],
        [ChangedByUsersId],
        [InventoryChangeReasonId],
        [QuantityIncrement]
		)
	VALUES
		(100000, 'Dog Food', 100000,'Check-in', 50),
        (100000, 'Cat Food', 100000, 'Check-out', 50),
        (100001, 'Dog Food', 100001, 'Check-in', 50),
        (100001, 'Cat Food', 100001, 'Check-out', 50),
        (100002, 'Dog Food', 100002, 'Check-in', 50),
        (100002, 'Cat Food', 100002, 'Check-out', 50)
GO

print '' print '*** creating IntakeLine sample data'
GO 
INSERT INTO [dbo].[IntakeLine]
		(
		[IntakeId],
		[AnimalId]
		)
	VALUES
		(100000, 100000),
        (100001, 100001),
        (100002, 100002)
GO

print '' print '*** creating FundraiserAnimal sample data'
GO 
INSERT INTO [dbo].[FundraiserAnimal]
		(
		[FundraisingEventId],
		[AnimalId]
		)
	VALUES
		(100000, 100000),
		(100000, 100006),
		(100000, 100005),
		(100000, 100011),
		(100000, 100007),
        (100001, 100001),
        (100001, 100009),
        (100001, 100010),
        (100001, 100011),
        (100002, 100001),
        (100002, 100003),
        (100002, 100005)
GO

print '' print '*** creating AnimalUpdates sample data'
GO 
INSERT INTO [dbo].[AnimalUpdates]
		(
		[AnimalId],
		[AnimalRecordNotes]
		)
	VALUES
		(100000, 'It has been sad recently'),
        (100001, 'It has been acting out recently'),
        (100002, 'It has been extremely hungry recently')
GO

print '' print '*** creating FosterPlacement sample data'
GO 
INSERT INTO [dbo].[FosterPlacement]
		(
		[AnimalId],
		[ApplicantId],
        [FosterStartDate],
        [FosterEndDate]
		)
	VALUES
		(100000, 100000, GETDATE(), DATEADD(day, 30, getdate())),
        (100001, 100001, GETDATE(), DATEADD(day, 30, getdate())),
        (100002, 100002, GETDATE(), DATEADD(day, 30, getdate()))
GO

print '' print '*** creating FosterPlacementRecord sample data'
GO 
INSERT INTO [dbo].[FosterPlacementRecord]
		(
		[FosterPlacementId],
		[FosterPlacementRecordNotes]
		)
	VALUES
		(100000, 'Its going well'),
        (100001, 'Its going well'),
        (100002, 'Its going well')
GO

print '' print '*** creating AdoptionApplicationResponse sample data'
GO 
INSERT INTO [dbo].[AdoptionApplicationResponse]
		(
		[AdoptionApplicationId],
        [UsersId],
        [Approved],
        [AdoptionApplicationResponseNotes]
		)
	VALUES
		(100000, 100000, 1, 'It went well'),
        (100002, 100001, 1, 'Nice big yard.')
GO

print '' print '*** creating FundraiserVolunteerUser sample data'
GO 
INSERT INTO [dbo].[FundraiserVolunteerUser]
		(
		[FundraisingEventId],
        [UsersId]
		)
	VALUES
		(100000, 100000),
        (100001, 100001),
        (100002, 100002)
GO

print '' print '*** creating ShelterExpense sample data'
GO 
INSERT INTO [dbo].[ShelterExpense]
		(
		[ShelterId],
        [CategoryID],
        [Year],
        [Amount]
		)
	VALUES
		(100000, 100006, 2022, 50000),
        (100000, 100003, 2022, 50000),
        (100000, 100008, 2022, 50000),
        (100001, 100006, 2022, 50000),
        (100001, 100003, 2022, 50000),
        (100001, 100008, 2022, 50000),
        (100002, 100006, 2022, 50000),
        (100002, 100003, 2022, 50000),
        (100002, 100008, 2022, 50000)
GO

print '' print '*** creating Schedule sample data'
GO 
INSERT INTO [dbo].[Schedule]
		(
		[UsersId],
        [JobId],
        [StartTime],
        [EndTime]
		)
	VALUES
		(100000, 100000, GETDATE(), DATEADD(day, 1, getdate())),
        (100001, 100000, GETDATE(), DATEADD(day, 1, getdate())),
        (100002, 100000, GETDATE(), DATEADD(day, 1, getdate())),
        (100000, 100000, '20230215 08:00:00 AM','20230215 06:00:00 PM' ),
		(100000, 100000, '20230216 08:00:00 AM','20230216 06:00:00 PM' ),
		(100000, 100000, '20230217 08:00:00 AM','20230217 06:00:00 PM' ),
		(100000, 100000, '20230218 08:00:00 AM','20230218 06:00:00 PM' ),
		(100001, 100000, '20230215 08:00:00 AM','20230215 06:00:00 PM' ),
		(100001, 100000, '20230216 08:00:00 AM','20230216 06:00:00 PM' ),
		(100001, 100000, '20230217 08:00:00 AM','20230217 06:00:00 PM' ),
		(100001, 100000, '20230218 08:00:00 AM','20230218 06:00:00 PM' )
GO

print '' print '*** creating Death sample data'
GO 
INSERT INTO [dbo].[Death]
		(
		[UsersId],
        [AnimalId],
        [DeathCause],
        [DeathDisposal],
        [DeathDisposalDate],
        [DeathNotes]
		)
	VALUES
		(100002, 100004, 'Old age', 'Plastic Bag', GETDATE(), 'Very heavy'),
        (100002, 100005, 'Old age', 'Plastic Bag', GETDATE(), 'Very heavy')
GO


print '' print '*** creating AnimalImage sample data'
GO 
INSERT INTO [dbo].[AnimalImage]
		(
		[AnimalId],
        [ImageId]
		)
	VALUES
		(100000, '314a2539-5dee-40ce-ac5b-026d53750c92'),
		(100000, '0c126019-3b6a-49a2-9103-d57ddaffaa69'),
		(100000, '0f19e696-2a27-41ce-b267-21ff8197cb4b'),
        (100001, '314a2539-5dee-40ce-ac5b-026d53750c93'),
        (100002, '314a2539-5dee-40ce-ac5b-026d53750c94'),
        (100003, '314a2539-5dee-40ce-ac5b-026d53750c95'),
        (100004, '314a2539-5dee-40ce-ac5b-026d53750c96'),
        (100005, '314a2539-5dee-40ce-ac5b-026d53750c97'),
		(100002, '02f42168-5f29-4f70-a6ab-3c0b4c64dc24'),
		(100002, '6efa448e-cead-4619-bb55-b9c078375404'),
		(100002, '0238caf0-9398-4c32-aeb8-bcf151f300ef'),
		(100002, 'abacccd4-2844-4573-8c1d-5d668cc34953'),
		(100002, 'eecea43f-3749-4f62-b70b-4381530618d9')
GO


print '' print '*** creating SurrenderForms sample data '
GO

INSERT [dbo].[SurrenderForms]
( [AnimalType], [ReasonForSurrender], [SpayOrNeuterStatus], [ContactPhone], [ContactEmail])
VALUES 
	( 'Cat', 'Moving Away', 1, 9876543311, 'Alex@company.com'),
	( 'Bird', 'Bird is too loud and bites', 0, 9876543311, 'Alex@company.com'),
	( 'Snake', 'Has a bad attitude', 0, 9876543311, 'Alex@company.com')
GO