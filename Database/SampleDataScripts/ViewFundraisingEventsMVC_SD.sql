USE [PetNet_db_am]
GO

print '' print '*** Creating FundraisingEvent sample data (for MVC View)'

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
		(100001, 100001, 100002, 'Adopt a Snake', '2023-06-12 10:00:00 AM', '2023-06-12 04:00:00 PM', 'Come see the snakes we have up for adoption.', 'Hissss!'),
        (100001, 100001, 100002, 'Learn About Reptiles', '2023-06-16 10:00:00 AM', '2023-06-16 04:00:00 PM', 'Bring your reptiles (non-venomous) and we''ll have some on hand for you to learn about.', 'Scales are not scary!'),
        (100001, 100001, 100003, '5th Annual We Love Animals Ball', '2023-07-21 04:00:00 PM', '2023-07-21 11:00:00 PM', 'Put on your black tie and dance the night away!', 'It will be fun!'),
		(100001, 100000, 100001, 'Kittens for Adoption', '2023-05-12 10:00:00 AM', '2023-05-12 04:00:00 PM', 'We have many kittens that are in need of a home. The are fully vacinated and ready for you.', 'Soft, Furry, and fun to own.'),
		(100001, 100000, 100001, 'Fur Ball Fun Day', '2023-07-10 10:00:00 AM', '2023-07-10 04:00:00 PM', 'See all the furry animals.', 'Awe shucks!')
GO

print '' print '*** creating FundraisingEventEntity sample data (for MVC View) - adding hosts, sponsor, contacts'
GO 
INSERT INTO [dbo].[FundraisingEventEntity]
		(
		[EventId],
		[ContactId]
		)
	VALUES
		(100003, 100268),
		(100004, 100273),
		(100005, 100317),
		(100006, 100345),
		(100007, 100255),
		(100003, 100241),
		(100004, 100207),
		(100005, 100164),
		(100006, 100183),
		(100007, 100192),
		(100001, 100137),
		(100002, 100007),		
		(100003, 100056),
		(100004, 100075),
		(100005, 100101),
		(100006, 100116),
		(100007, 100027)
GO