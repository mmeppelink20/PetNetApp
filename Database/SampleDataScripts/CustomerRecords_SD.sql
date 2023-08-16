Use [PetNet_db_am]
/***************************************************************
Teft Francisco
Created: 2023/03/03
***************************************************************/

/* This is needed to insert applicants with reference to the Users table. */

print '' print '*** Adoption Record data ***'

GO
INSERT INTO [dbo].[AdoptionPlacement]
		([AnimalId], [ApplicantId], [AdoptionDate])
	VALUES
		(100002, 100003, '2023-02-10'),
		(100001, 100003, '2023-02-12')
GO