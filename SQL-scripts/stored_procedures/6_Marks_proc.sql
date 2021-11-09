-- All procedures for 'Teachers' table.

USE JournalForSchool
GO

-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllMarks] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[Marks]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetMarkById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[Marks] WHERE Id=@id
GO


-- PROCEDURE FOR LOCAL USE. (in local procedures)
-- PAST THIS PROCEDURE IN THE BEGIN OF PROCEDURES LIKE: (Create, Update);
-- RAISES AN ERROR IF '@timetableId' is not null AND such 'Timetable' doesn't exists;
CREATE PROCEDURE [dbo].[sp_CheckTimetableIdAndUserIdAreCorrect_MarksProc]
	@checkTimetableId	int = NULL,
	@checkUserId		int = NUll
AS
BEGIN
	IF @checkTimetableId IS NOT NULL AND @checkUserId IS NOT NULL
		-- For '@timetableId' check.	
		IF EXISTS (SELECT TOP 1 * FROM dbo.Timetables WHERE dbo.Timetables.Id = @checkTimetableId)
			-- For '@userId' check.	
			IF EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Id = @checkUserId)
				RETURN
			ELSE 		
				BEGIN
				DECLARE @MessageText_MarksProc_0 NVARCHAR(4000)	= N'Fail. Cannot create/update Mark , cuz "userId" with such @userId = %d does not exists.';			
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_MarksProc_0, 16, 1, @checkUserId);
				END		
		ELSE
			BEGIN
			DECLARE @MessageText_MarksProc_1 NVARCHAR(4000)	= N'Fail. Cannot create/update Mark , cuz "timeTable" with such @timetableId = %d does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_MarksProc_1, 16, 1, @checkTimetableId);
			END
	ELSE
		BEGIN
		DECLARE @MessageText_MarksProc_2 NVARCHAR(4000)	= N'Fail. Cannot create/update Mark , cuz @timetableId or @userId IS NULL.';			
		-- RAISERROR with severity 11-19 will cause execution to 
		-- jump to the CATCH block.
		RAISERROR (@MessageText_MarksProc_2, 16, 1 );
		END	
END
GO


-- PROCEDURE FOR LOCAL USE. (in local procedures like UPDATE)
-- Check if fields is unique across current table.
CREATE PROCEDURE [dbo].[sp_CheckIfFieldsAreCorrect_MarksProc]
	@checkCurrentMarkId		int			= NULL, -- For update function we should pass an 'Id' for excluding current. 
	@checkUserId			int			= NULL,
	@checkTimetableId		int			= NULL,
	@checkSelectedIndex		int			= NULL,
	@checkDate				varchar(50)	= NULL
AS
BEGIN

	-- 0. Check if such '@checkUserId' and '@checkTimetableId' exists in their tables.
	exec sp_CheckTimetableIdAndUserIdAreCorrect_MarksProc @checkTimetableId=@checkTimetableId, @checkUserId=@checkUserId;

	-- 1. Check if something is NULL.
	IF @checkCurrentMarkId IS NOT NULL AND @checkUserId IS NOT NULL AND @checkTimetableId IS NOT NULL AND @checkSelectedIndex IS NOT NULL AND @checkDate IS NOT NULL
		-- 2. Check if '@checkCurrentMarkId' is exists in 'Marks'.
		IF EXISTS (SELECT TOP 1 * FROM dbo.Marks WHERE dbo.Marks.Id = @checkCurrentMarkId)						
			-- 3. Check if Mark with all that parameters is already in the table and its 'id' != '@checkCurrentMarkId'.
			IF NOT EXISTS (SELECT TOP 1 * FROM dbo.Marks WHERE 
				(UserId = @checkUserId AND 
				TimeTableId = @checkTimetableId AND 
				SelectedIndex = @checkSelectedIndex AND
				Marks.Date = @checkDate) AND (Id = @checkCurrentMarkId) )
					RETURN			
			ELSE
				BEGIN
				DECLARE @MessageText_MarksProc_5 NVARCHAR(4000)	= N'Fail. Cannot update Mark , cuz Mark with exact the same parameters already exists with differernt "id".';
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_MarksProc_5, 16, 1 );
				END		
		ELSE
			BEGIN
			DECLARE @MessageText_MarksProc_3 NVARCHAR(4000)	= N'Fail. Cannot update/delete Mark , Mark with current id = %d does not exists.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_MarksProc_3, 16, 1, @checkCurrentMarkId);
			END		
	ELSE
		BEGIN
		DECLARE @MessageText_MarksProc_4 NVARCHAR(4000)	= N'Fail. Cannot update Mark , cuz some parameters are NULL.';
		-- RAISERROR with severity 11-19 will cause execution to 
		-- jump to the CATCH block.
		RAISERROR (@MessageText_MarksProc_4, 16, 1 );
		END		
END
GO


