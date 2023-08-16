USE [PetNet_db_am]
GO

/********************************************************************************
Andrew Schneider
Created: 2023/03/10

Description:
File containing the stored procedures for Add/Edit Institutional Entity use cases
**********************************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
**********************************************************************************/

/* Insert Institutional Entity stored procedure */
/* Created by Andrew S. */
print '' print '*** creating sp_insert_institutional_entity (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_insert_institutional_entity]
(
	@CompanyName			[nvarchar](100),
	@GivenName				[nvarchar](50),
	@FamilyName				[nvarchar](50),
	@Email					[nvarchar](254),
	@Phone					[nvarchar](13),
	@Address				[nvarchar](50),
	@Address2				[nvarchar](50),
	@Zipcode				[char](9),
	@ContactType			[nvarchar](17),
	@ShelterId				[int]
)

AS
	BEGIN
		INSERT INTO [dbo].[InstitutionalEntity]
		([CompanyName], [GivenName], [FamilyName], [Email], [Phone], [Address]
		, [AddressTwo], [Zipcode], [ContactType], [ShelterId])
		VALUES
		(@CompanyName, @GivenName, @FamilyName, @Email, @Phone, @Address
		, @Address2, @Zipcode, @ContactType, @ShelterId)
	SELECT SCOPE_IDENTITY()
	END
GO


/* Update Institutional Entity stored procedure */
/* Created by Andrew S. */
print '' print '*** creating sp_update_institutional_entity (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_update_institutional_entity]
(
	@InstitutionalEntityId		[int],
	@ShelterId					[int],
	@OldCompanyName				[nvarchar](100),
	@OldGivenName				[nvarchar](50),
	@OldFamilyName				[nvarchar](50),
	@OldEmail					[nvarchar](254),
	@OldPhone					[nvarchar](13),
	@OldAddress					[nvarchar](50),
	@OldAddress2				[nvarchar](50),
	@OldZipcode					[char](9),
	@OldContactType				[nvarchar](17),
	@NewCompanyName				[nvarchar](100),
	@NewGivenName				[nvarchar](50),
	@NewFamilyName				[nvarchar](50),
	@NewEmail					[nvarchar](254),
	@NewPhone					[nvarchar](13),
	@NewAddress					[nvarchar](50),
	@NewAddress2				[nvarchar](50),
	@NewZipcode					[char](9),
	@NewContactType				[nvarchar](17)
)

AS
	BEGIN
		UPDATE	[InstitutionalEntity]
		SET		[CompanyName] 			= @NewCompanyName,
				[GivenName] 			= @NewGivenName,
				[FamilyName] 			= @NewFamilyName,
				[Email] 				= @NewEmail,
				[Phone] 				= @NewPhone,
				[Address]				= @NewAddress,
				[AddressTwo] 			= @NewAddress2,
				[Zipcode] 				= @NewZipcode,
				[ContactType]			= @NewContactType
		WHERE	[InstitutionalEntityId] = @InstitutionalEntityId
		  AND	[ShelterId]				= @ShelterId
		  AND	([CompanyName] 			= @OldCompanyName
				OR ([CompanyName] IS NULL AND @OldCompanyName IS NULL)) 
		  AND	[GivenName] 			= @OldGivenName
		  AND	[FamilyName] 			= @OldFamilyName
		  AND	[Email] 				= @OldEmail
		  AND	[Phone] 				= @OldPhone
		  AND	[Address] 				= @OldAddress
		  AND 	([AddressTwo] 			= @OldAddress2
				OR ([AddressTwo] IS NULL AND @OldAddress2 IS NULL))
		  AND 	[Zipcode] 				= @OldZipcode
		  AND 	[ContactType]         	= @OldContactType
		RETURN @@ROWCOUNT
	END
GO