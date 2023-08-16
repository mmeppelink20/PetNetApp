USE [PetNet_db_am]
GO

/***************************************************************
Alex Oetken 
Created: 2023/03/24

Description:
File containing the stored procedures for Surrender use cases
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description: 
****************************************************************/

print '' print '*** creating sp_insert_new_surrender_form'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_surrender_form]
(
	@AnimalType				NVARCHAR(50), 
	@ReasonForSurrender 	NVARCHAR(3000),
	@SpayOrNeuterStatus		BIT, 
	@ContactPhone			NVARCHAR(13), 
	@ContactEmail			NVARCHAR(300)
)
AS
	BEGIN
		INSERT INTO [dbo].[SurrenderForms]
		([AnimalType], [ReasonForSurrender], [SpayOrNeuterStatus], [ContactPhone], [ContactEmail])
		VALUES 
		(@AnimalType, @ReasonForSurrender, @SpayOrNeuterStatus, @ContactPhone, @ContactEmail)
		RETURN @@ROWCOUNT
	END
GO
	
	
print ''
print '*** creating sp_delete_surrender_form Alex'
GO 
CREATE PROCEDURE [dbo].[sp_delete_surrender_form]
(
	@SurrenderFormID [int]
)
AS 
	BEGIN 
		DELETE FROM [dbo].[SurrenderForms]
		WHERE @SurrenderFormID = [SurrenderFormID]
END 
GO


print ''
print '*** creating sp_select_all_surrender_forms'
GO 
CREATE PROCEDURE [dbo].[sp_select_all_surrender_forms]
AS 
BEGIN 
SELECT * FROM [dbo].[SurrenderForms]
END 
GO 