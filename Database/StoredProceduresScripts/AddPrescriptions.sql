USE [PetNet_db_am]

DROP PROCEDURE IF EXISTS dbo.sp_insert_prescription;  
GO
print '' print '*** Creating sp_insert_prescription (Tyler Hand)'
GO
CREATE PROCEDURE [dbo].[sp_insert_prescription]
(	
	
	@MedicalRecordID    		int,
	@UsersId					int,
	@PrescriptionTypeId			nvarchar(50),
    @PrescriptionName			nvarchar(50),
    @PrescriptionDosage        	nvarchar(50),
	@PrescriptionFrequency      nvarchar(50),
	@PrescriptionDuration       int,			
	@PrescriptionNotes          nvarchar(500),
	@DatePrescribed				datetime,
	@EndDate					datetime
)
AS
	BEGIN
		INSERT INTO [dbo].[Prescription]
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
			(
			 @MedicalRecordID,    	
			 @UsersId,				
			 @PrescriptionTypeId,		
			 @PrescriptionName,		
			 @PrescriptionDosage,     
			 @PrescriptionFrequency,  
			 @PrescriptionDuration,   
			 @PrescriptionNotes,      
			 @DatePrescribed,			
			 @EndDate				
			 )
	END
GO