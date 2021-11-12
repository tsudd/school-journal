USE JournalForSchool
GO

-- 1) GetTheDisctinctClassesNames.
CREATE PROCEDURE dbo.sp_GetTheDisctinctClassesNames
AS
BEGIN
	SELECT DISTINCT TheClasses.TheClass 
		FROM dbo.TheClasses 
		order by TheClass DESC;
END
GO

-- 2) GetAdminByUserId.
CREATE PROCEDURE dbo.sp_GetAdminByUserId
	@userId	int = NULL
AS
BEGIN
	SELECT TOP 1 Admins.* 
		FROM Admins 		
		WHERE Admins.UserId = @userId;
END
GO

-- 3) GetTeacherByUserId.
CREATE PROCEDURE dbo.sp_GetTeacherByUserId
	@userId	int = NULL
AS
BEGIN
	SELECT TOP 1 Teachers.* 
		FROM Teachers	
		WHERE Teachers.UserId = @userId;
END
GO

-- 4) GetTimetableForUser.
CREATE PROCEDURE dbo.sp_GetTimetableForUser
	@day		nvarchar(50)	= 'Monday',
	@classId	int				= NULL,
	@lessonNumber int			= NULL
AS
BEGIN
	IF @day = 'Monday'
		SELECT TOP 1 Timetables.* FROM Timetables where ClassId = @classId and LessonNumber = @lessonNumber and Monday = 1;
	ELSE
		IF @day = 'Tuesday'
			SELECT TOP 1 Timetables.* FROM Timetables where ClassId = @classId and LessonNumber = @lessonNumber and Tuesday = 1;
		ELSE
			IF @day = 'Wednesday'
				SELECT TOP 1 Timetables.* FROM Timetables where ClassId = @classId and LessonNumber = @lessonNumber and Wednesday = 1;
			ELSE
				IF @day = 'Thursday'
					SELECT TOP 1 Timetables.* FROM Timetables where ClassId = @classId and LessonNumber = @lessonNumber and Thursday = 1;
				ELSE
					IF @day = 'Friday'
						SELECT TOP 1 Timetables.* FROM Timetables where ClassId = @classId and LessonNumber = @lessonNumber and Friday = 1;	
					ELSE 
						BEGIN						
						DECLARE @MessageText_AdditionalProc_545 NVARCHAR(4000)	= N'Fail. GetTimetableForUser , cuz @day = %s invalid.';
						-- RAISERROR with severity 11-19 will cause execution to 
						-- jump to the CATCH block.
						RAISERROR (@MessageText_AdditionalProc_545, 16, 1, @day);			
						END
END
GO

-- 5) GetAllPupilsAndUpdate.
CREATE PROCEDURE dbo.sp_GetAllPupilsAndUpdate	
AS
BEGIN
	SELECT * FROM Users WHERE Users.TheClassesId IS NOT NULL;
END
GO

-- 6) GetAllTeachersAndUpdate.
-- юзать sp_GetAllTeachers !