-- All procedures for 'Teachers' table.

USE JournalForSchool
GO

-- NOTE :
--		only UPDATE and DELETE procedures shouldn't have 'SET NOCOUNT ON'.

-- Get All.
CREATE PROCEDURE [dbo].[sp_GetAllUsers] 	
AS
	SET NOCOUNT ON
	SELECT * FROM [dbo].[Users]
GO

-- Get One.
CREATE PROCEDURE [dbo].[sp_GetUserById]
	@id int = NULL
AS
	SET NOCOUNT ON
	SELECT TOP 1 * FROM [dbo].[Users] WHERE Id=@id
GO

-- PROCEDURE FOR LOCAL USE. (in local procedures)
-- PAST THIS PROCEDURE IN THE BEGIN OF PROCEDURES LIKE: (Create, Update);
-- RAISE AN ERROR IF @classesId is not null AND such 'TheClass' doesn't exists;
CREATE PROCEDURE [dbo].[sp_CheckClassesIdIsCorrect]
	@classesId int = NULL
AS
BEGIN
	IF @classesId IS NOT NULL
		BEGIN
		IF EXISTS (SELECT TOP 1 * FROM dbo.TheClasses WHERE dbo.TheClasses.Id = @classesId)
			RETURN
		ELSE
			BEGIN
			DECLARE @MessageText_522 NVARCHAR(4000)	= N'Fail. Cannot create/update User , cuz TheClass with such @classesId = %d does not exists.';			
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_522, 16, 1, @classesId );
			END
		END
	ELSE
		RETURN
END
GO

-- PROCEDURE FOR LOCAL USE. (in local procedures)
-- Check if fields is unique across current table.
CREATE PROCEDURE [dbo].[sp_CheckIfFieldsIsUnique]
	@checkId				int		= NULL, -- For update function we should pass an 'Id' for excluding current. 
	@checkLogin		nvarchar(50)	= NULL,
	@checkPhone		nvarchar(50)	= NULL
AS
BEGIN
	IF @checkId IS NULL
		-- If not Unique.
		IF EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Login = @checkLogin OR dbo.Users.Phone = @checkPhone)
			BEGIN
			DECLARE @MessageText_523 NVARCHAR(4000)	= N'Fail. Cannot create/update User , cuz Phone/Login already in use for some User.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_523, 16, 1);
			END
		ELSE
			RETURN
	ELSE		
		-- Check if '@checkId' is exists in 'Users'.
		IF NOT EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Id = @checkId)
			BEGIN
			DECLARE @MessageText_524 NVARCHAR(4000)	= N'Fail. Cannot update/delete User , cuz User with current id = %d does not exists.';
			-- RAISERROR with severity 11-19 will cause execution to 
			-- jump to the CATCH block.
			RAISERROR (@MessageText_524, 16, 1, @checkId);
			END
		ElSE
			-- checking in case if only checking '@checkId' is required. (for delete operation)
			IF @checkLogin IS NOT NULL AND @checkPhone IS NOT NULL
				-- If not Unique AND User.Id != @checkId
				BEGIN
				IF EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE (dbo.Users.Login = @checkLogin OR dbo.Users.Phone = @checkPhone) AND dbo.Users.Id != @checkId)
					BEGIN
					DECLARE @MessageText_525 NVARCHAR(4000)	= N'Fail. Cannot create/update User , cuz Phone/Login already in use for some User, excluded user with current id = %d.';
					-- RAISERROR with severity 11-19 will cause execution to 
					-- jump to the CATCH block.
					RAISERROR (@MessageText_525, 16, 1, @checkId);
					END
				ELSE 
					RETURN
				END
			ELSE
				RETURN
END
GO

-- Create One.
-- Returns created item id.
CREATE PROCEDURE [dbo].[sp_CreateUser]	
	@firstName		nvarchar(50)	= 'default_FirstName',
	@middleName		nvarchar(50)	= 'default_MiddleName',
	@lastName		nvarchar(50)	= 'default_LastName',	
	@theClassesId	int				= NULL,					-- Could be null, cuz it could be a teacher.
	@login			nvarchar(50)	= 'default_Login', -- Unique
	@password		nvarchar(50)	= 'default_Passsword',
	@phone			nvarchar(50)	= 'default_Phone', -- Unique
	@imagePath		varchar(255)	= NULL					-- Could be null.
