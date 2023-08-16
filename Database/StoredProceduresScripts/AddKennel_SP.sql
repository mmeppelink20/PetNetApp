USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/02/13

Description:
File containing the stored procedures for AddKennel use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_kennel'
GO
Create procedure [dbo].[sp_insert_kennel]
(
    @ShelterId				[int],
    @KennelName				[nvarchar](50),
    @AnimalTypeId			[nvarchar](50)
)
AS
	BEGIN
		Insert into [Kennel]
			([ShelterId], [KennelName], [AnimalTypeId])
        Values
			(@ShelterId, @KennelName, @AnimalTypeId)
		Return @@ROWCOUNT
    END
GO

print '' print '*** creating sp_select_animal_types'
GO
Create procedure [dbo].[sp_select_animal_types]
AS
	BEGIN
		Select 	[AnimalTypeId]
        From	[AnimalType]
    END
GO