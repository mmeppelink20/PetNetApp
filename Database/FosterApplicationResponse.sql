print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO

/* Created by: Asa Armstrong  */
print '' print '*** creating table for FosterApplicationResponse'
GO
CREATE TABLE [dbo].[FosterApplicationResponse] (
	[FosterApplicationResponseId]			[int]	IDENTITY(100000,1)	NOT NULL,
	[FosterApplicationId]					[int]						NOT NULL,
	[UsersId]								[int]						NOT NULL,
	[Approved]								[bit]						NOT NULL,
	[FosterApplicationResponseDate]			[datetime]					NOT NULL DEFAULT GETDATE(),
	[FosterApplicationResponseNotes]		[nvarchar](50)				NULL,
	CONSTRAINT [pk_FosterApplicationResponseId]	PRIMARY KEY	([FosterApplicationResponseId]),
	CONSTRAINT [fk_FosterApplicationResponse_FosterApplicationId] FOREIGN KEY ([FosterApplicationId])
		REFERENCES [dbo].[FosterApplication]([FosterApplicationId]),
	CONSTRAINT [fk_FosterApplicationResponse_UsersId] FOREIGN KEY ([UsersId])
		REFERENCES [dbo].[Users]([UsersId])
)
GO