AS
BEGIN
	SET NOCOUNT ON 

	Begin TRY
		-- Check if '@theClassesId' is correct.
		exec dbo.sp_CheckClassesIdIsCorrect @classesId=@theClassesId;
		-- Check if fields is Unique.
		exec dbo.sp_CheckIfFieldsIsUnique @checkLogin=@login, @checkPhone=@phone;

		INSERT INTO [dbo].[Users] 
			(							
				FirstName,
				MiddleName,
				LastName,
				TheClassesId,
				Login,
				Password,
				Phone,
				ImagePath
			)
			OUTPUT inserted.*
			VALUES 
			(							
				@firstName,
				@middleName,
				@lastName,
				@theClassesId,
				@login,
				@password,
				@phone,
				@imagePath
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
CREATE PROCEDURE [dbo].[sp_UpdateUser]
	@id				int				= NULL,
	@firstName		nvarchar(50)	= 'default_FirstName',
	@middleName		nvarchar(50)	= 'default_MiddleName',
	@lastName		nvarchar(50)	= 'default_LastName',	
	@theClassesId	int				= NULL,					-- Could be null, cuz it could be a teacher.
	@login			nvarchar(50)	= 'default_Login', -- Unique
	@password		nvarchar(50)	= 'default_Passsword',
	@phone			nvarchar(50)	= 'default_Phone', -- Unique
	@imagePath		varchar(255)	= NULL					-- Could be null.
AS
BEGIN	

	Begin TRY
		-- Check if '@theClassesId' is correct.
		exec dbo.sp_CheckClassesIdIsCorrect @classesId=@theClassesId;
		-- Check if fields is Unique.
		-- Check if User with '@id' exists.
		exec dbo.sp_CheckIfFieldsIsUnique @checkLogin=@login, @checkPhone=@phone, @checkId=@id;
		
		-- All is valid. Make update.
		BEGIN
		UPDATE dbo.Users
		SET 
			FirstName = @firstName,
			MiddleName = @middleName,
			LastName = @lastName,
			TheClassesId = @theClassesId,
			Login = @login,
			Password = @password,
			Phone = @phone,
			ImagePath = @imagePath
		WHERE dbo.Users.Id = @id
		SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Id = @id
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
CREATE PROCEDURE [dbo].[sp_DeleteUser]
	@id int = NULL
AS
BEGIN

	Begin TRY		
		-- Check if fields is Unique.
		-- Check if User with '@id' exists.
		exec dbo.sp_CheckIfFieldsIsUnique @checkId=@id;
		
		-- All is validated, make delete.
		DELETE FROM dbo.Users
		OUTPUT deleted.*
		WHERE dbo.Users.Id = @id
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

-- IsLoginExists
-- Raise an error if login doesn't exists.
-- Return User id with such login if exists.
CREATE PROCEDURE [dbo].[sp_IsLoginExistsForUser]
	@login			nvarchar(50)	= 'default_Login' -- Unique
AS
BEGIN
	IF NOT EXISTS (SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Login = @login)
		BEGIN
		DECLARE @MessageText_526 NVARCHAR(4000)	= N'No. Login does not exists for that Login = %s';
		-- RAISERROR with severity 11-19 will cause execution to 
		-- jump to the CATCH block.
		RAISERROR (@MessageText_526, 16, 1, @login);
		END
	ELSE
		SELECT TOP 1 dbo.Users.Id as Id FROM dbo.Users WHERE dbo.Users.Login = @login;
END
GO

-- GetUserByLoginAndPassword
CREATE PROCEDURE [dbo].[sp_GetUserByLoginAndPassword]
	@login			nvarchar(50)	= 'default_Login', -- Unique
	@password		nvarchar(50)	= 'default_Passsword'
AS
BEGIN
	SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Login = @login AND dbo.Users.Password = @password;
END
GO

-- GetUserByLogin
CREATE PROCEDURE [dbo].[sp_GetUserByLogin]
	@login			nvarchar(50)	= 'default_Login' -- Unique
AS
BEGIN
	SELECT TOP 1 * FROM dbo.Users WHERE dbo.Users.Login = @login;
END
GO

-- GetAllUsersByClassId	(p.s. like get students)
-- And make OrderBy.
CREATE PROCEDURE [dbo].[sp_GetAllUsersByClassId]
	@theClassesId	int	= NULL	-- Could be null, cuz it could be a teacher.
AS
BEGIN
	SELECT 
		Users.Id as Id, 
		Users.FirstName as FirstName,
		Users.MiddleName as MiddleName, 
		Users.LastName as LastName, 
		Users.TheClassesId as TheClassesId, 
		Users.Login as Login, 
		Users.Password as Password, 
		Users.Phone as Phone, 
		Users.ImagePath as ImagePath 
	FROM dbo.Users, dbo.TheClasses
	WHERE Users.TheClassesId = @theClassesId
	ORDER BY LastName ASC;
END
GO

-- GetUserByName
CREATE PROCEDURE [dbo].[sp_GetUserByName]
	@firstName		nvarchar(50)	= 'default_FirstName',
	@middleName		nvarchar(50)	= 'default_MiddleName',
	@lastName		nvarchar(50)	= 'default_LastName'	
AS
BEGIN
	SELECT TOP 1 * FROM dbo.Users 
	WHERE 
		dbo.Users.FirstName = @firstName AND
		dbo.Users.LastName = @lastName AND
		dbo.Users.MiddleName = @middleName;
END
GO

-- GetUserNameById 
-- Little remake it to return 'Login'. (cuz original function from backend SUCKS)
CREATE PROCEDURE [dbo].[sp_GetUserNameById]
	@id int = NULL
AS
BEGIN
	SELECT TOP 1 Login FROM dbo.Users WHERE Id = @id;
END
GO
