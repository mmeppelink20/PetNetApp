/***************************************************************
Mads Rhea
Created: 2023/04/02

Description:
File contains 
    - a stored procedure to [sp_update_replyvisibility].

**************************************************************
Updated by:
Updated: 202-/--/--

Description: ---
****************************************************************/

USE [PetNet_db_am]
GO

print '' print '*** creating [SP_UPDATE_REPLYVISIBILITY]' 
GO
CREATE PROCEDURE [dbo].[sp_update_replyvisibility]
(
    @ReplyId            [int],
    @ReplyVisibility    [bit],
    @ReplyAdminRemoved  [bit]

)
AS
BEGIN
UPDATE [Reply]
SET [ReplyVisibility] = @ReplyVisibility,
    [ReplyAdminRemoved] = @ReplyAdminRemoved
WHERE [ReplyId] = @ReplyId
RETURN @@ROWCOUNT
END
GO