USE [PetNet_db_am]
GO

/***************************************************************
Gwen Arman
Created: 2023/02/13

Description:
File containing the stored procedures for RemoveKennel(s) use case
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_delete_animal_kenneling_by_kennelid'
GO

Create procedure [dbo].[sp_delete_animal_kenneling_by_kennelid]
(
	@KennelId				[int]
)
AS
	BEGIN
		Delete 
		From	[AnimalKenneling]
		Where 	@KennelId = [KennelId]
        Return @@ROWCOUNT
    END
GO

print '' print '*** creating sp_update_kennel_status_by_kennelid'
GO

Create procedure [dbo].[sp_update_kennel_status_by_kennelid]
(
	@KennelId				[int]
)
AS
	BEGIN
		Update 	[Kennel]
		Set		[KennelActive] = 0
		Where 	@KennelId = [KennelId]
        Return @@ROWCOUNT
    END
GO