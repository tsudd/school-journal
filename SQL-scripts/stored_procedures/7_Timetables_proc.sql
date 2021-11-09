-- All procedures for 'Teachers' table.

USE JournalForSchool
GO


-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllTimetables] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[Timetables]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetTimetableById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[Timetables] WHERE Id=@id
GO


-- PROCEDURE FOR LOCAL USE. (in local procedures)
-- PAST THIS PROCEDURE IN THE BEGIN OF PROCEDURES LIKE: (Create, Update);
-- RAISES AN ERROR IF '@timetableId' is not null AND such 'Timetable' doesn't exists;
CREATE PROCEDURE [dbo].[sp_CheckForeignRefsAreCorrect_TimetablesProc]
	@subjectId		int = NULL,
	@classId		int = NULL,
	@teacherId		int = NULL
AS
BEGIN
	IF @subjectId IS NOT NULL AND @classId IS NOT NULL AND @teacherId IS NOT NULL
		-- For '@subjectId' check.	
		IF EXISTS (SELECT TOP 1 * FROM dbo.Subjects WHERE dbo.Subjects.Id = @subjectId)
			-- For '@classId' check.	
			IF EXISTS (SELECT TOP 1 * FROM dbo.TheClasses WHERE dbo.TheClasses.Id = @classId)
				-- For '@teacherId' check.
				IF EXISTS (SELECT TOP 1 * FROM dbo.Teachers WHERE dbo.Teachers.Id = @teacherId)
					RETURN
				ElSE
					BEGIN
					DECLARE @MessageText_TimetableProc_12 NVARCHAR(4000)	= N'Fail. Cannot create/update Timetable , cuz Teacher with such @teacherId = %d does not exists.';			
					-- RAISERROR with severity 11-19 will cause execution to 
					-- jump to the CATCH block.
					RAISERROR (@MessageText_TimetableProc_12, 16, 1, @teacherId);
					END	
			ELSE 		
				BEGIN
				DECLARE @MessageText_TimetableProc_0 NVARCHAR(4000)	= N'Fail. Cannot create/update Timetable , cuz Class with such @classId = %d does not exists.';			
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_TimetableProc_0, 16, 1, @classId);
				END		
		ELSE
			BEGIN
			DECLARE @MessageText_TimetableProc_1 NVARCHAR(4000)	= N'Fail. Cannot create/update Timetable , cuz Subject with such @subjectId = %d does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_TimetableProc_1, 16, 1, @subjectId);
			END
	ELSE
		BEGIN
		DECLARE @MessageText_TimetableProc_2 NVARCHAR(4000)	= N'Fail. Cannot create/update Timetable , cuz some parameter IS NULL.';			
		-- RAISERROR with severity 11-19 will cause execution to 
		-- jump to the CATCH block.
		RAISERROR (@MessageText_TimetableProc_2, 16, 1 );
		END	
END
GO


-- PROCEDURE FOR LOCAL USE. (in local procedures like UPDATE)
-- Check if fields is unique across current table.
CREATE PROCEDURE [dbo].[sp_CheckIfFieldsAreCorrect_TimetablesProc]
	@id				int = NULL,
	@lessonNumber	int = NULL,
	@subjectId		int = NULL,
	@classId		int = NULL,
	@teacherId		int = NULL,
	@monday			int = NULL,
	@tuesday		int = NULL,
	@wednesday		int = NULL,
	@thursday		int = NULL,
	@friday			int = NULL
AS
BEGIN

	-- 0. Check if internal ids exists in their tables.	
	-- Check if '@subjectId' and '@classId' and '@teacherId' are correct (means if they already exists in their tables).
	exec dbo.sp_CheckForeignRefsAreCorrect_TimetablesProc @subjectId=@subjectId, @classId=@classId, @teacherId=@teacherId;

	-- 1. Check if something is NULL.
	IF @id IS NOT NULL AND @lessonNumber IS NOT NULL AND @subjectId IS NOT NULL AND @classId IS NOT NULL AND @teacherId IS NOT NULL
		-- 2. Check if '@id' is exists in 'Timetables'.
		IF EXISTS (SELECT TOP 1 * FROM dbo.Timetables WHERE dbo.Timetables.Id = @id)						
			-- 3. Check if Timetable with all that parameters is already in the table and its 'id' != '@id'.
			IF NOT EXISTS (SELECT TOP 1 * FROM dbo.Timetables WHERE 
				(
					LessonNumber = @lessonNumber AND
					SubjectId = @subjectId AND
					ClassId = @classId AND 
					TeacherId = @teacherId AND
					Monday = @monday AND
					Tuesday = @tuesday AND
					Wednesday = @wednesday AND
					Thursday = @thursday AND
					Friday = @friday ) AND (Id = @id) )
					RETURN			
			ELSE
				BEGIN
				DECLARE @MessageText_TimetablesProc_5 NVARCHAR(4000)	= N'Fail. Cannot update Timetable , cuz Timetable with exact the same parameters already exists with differernt "id".';
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_TimetablesProc_5, 16, 1 );
				END		
		ELSE
			BEGIN
			DECLARE @MessageText_TimetablesProc_6 NVARCHAR(4000)	= N'Fail. Cannot update/delete Timetable , Timetable with current id = %d does not exists.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_TimetablesProc_6, 16, 1, @id);
			END		
	ELSE
		BEGIN
		DECLARE @MessageText_TimetablesProc_4 NVARCHAR(4000)	= N'Fail. Cannot update Timetable , cuz some parameters are NULL. \n 
			List of parameters : 
				@id = %d,
				@lessonNumber = %d,
				@subjectId = %d,
				@classId = %d,
				@teacherId = %d,
				@monday = %d,
				@tuesday = %d,
				@wednesday = %d,
				@thursday = %d,
				@friday = %d;'
		-- RAISERROR with severity 11-19 will cause execution to 
		-- jump to the CATCH block.
		RAISERROR (@MessageText_TimetablesProc_4, 16, 1, @id, @lessonNumber, @subjectId, @classId, @teacherId, @monday, @tuesday, @wednesday, @thursday, @friday);
		END		