-- Create One.
-- Returns created item id.
CREATE PROCEDURE [dbo].[sp_CreateMark]	
	@userId			int			= NULL,
	@timetableId	int			= NULL,
	@selectedIndex	int			= NULL,
	@date			varchar(50)	= NULL
AS
BEGIN
	SET NOCOUNT ON 

	Begin TRY
		-- Check if '@userId' and '@timetableId' are correct (means if they already exists in their tables).
		exec dbo.sp_CheckTimetableIdAndUserIdAreCorrect_MarksProc @checkTimetableId=@timetableId, @checkUserId=@userId;		

		INSERT INTO [dbo].[Marks] 
			(							
				UserId,
				TimeTableId,
				SelectedIndex,
				Date
			)
			OUTPUT inserted.*
			VALUES 
			(							
				@userId,
				@timetableId,
				@selectedIndex,
				@date
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
CREATE PROCEDURE [dbo].[sp_UpdateMark]
	@id				int			= NULL,
	@userId			int			= NULL,
	@timetableId	int			= NULL,
	@selectedIndex	int			= NULL,
	@date			varchar(50)	= NULL
AS
BEGIN	

	Begin TRY

		-- Check if all parameters are correct.
		exec sp_CheckIfFieldsAreCorrect_MarksProc 
			@checkCurrentMarkId=@id, 
			@checkUserId=@userId, 
			@checkTimetableId=@timetableId, 
			@checkSelectedIndex=@selectedIndex,
			@checkDate=@date;
		
		-- All is valid. Make update.
		BEGIN
			UPDATE dbo.Marks
			SET 
				UserId = @userId,
				TimeTableId = @timetableId,
				SelectedIndex = @selectedIndex,
				Date = @date
			WHERE dbo.Marks.Id = @id;
			SELECT TOP 1 * FROM dbo.Marks WHERE dbo.Marks.Id = @id;
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
CREATE PROCEDURE [dbo].[sp_DeleteMark]
	@id int = NULL
AS
BEGIN

	Begin TRY				
		-- Check if 'Mark' with '@id' exists.
		IF EXISTS (SELECT TOP 1 * FROM dbo.Marks WHERE Id = @id)
			-- All is validated, make delete.
			DELETE FROM dbo.Marks
			OUTPUT deleted.*
			WHERE dbo.Marks.Id = @id
		ELSE
			BEGIN
			DECLARE @MessageText_MarksProc_5 NVARCHAR(4000)	= N'Fail. delete Mark , cuz Mark with current id = %d does not exists.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_MarksProc_5, 16, 1, @id);
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

-- GetMarkSelectedIndex
-- Raise an error if  doesn't exists.
-- Return Mark with if exists.
CREATE PROCEDURE [dbo].[sp_GetMarkSelectedIndex]	
	@userId			int			= NULL,
	@timetableId	int			= NULL,	
	@date			varchar(50)	= NULL
AS
BEGIN

	Begin TRY
		-- Check if '@userId' and '@timetableId' are correct (means if they already exists in their tables).
		exec dbo.sp_CheckTimetableIdAndUserIdAreCorrect_MarksProc @checkTimetableId=@timetableId, @checkUserId=@userId;
		
		IF EXISTS (SELECT TOP 1 * FROM dbo.Marks WHERE UserId = @userId AND TimeTableId = @timetableId AND Date = @date)
			SELECT TOP 1 * FROM dbo.Marks WHERE UserId = @userId AND TimeTableId = @timetableId AND Date = @date
		ELSE
			BEGIN
			DECLARE @MessageText_MarksProc_52 NVARCHAR(4000)	= N'Fail. Cannot get Mark , cuz Mark with exact the same parameters does not exist.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_MarksProc_52, 16, 1 );
			END
	End TRY

	Begin Catch
		DECLARE 
		@ErrorMessage	NVARCHAR(4000)	= ERROR_MESSAGE(),	-- Message text.
		@ErrorSeverity	INT				= ERROR_SEVERITY(), -- Severity.
		@ErrorState		INT				= ERROR_STATE();	-- State.

		-- Use RAISERROR inside the CATCH block to return error
		-- information about the original error that caused
		-- execution to jump to the CATCH block.
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );
	End Catch
END
GO

-- DeleteIfExist_MarksProc
CREATE PROCEDURE [dbo].[sp_DeleteIfExist_MarksProc]	
	@userId			int			= NULL,
	@timetableId	int			= NULL,	
	@date			varchar(50)	= NULL
AS
BEGIN
	IF EXISTS (SELECT TOP 1 * FROM dbo.Marks WHERE UserId = @userId AND TimeTableId = @timetableId AND Date = @date)
		DELETE FROM dbo.Marks
		OUTPUT deleted.*
		WHERE dbo.Marks.Date = @date AND UserId = @userId AND TimeTableId = @timetableId;
	ELSE
		RETURN
END
GO
