-- All procedures for 'Subjects' table.

-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllSubjects] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[Subjects]
GO

-- Get All Names.
CREATE PROCEDURE [dbo].[sp_GetAllSubjectNames] 	
AS
	SET NOCOUNT ON
	SELECT SubjectName FROM [dbo].[Subjects]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetSubjectById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[Subjects] WHERE Id=@id
GO

-- Get One name.
CREATE PROCEDURE [dbo].[sp_GetSubjectNameById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 SubjectName FROM [dbo].[Subjects] WHERE Id=@id
GO

-- Get One id.
CREATE PROCEDURE [dbo].[sp_GetSubjectIdByName]
	@subjectName nvarchar(50) = 'Default subject name'
AS
	SET NOCOUNT ON
	SELECT TOP 1 Id FROM [dbo].[Subjects] WHERE SubjectName=@subjectName
GO

-- Create One.
-- Returns created item id.
CREATE PROCEDURE [dbo].[sp_CreateSubject]	
	@subjectName nvarchar(50) = 'Default subject name'
AS
BEGIN
	SET NOCOUNT ON 

	-- Check if subject with such '@subjectName' exists in the table.
	IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[Subjects] WHERE [dbo].[Subjects].SubjectName = @subjectName)
		BEGIN
			INSERT INTO [dbo].[Subjects] 
						(
							Id,
							SubjectName
						)
					OUTPUT inserted.*
					VALUES 
						(
							(SELECT ISNULL(MAX(Id) + 1, 1) FROM [dbo].Subjects),
							@subjectName
						)			
		END
	ELSE
		SELECT TOP 1 * FROM [dbo].[Subjects] WHERE [dbo].[Subjects].SubjectName = @subjectName

END
GO

-- Update one.
CREATE PROCEDURE [dbo].[sp_UpdateSubject]
	@id				int				= NULL,
	@subjectName	nvarchar(50)	= 'Default subject name'
AS
BEGIN	

	Begin TRY
		-- Check if subject with such '@subjectName' exists in the table.
		IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[Subjects] WHERE [dbo].[Subjects].SubjectName = @subjectName)
			BEGIN
				-- Check if subject with such '@id' exists.
				IF EXISTS (SELECT TOP 1 * FROM [dbo].[Subjects] WHERE [dbo].[Subjects].Id = @id)
					BEGIN
						UPDATE dbo.Subjects 
						SET SubjectName = @subjectName
						WHERE Id = @id
						SELECT TOP 1 * FROM dbo.Subjects WHERE dbo.Subjects.Id = @id
					END
				ELSE
					BEGIN
					DECLARE @MessageText NVARCHAR(4000)	= N'Fail. Cannot update subject with such @id = %d, subject with such id does not exists.';			
					-- RAISERROR with severity 11-19 will cause execution to 
					-- jump to the CATCH block.
					RAISERROR (@MessageText, 16, 1, @id );
					END
			END
		ELSE
			-- Check if we want to update object to the same object as it is.			
			IF EXISTS (SELECT * FROM (SELECT * FROM dbo.Subjects WHERE dbo.Subjects.Id = @id) as a WHERE a.SubjectName = @subjectName)
				SELECT TOP 1 * FROM dbo.Subjects WHERE dbo.Subjects.Id = @id
			ELSE
				BEGIN
				DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot update subject with such @id = %d and @subjectName = %s, cuz such subjectName already used for another subject.';			
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_2, 16, 1, @id, @subjectName );
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
CREATE PROCEDURE [dbo].[sp_DeleteSubject]
	@id int = NULL
AS
BEGIN

	Begin TRY
		-- Check if subject with such '@id' exists in the table.
		IF EXISTS (SELECT TOP 1 * FROM [dbo].[Subjects] WHERE [dbo].[Subjects].Id= @id)
			DELETE FROM dbo.Subjects
			OUTPUT deleted.*
			WHERE dbo.Subjects.Id = @id
		ELSE
			BEGIN
			DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot delete subject with such @id = %d, cuz subjectwith that id does not exists.';			
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