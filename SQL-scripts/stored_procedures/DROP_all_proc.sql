-- QUERY to drop all procedures.

USE JournalForSchool
GO

-- drop Admins procedures.
DROP PROCEDURE 
	dbo.sp_GetAllAdmins, 
	dbo.sp_GetAdminById, 
	dbo.sp_CreateAdmin, 
	dbo.sp_UpdateAdmin, 
	dbo.sp_DeleteAdmin;
GO

-- drop Subjects procedures.
DROP PROCEDURE 
	dbo.sp_GetAllSubjects, 
	dbo.sp_GetAllSubjectNames, 
	dbo.sp_GetSubjectById, 
	dbo.sp_GetSubjectNameById, 
	sp_GetSubjectIdByName, 
	dbo.sp_CreateSubject, 
	dbo.sp_UpdateSubject, 
	dbo.sp_DeleteSubject,
	dbo.sp_GetTheClassesLetters,
	dbo.sp_GetTheClassByNumber;
GO

-- drop TheClasses procedures.
DROP PROCEDURE
	dbo.sp_GetAllClasses,
	dbo.sp_GetClassById,
	dbo.sp_CreateClass,
	dbo.sp_UpdateClass,
	dbo.sp_DeleteClass,
	dbo.sp_GetTheClassesLetters,
	dbo.sp_GetTheClassByNumber;
GO

-- drop Teachers procedures.
DROP PROCEDURE
	dbo.sp_GetAllTeachers,
	dbo.sp_GetTeacherById,
	dbo.sp_CreateTeacher,
	dbo.sp_UpdateTeacher,
	dbo.sp_DeleteTeacher,
	dbo.sp_GetAllTeachersNames;
GO

-- drop Users procedures.
DROP PROCEDURE
	dbo.sp_GetAllUsers,
	dbo.sp_GetUserById,
	dbo.sp_CreateUser,
	dbo.sp_UpdateUser,
	dbo.sp_DeleteUser,
	dbo.sp_CheckIfFieldsIsUnique,
	dbo.sp_CheckClassesIdIsCorrect,
	dbo.sp_IsLoginExistsForUser,
	dbo.sp_GetUserByLoginAndPassword,
	dbo.sp_GetUserByLogin,
	dbo.sp_GetAllUsersByClassId,
	dbo.sp_GetUserByName,
	dbo.sp_GetUserNameById;
GO	

-- drop Marks procedures.
DROP PROCEDURE
	dbo.sp_GetAllMarks,
	dbo.sp_GetMarkById,
	dbo.sp_CreateMark,
	dbo.sp_UpdateMark,
	dbo.sp_DeleteMark,
	dbo.sp_GetMarkSelectedIndex,
	dbo.sp_DeleteIfExist_MarksProc,
	dbo.sp_CheckTimetableIdAndUserIdAreCorrect_MarksProc,
	dbo.sp_CheckIfFieldsAreCorrect_MarksProc;	
GO

-- drop Timetables procedures.
DROP PROCEDURE
	dbo.sp_GetAllTimetables,
	dbo.sp_GetTimetableById,
	dbo.sp_CreateTimetable,
	dbo.sp_UpdateTimetable,
	dbo.sp_DeleteTimetable,
	dbo.sp_CheckIfFieldsAreCorrect_TimetablesProc,
	dbo.sp_CheckForeignRefsAreCorrect_TimetablesProc;	
GO
