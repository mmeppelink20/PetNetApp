print '' print '*** using PetNet_db_am'
GO
USE [PetNet_db_am]
GO


/* Created By: Asa Armstrong  */
print '' print '*** Creating sp_select_all_institutionalEntities_by_shelterId_and_entityType'
GO
CREATE PROCEDURE [dbo].[sp_select_all_institutionalEntities_by_shelterId_and_entityType]
(
	@ShelterId				int,
	@EntityType			nvarchar(15)
)
AS
	BEGIN
		SELECT InstitutionalEntityId, CompanyName, GivenName, FamilyName, Email, Phone, Address, AddressTwo, Zipcode, ContactType, ShelterId
		FROM [dbo].[InstitutionalEntity]
		WHERE @ShelterId = InstitutionalEntity.ShelterId
		AND @EntityType = ContactType
	END
GO

/*
print '' print '*** testing sp_select_all_institutionalEntities_by_shelterId_and_contactType'
GO
EXEC sp_select_all_institutionalEntities_by_shelterId_and_entityType @ShelterId = 100000, @EntityType = "Host"
GO
*/

/* Created By: Barry Mikulas  */
print '' print '*** Creating sp_select_city_state_lat_long_by_zipcode (Barry)'
GO
CREATE PROCEDURE [dbo].[sp_select_city_state_lat_long_by_zipcode]
(
	@Zipcode				char(9)
)
AS
	BEGIN
		SELECT [Zipcode], [City], [State], [Latitude], [Longitude]
		FROM [dbo].[Zipcode]
		WHERE @Zipcode = [Zipcode].[Zipcode]
	END
GO

