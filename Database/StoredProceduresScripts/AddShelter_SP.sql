/***************************************************************
Brian Collum
Created: 2023/02/23

Description:
File containing stored procedure for the  use case
**************************************************************/


USE [PetNet_db_am]

print '' print '*** Creating sp_add_shelter (Brian Collum)'
GO
CREATE PROCEDURE	[dbo].[sp_add_shelter]
(
	@ShelterName	[nvarchar](50),
	@Address		[nvarchar](50),
	@AddressTwo		[nvarchar](50),
	@Zipcode		[char](9),
	@Phone			[nvarchar](13),
	@Email			[nvarchar](254),
	@Areasofneed	[nvarchar](max),
	@ShelterActive	[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[Shelter]
		([ShelterName],		[Address],		[AddressTwo],		[Zipcode],		[Phone],	[Email],	[Areasofneed],		[ShelterActive])
		VALUES(
		@ShelterName,	@Address,	@AddressTwo,	@Zipcode,	@Phone,	@Email,	@Areasofneed,	@ShelterActive
		);
		SELECT
			[ShelterId],
			[ShelterName],
			[Address],
			[AddressTwo],
			[Zipcode],
			[Phone],
			[Email],
			[Areasofneed],
			[ShelterActive]
		FROM [dbo].[Shelter]
		WHERE	@ShelterName = ShelterName
		RETURN	@@ROWCOUNT
	END
GO