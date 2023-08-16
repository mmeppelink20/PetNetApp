USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/02/13

Description:
File containing the stored procedures for ViewKennel use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_select_kennels'
GO
Create procedure [dbo].[sp_select_kennels]
(
	@ShelterId	[int]
)
AS
	BEGIN
		Select 	[Kennel].[KennelId], [ShelterId], [KennelName], [Kennel].[AnimalTypeId], [KennelActive], [AnimalName],
				[RecievedDate], [Notes], [AnimalKenneling].[AnimalId]
        From	[Kennel] left join [AnimalKenneling]
					on [Kennel].[KennelId] = [AnimalKenneling].[KennelId]
				left join [Animal]
					on [AnimalKenneling].[AnimalId] = [Animal].[AnimalId]
		Where	@ShelterId = [ShelterId] AND [KennelActive] = 1
    END
GO

print '' print '*** creating sp_select_image_by_animalid'
GO
Create procedure [dbo].[sp_select_image_by_animalid]
(
	@AnimalId	[int]
)
AS
	BEGIN
		Select 	Top(1) [Images].[ImageId], [Images].[ImageFileName]
        From	[Images] join [AnimalImage]
					on [Images].[ImageId] = [AnimalImage].[ImageId]
		Where	@AnimalId = [AnimalId] 
        
    END
GO

