USE [PetNet_db_am]
GO

/***************************************************************
Andrew Schneider
Created: 2023/03/30

Description:
File containing the stored procedures for Resource Add Requests
**************************************************************
<Updater Name>
Updated: yyyy/mm/dd

Description:
****************************************************************/


/* SelectActiveResourceAddRequest stored procedure */
/* Created by Andrew Schneider on 03/30/2023 */
print '' print '*** creating sp_select_active_resource_add_requests_by_shelter_id (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_select_active_resource_add_requests_by_shelter_id]
(
	@ShelterId					[int]
)
AS
	BEGIN
		SELECT	[ResourceAddRequestId], [UsersId], [Title], [Note], [Active], [ShelterId]
		FROM 	[ResourceAddRequest]
		WHERE	[ResourceAddRequest].[ShelterId] = @ShelterId
		AND		[ResourceAddRequest].[Active] = 1
	END
GO


/* UpdateResourceAddRequestActiveField stored procedure */
/* Created by Andrew Schneider on 03/30/2023 */
print '' print '*** creating sp_update_resource_add_request_active_field (Andrew S.)'
GO
CREATE PROCEDURE [dbo].[sp_update_resource_add_request_active_field]
(
	@ResourceAddRequestId		[int],
	@ShelterId					[int],
	@OldActive					[bit],
	@NewActive					[bit]
)
AS
	BEGIN
		UPDATE	[ResourceAddRequest]
		SET		[Active] 				= @NewActive
		WHERE	[ResourceAddRequestId] 	= @ResourceAddRequestId
		  AND	[ShelterId]				= @ShelterId
		  AND	[Active] 				= @OldActive
		RETURN @@ROWCOUNT
	END
GO