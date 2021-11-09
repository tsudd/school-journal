-- All procedures for 'TheClasses' table.

USE JournalForSchool
GO

-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllClasses] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[TheClasses]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetClassById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[TheClasses] WHERE Id=@id
GO

-- Create One.
-- Returns created item id.
CREATE PROCEDURE [dbo].[sp_CreateClass]	
	@theClass		int			= 11,
	@classLetter	nchar(10)	= 'A'
AS
BEGIN
	SET NOCOUNT ON 

	-- Check if subject with such '@theClass' and '@classLetter' exists in the table.
	IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[TheClasses] WHERE [dbo].[TheClasses].TheClass = @theClass AND [dbo].[TheClasses].ClassLetter = @classLetter)
		BEGIN
			INSERT INTO [dbo].[TheClasses] 
						(							
							TheClass,
							ClassLetter
						)
					OUTPUT inserted.*
					VALUES 
						(							
							@theClass,
							@classLetter
						)			
		END
	ELSE
		SELECT TOP 1 * FROM [dbo].[TheClasses] WHERE [dbo].[TheClasses].ClassLetter = @classLetter AND [dbo].[TheClasses].TheClass = @theClass;
END
GO

-- Update one.
CREATE PROCEDURE [dbo].[sp_UpdateClass]
	@id				int			= NULL,
	@theClass		int			= 11,
	@classLetter	nchar(10)	= 'A'
AS
BEGIN	

	Begin TRY
		-- Check if subject with such '@id' exists in the table.
		IF EXISTS (SELECT TOP 1 * FROM [dbo].TheClasses WHERE [dbo].TheClasses.Id = @id)
			BEGIN
				-- Check if subject with updated values already exists in the table.
				IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].TheClasses WHERE [dbo].TheClasses.TheClass = @theClass AND [dbo].TheClasses.ClassLetter = @classLetter)
					BEGIN
						UPDATE 
							dbo.TheClasses 
						SET 
							TheClass = @theClass,
							ClassLetter = @classLetter
						WHERE 
							Id = @id
						SELECT TOP 1 * FROM dbo.TheClasses where dbo.TheClasses.Id= @id
					END
				ELSE
					-- Check if we want to update object to the same object as it is.			
					IF EXISTS (SELECT * FROM (SELECT * FROM dbo.TheClasses WHERE dbo.TheClasses.Id= @id) as a WHERE a.theClass = @theClass AND a.classLetter = @classLetter)
						SELECT * FROM dbo.TheClasses WHERE dbo.TheClasses.Id= @id
					ELSE
						BEGIN
						DECLARE @MessageText NVARCHAR(4000)	= N'Fail. Cannot update theClasses with such @id = %d, cuz theclass with the same @theClass = %d and @classLetter = %s already exists.';			
						-- RAISERROR with severity 11-19 will cause execution to 
						-- jump to the CATCH block.
						RAISERROR (@MessageText, 16, 1, @id, @theClass, @classLetter );
						END
			END
		ELSE
			BEGIN
			DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot update theClass with such @id = %d, cuz theClass with that id does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_2, 16, 1, @id );
			END
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
CREATE PROCEDURE [dbo].[sp_DeleteClass]
	@id int = NULL
AS
BEGIN
	Begin TRY
		-- Check if subject with such '@id' exists in the table.
		IF EXISTS (SELECT TOP 1 * FROM [dbo].TheClasses WHERE [dbo].TheClasses.Id = @id)
			DELETE FROM dbo.TheClasses
			OUTPUT deleted.*
			WHERE dbo.TheClasses.Id = @id
		ELSE
			BEGIN
			DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot delete theClass with such @id = %d, cuz theClass with that id does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_2, 16, 1, @id );
			END
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

-- Non standarts procedures --

-- Returns letters of class number in nice order.
CREATE PROCEDURE [dbo].[sp_GetTheClassesLetters]
	@theClass int = 11
AS
BEGIN
	SELECT ClassLetter FROM (SELECT * FROM dbo.TheClasses WHERE TheClass = @theClass)
		as a 
		ORDER BY ClassLetter ASC;
END
GO

-- Get class without id.
CREATE PROCEDURE [dbo].[sp_GetTheClassByNumber]
	@theClass		int			= 11,
	@classLetter	nchar(10)	= 'A'
AS
BEGIN
	SELECT TOP 1 * FROM dbo.TheClasses 
	WHERE 
		(dbo.TheClasses.TheClass = @theClass AND dbo.TheClasses.ClassLetter = @classLetter);
END
GO

-- Another functions that needed on backend side could be fine implemented by using that sp.