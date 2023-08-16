USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/03/24

Description:
File containing the stored procedures for AddPost use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_post'
GO
Create procedure [dbo].[sp_insert_post]
(
	@PostAuthor		[int],
    @PostContent	[nvarchar](250),
    @PostDate		[datetime]
)
AS
	BEGIN
		Insert into [Post]
			([PostAuthor], [PostContent], [PostDate])
		Values
			(@PostAuthor, @PostContent, @PostDate)
		SELECT SCOPE_IDENTITY()
    END
GO