END
GO


-- Create One.
-- Returns created item id.
CREATE PROCEDURE [dbo].[sp_CreateTimetable]	
	@lessonNumber	int = NULL,
	@subjectId		int = NULL,
	@classId		int = NULL,
	@teacherId		int = NULL,
	@monday			int = NULL,
	@tuesday		int = NULL,
	@wednesday		int = NULL,
	@thursday		int = NULL,
	@friday			int = NULL
AS
BEGIN
	SET NOCOUNT ON 

	Begin TRY
		-- Check if '@subjectId' and '@classId' and '@teacherId' are correct (means if they already exists in their tables).
		exec dbo.sp_CheckForeignRefsAreCorrect_TimetablesProc @subjectId=@subjectId, @classId=@classId, @teacherId=@teacherId;

		INSERT INTO [dbo].[Timetables] 
			(							
				LessonNumber,
				SubjectId,
				ClassId,
				TeacherId,
				Monday,
				Tuesday,
				Wednesday,
				Thursday,
				Friday
			)
			OUTPUT inserted.*
			VALUES 
			(							
				@lessonNumber,
				@subjectId,
				@classId,
				@teacherId,
				@monday,
				@tuesday,
				@wednesday,
				@thursday,
				@friday
			)					
	End TRY

	-- CATCH ERRORS.
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


-- Update one.
CREATE PROCEDURE [dbo].[sp_UpdateTimetable]
	@id				int	= NULL,
	@lessonNumber	int = NULL,
	@subjectId		int = NULL,
	@classId		int = NULL,
	@teacherId		int = NULL,
	@monday			int = NULL,
	@tuesday		int = NULL,
	@wednesday		int = NULL,
	@thursday		int = NULL,
	@friday			int = NULL
AS
BEGIN	

	Begin TRY

		-- Check if all parameters are correct.
		exec sp_CheckIfFieldsAreCorrect_TimetablesProc 
			@id=@id, 
			@lessonNumber=@lessonNumber,
			@subjectId=@subjectId, 
			@classId=@classId, 
			@teacherId=@teacherId,
			@monday=@monday,
			@tuesday=@tuesday,
			@wednesday=@wednesday,
			@thursday=@thursday,
			@friday=@friday;
			
		
		-- All is valid. Make update.
		BEGIN
			UPDATE dbo.Timetables
			SET 				
				LessonNumber = @lessonNumber,
				SubjectId = @subjectId,
				ClassId = @classId,
				TeacherId = @teacherId,
				Monday = @monday,
				Tuesday = @tuesday,
				Wednesday = @wednesday,
				Thursday = @thursday,
				Friday = @friday
			WHERE dbo.Timetables.Id = @id;
			SELECT TOP 1 * FROM dbo.Timetables WHERE dbo.Timetables.Id = @id;
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
CREATE PROCEDURE [dbo].[sp_DeleteTimetable]
	@id int = NULL
AS
BEGIN

	Begin TRY				
		-- Check if 'Timetable' with '@id' exists.
		IF EXISTS (SELECT TOP 1 * FROM dbo.Timetables WHERE Id = @id)
			-- All is validated, make delete.
			DELETE FROM dbo.Timetables
			OUTPUT deleted.*
			WHERE dbo.Timetables.Id = @id
		ELSE
			BEGIN
			DECLARE @MessageText_TimetableProc_545 NVARCHAR(4000)	= N'Fail. delete Timetable , cuz timetable with current id = %d does not exists.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_TimetableProc_545, 16, 1, @id);
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


-- More procedures. (non-standart)
