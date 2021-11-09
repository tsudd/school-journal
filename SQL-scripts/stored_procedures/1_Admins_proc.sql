-- All procedures for 'Admins' table.

USE JournalForSchool
GO

-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllAdmins] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[Admins]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetAdminById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[Admins] WHERE Id=@id
GO

-- Create One.
-- Returns created admin id.
CREATE PROCEDURE [dbo].[sp_CreateAdmin]	
	@id int = NULL,
	@userId int = NULL
AS
BEGIN
	SET NOCOUNT ON 

	
		-- Check if user with such id exists in the table.
		IF EXISTS (SELECT * FROM dbo.Users WHERE Id = @userId)
			BEGIN
				-- Check if admin with such userId already exists.
				IF EXISTS (SELECT TOP 1 * FROM dbo.Admins WHERE UserId = @userId) 
					SELECT TOP 1 Id FROM dbo.Admins WHERE UserId = @userId
				ELSE
					BEGIN
					INSERT INTO Admins
						(
							Id,
							UserId
						) 
					OUTPUT inserted.*
					VALUES 
						(
							@id,
							@userId
						)					
					END
			END		
		ELSE
			DECLARE @MessageText NVARCHAR(4000)	= N'Fail. Cannot create admin with such @userId = %d, user with such id does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText, 16, 1, @userId );
	
END
GO

-- Update one.
CREATE PROCEDURE [dbo].[sp_UpdateAdmin]
	@id int		= NULL,
	@userId int = NULL
AS
BEGIN	

	Begin TRY

	-- Check if user with such id exists in the table.
	IF EXISTS (SELECT TOP 1 * FROM [dbo].[Users] WHERE [dbo].[Users].Id = @userId)
		BEGIN
			-- Check if admin with such Id already exists.
			IF EXISTS (SELECT TOP 1 * FROM [dbo].[Admins] WHERE dbo.Admins.Id= @id)
				BEGIN
				UPDATE dbo.Admins 
				SET UserId=@userId
				WHERE Id=@id
				SELECT TOP 1 * FROM dbo.Admins WHERE dbo.Admins.Id = @id
				END
			ELSE
				DECLARE @MessageText NVARCHAR(4000)	= N'Fail. Cannot update admin with such @id = %d, admin with such id does not exists.';			
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText, 16, 1, @id );
		END
	ELSE
		DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot update admin with such @userId = %d, user with such id does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
		RAISERROR (@MessageText_2, 16, 1, @userId );
	End TRY

	Begin CATCH
		DECLARE 
		@ErrorMessage	NVARCHAR(4000)	= ERROR_MESSAGE(),	-- Message text.
		@ErrorSeverity	INT				= ERROR_SEVERITY(), -- Severity.
		@ErrorState		INT				= ERROR_STATE();	-- State.

		-- Use RAISERROR inside the CATCH block to return error
		-- information about the original error that caused
		-- execution to jump to the CATCH block.
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );
	End CATCH
END
GO


-- Delete One.
-- returns deleted item.
CREATE PROCEDURE [dbo].[sp_DeleteAdmin]
	@id int = NULL
AS
BEGIN
	Begin TRY
		-- Check if admin with such Id already exists.
		IF EXISTS (SELECT TOP 1 * FROM [dbo].[Admins] WHERE dbo.Admins.Id= @id)
			DELETE FROM dbo.Admins 
			OUTPUT deleted.*
			WHERE dbo.Admins.Id = @id
		ELSE
			DECLARE @MessageText NVARCHAR(4000)	= N'Fail. Cannot delete admin with such @id = %d, admin with such id does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText, 16, 1, @id );
	End TRY

	Begin CATCH
		DECLARE 
		@ErrorMessage	NVARCHAR(4000)	= ERROR_MESSAGE(),	-- Message text.
		@ErrorSeverity	INT				= ERROR_SEVERITY(), -- Severity.
		@ErrorState		INT				= ERROR_STATE();	-- State.

		-- Use RAISERROR inside the CATCH block to return error
		-- information about the original error that caused
		-- execution to jump to the CATCH block.
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );
	End CATCH
END
GO