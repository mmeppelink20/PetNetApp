/* Created by: Molly Meister */
USE [PetNet_db_am]

print '' print '*** creating sp_select_images_by_animal_id (molly meister)'
GO
CREATE PROCEDURE [dbo].[sp_select_images_by_animal_id]
(
	@AnimalId		[int]
)
AS
	BEGIN
		SELECT	[AnimalMedicalImage].[ImageId], [ImageFileName]
		FROM	MedicalRecord	JOIN AnimalMedicalImage ON MedicalRecord.MedicalRecordId = AnimalMedicalImage.MedicalRecordId
								JOIN Images ON AnimalMedicalImage.ImageId = Images.ImageId
		WHERE AnimalId = @AnimalId
	END 
GO