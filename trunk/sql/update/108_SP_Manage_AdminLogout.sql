set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER PROC [dbo].[SP_Manage_AdminLogout]
(
	@vcAdminName VARCHAR(50)
)
AS
	UPDATE admin SET cIsOnline = 'N' WHERE vcAdminName =@vcAdminName
	DELETE AdminOnline WHERE vcAdminName =@vcAdminName

