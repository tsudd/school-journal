-- All procedures for 'Teachers' table.

USE JournalForSchool
GO

-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllTeachers] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[Teachers]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetTeacherById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[Teachers] WHERE Id=@id
GO

-- Create One.
-- Returns created item id.
CREATE PROCEDURE [dbo].[sp_CreateTeacher]	
	@userId				int	= NULL,
	@subjectId			int	= NULL, -- ѕредположим что учитель может иметь несколько предметов, было бы хорошо указать это €вно на уровне таблицы или вроде того
	@theClassMentorId	int = NULL  -- Ќе нужное поле на уровне таблицы, поэтому в данной процедуре не учитываю валидацию дл€ этого параметра.
AS
BEGIN
	SET NOCOUNT ON 

	Begin TRY
	-- Check if teacher with such '@userId' and '@subjectId' already exists in the table.
	IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[Teachers] WHERE [dbo].[Teachers].SubjectId= @subjectId AND dbo.Teachers.UserId = @userId)
		-- Check if 'User' with such '@userId' exists in 'Users' table.
		IF EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Id = @userId)
			-- Check if 'Subject' from 'Subjects' table with such '@subjectId' exists.
			IF EXISTS (SELECT TOP 1 * FROM dbo.Subjects WHERE dbo.Subjects.Id = @subjectId)
				BEGIN
				INSERT INTO [dbo].[Teachers] 
					(							
						UserId,
						SubjectId,
						TheClassMentorId
					)
					OUTPUT inserted.*
					VALUES 
					(							
						@userId,
						@subjectId,
						@theClassMentorId
					)			
				END
			ELSE
				BEGIN
				DECLARE @MessageText_4 NVARCHAR(4000)	= N'Fail. Cannot create teacher with such cuz Subject with such @subjectId does not exists.';			
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_4, 16, 1);
				END
		ElSE
			BEGIN
			DECLARE @MessageText_3 NVARCHAR(4000)	= N'Fail. Cannot create teacher , cuz User with such @userId does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_3, 16, 1 );
			END
	ELSE		
		SELECT TOP 1 * FROM [dbo].[Teachers] WHERE [dbo].[Teachers].SubjectId= @subjectId AND dbo.Teachers.UserId = @userId
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
CREATE PROCEDURE [dbo].[sp_UpdateTeacher]
	@id					int = NULL,
	@userId				int	= NULL,
	@subjectId			int	= NULL, 
	@theClassMentorId	int = NULL 
AS
BEGIN	

	Begin TRY
		-- Check if teacher with such '@userId' and '@subjectId'  exists in the table.
		IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[Teachers] WHERE [dbo].[Teachers].SubjectId = @subjectId AND dbo.Teachers.UserId = @userId)
			BEGIN
				-- Check if Teacher with such '@id' exists.
				IF EXISTS (SELECT TOP 1 * FROM [dbo].[Teachers] WHERE [dbo].[Teachers].Id = @id)
					-- Check if 'User' with such '@userId' exists in 'Users' table.
					IF EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Id = @userId)
						-- Check if Subject from 'Subjects' table with such '@subjectId' exists.
						IF EXISTS (SELECT TOP 1 * FROM dbo.Subjects WHERE dbo.Subjects.Id = @subjectId)
							-- All is valid. Make update.
							BEGIN
								UPDATE dbo.Teachers
								SET 
									UserId			= @userId,
									SubjectId		= @subjectId,
									TheClassMentorId = @theClassMentorId
								WHERE dbo.Teachers.Id = @id
								SELECT TOP 1 * FROM dbo.Teachers WHERE dbo.Teachers.Id = @id
							END
						Else
							BEGIN
							DECLARE @MessageText_4 NVARCHAR(4000)	= N'Fail. Cannot update teacher with such @id = %d, cuz Subject with such @subjectId does not exists.';			
							-- RAISERROR with severity 11-19 will cause execution to 
							-- jump to the CATCH block.
							RAISERROR (@MessageText_4, 16, 1, @id );
							END
					ELSE 
						BEGIN
						DECLARE @MessageText_3 NVARCHAR(4000)	= N'Fail. Cannot update teacher with such @id = %d, cuz User with such @userId does not exists.';			
						-- RAISERROR with severity 11-19 will cause execution to 
						-- jump to the CATCH block.
						RAISERROR (@MessageText_3, 16, 1, @id );
						END
				ELSE
					BEGIN
					DECLARE @MessageText NVARCHAR(4000)	= N'Fail. Cannot update teacher with such @id = %d, teacher with such id does not exists.';			
					-- RAISERROR with severity 11-19 will cause execution to 
					-- jump to the CATCH block.
					RAISERROR (@MessageText, 16, 1, @id );
					END
			END
		ELSE
			-- Check if we want to update object to the same object as it is.			
			IF EXISTS (SELECT * FROM (SELECT * FROM dbo.Teachers WHERE dbo.Teachers.Id = @id) as a WHERE a.SubjectId= @subjectId AND a.UserId = @userId)
				SELECT TOP 1 * FROM dbo.Teachers WHERE dbo.Teachers.Id = @id
			ELSE
				BEGIN
				DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot update teacher with such @id = %d and @userId and @subjectId = (%d, %d), cuz such combination of (userId and subjectId) already used for another teacher.';			
				-- RAISERROR with severity 11-19 will cause execution to 
				-- jump to the CATCH block.
				RAISERROR (@MessageText_2, 16, 1, @id, @userId, @subjectId );
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
CREATE PROCEDURE [dbo].[sp_DeleteTeacher]
	@id int = NULL
AS
BEGIN

	Begin TRY
		-- Check if teacher with such '@id' exists in the table.
		IF EXISTS (SELECT TOP 1 * FROM [dbo].[Teachers] WHERE [dbo].[Teachers].Id= @id)
			DELETE FROM dbo.Teachers
			OUTPUT deleted.*
			WHERE dbo.Teachers.Id = @id
		ELSE
			BEGIN
			DECLARE @MessageText_2 NVARCHAR(4000)	= N'Fail. Cannot delete teacher with such @id = %d, cuz that id does not exists.';			
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

-- Returns table with every teachers fullname in one column 'FullName'.
CREATE PROCEDURE [dbo].[sp_GetAllTeachersNames]
AS
BEGIN
	SET NOCOUNT ON

	SELECT CONCAT(a.FirstName, ' ', a.MiddleName, ' ', a.LastName) as FullName 
	FROM 
	(
		SELECT DISTINCT Users.Id as id, Users.FirstName as FirstName, Users.MiddleName as MiddleName, Users.LastName as LastName 
		FROM Teachers, Users
		WHERE Teachers.UserId = Users.Id
	) as a;
END
GO