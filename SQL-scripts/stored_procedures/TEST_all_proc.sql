USE [JournalForSchool]
GO

-- #1 ADMINS: *****************************************************************

-- 1) get All. +
-- exec sp_getAllAdmins;

-- 2) get One. +
-- exec sp_GetAdminById @id=6;

-- 3) create One. +-	*p.s. create is done but some error raises, don't know why :(
-- exec dbo.sp_CreateAdmin @id=12, @userId=1010;

-- 4) update One. +-	*p.s. update is done but some error raises, don't know why :(
-- exec sp_UpdateAdmin @id=6, @userId=1010;

-- 5) delete One. +-	*p.s. delete is done but some error raises, don't know why :(
-- exec sp_DeleteAdmin @id=1010;


-- #2 SUBJECTS: *****************************************************************

-- 1) get all. +
-- exec sp_GetAllSubjects;

-- 1.2) get all names. +
-- exec sp_GetAllSubjectNames;

-- 2) get One. +
-- exec sp_GetSubjectById @id=7;

-- 2.2) get One name. +
-- exec sp_GetSubjectNameById @id=7;

-- 2.3) get One id. +
-- exec sp_GetSubjectIdByName @subjectName='Биология';

-- 3) create One. +
-- exec sp_CreateSubject @subjectName='Мемология++';

-- 4) update One. +
-- exec sp_UpdateSubject @id=5, @subjectName='история_2'

-- 5) delete One. +
-- exec sp_DeleteSubject @id=10;

-- #3 TheClasses *****************************************************************

-- 1) get All. +
-- exec sp_GetAllClasses;

-- 2) get One. +
-- exec sp_GetClassById @id=9;

-- 3) create One. +
-- exec sp_CreateClass @theClass=9, @classLetter='Б';

-- 4) update One. +
-- exec sp_UpdateClass @id=6, @theClass=10, @classLetter='В';

-- 4) delete One. +
-- exec sp_DeleteClass @id=10;

-- 5) GetTheClassesLetters. +
-- exec sp_GetTheClassesLetters @theClass=11;

-- 6) GetTheClassByNumber. +
-- exec sp_GetTheClassByNumber @theClass=11, @classLetter='В';

-- #4 Teachers ***************************************************************** (Представим что учитель может иметь несколько предметов, хотя хз норм ли это с такой структурой таблицы)

-- 1) get All. +
-- exec sp_GetAllTeachers;

-- 2) get One. +
-- exec sp_GetTeacherById @id=3;

-- 3) create One. +
-- exec sp_CreateTeacher @userId=4, @subjectId=5;

-- 4) update One. +
-- exec sp_UpdateTeacher @id=1, @subjectId=5, @userId=1010

-- 5) delete One. +
-- exec sp_DeleteTeacher @id=12;

-- 6) GetAllTeachersNames. +
-- exec sp_GetAllTeachersNames;

-- #5 Users *****************************************************************

-- 1) get All. +
-- exec sp_GetAllUsers;

-- 2) get One. +
-- exec sp_GetUserById @id=3;

-- 3) create One. +
-- exec sp_CreateUser @login='some_login_2', @phone='some_phone_2';

-- 4) update One. +
-- exec sp_UpdateUser @id=6, @firstName='Дмитрий_1', @login='dima_1', @phone='phone_1';

-- 5) delete One. +
-- exec sp_DeleteUser @id=1023;

-- 6) isLoginExists. +
--exec sp_IsLoginExistsForUser @login='dima_1';

-- 7) GetUserByLoginAndPassword. +
-- exec sp_GetUserByLoginAndPassword @login='some_login_1', @password='default_Passsword';

-- 8) GetUserByLogin. +
-- exec sp_GetUserByLogin @login='dima_1';

-- 9) GetAllUsersByClassId. +
-- exec sp_GetAllUsersByClassId @theClassesId=1;

-- 10) GetUserByName. +
-- exec sp_GetUserByName @firstName='default_FirstName', @middleName='default_MiddleName', @lastName='default_LastName';

-- 11) GetUserNameById. +
-- exec sp_GetUserNameById @id=6;


-- #6 Marks *****************************************************************

-- 1) get All. +
-- exec sp_GetAllMarks;

-- 2) get One. +
-- exec sp_GetMarkById @id=7;

-- 3) create One. +
-- exec sp_CreateMark @userId=2, @timeTableId=3, @selectedIndex=3, @date='Test_date_2';

-- 4) update One. +
-- exec sp_UpdateMark @id=1690, @userId=2, @timeTableId=3, @selectedIndex=5, @date='Test_date_1';

-- 5) delete One. +
-- exec sp_DeleteMark @id=1690;

-- 6) GetMarkSelectedIndex. + 
-- exec sp_GetMarkSelectedIndex @userId=2, @timeTableId=3, @date='Test_date_2';

-- 7) DeleteIfExist_MarksProc. +
-- exec sp_DeleteIfExist_MarksProc @userId=2, @timeTableId=3, @date='Test_date_2';

-- #6 Timetables *****************************************************************

-- 1) get All. +
-- exec sp_GetAllTimetables;

-- 2) get One. +
-- exec sp_GetTimetableById @id=7;

-- 3) create One. +
-- exec sp_CreateTimetable @lessonNumber=1, @subjectId=1, @classId=1, @teacherId=1, @monday=1, @tuesday=1, @wednesday=1, @thursday=1, @friday=1; 

-- 4) update One. +
-- exec sp_UpdateTimetable @id=15, @lessonNumber=1, @subjectId=1, @classId=1, @teacherId=1, @monday=1, @tuesday=1, @wednesday=1, @thursday=1, @friday=0; 

-- 5) delete One. +
-- exec sp_DeleteTimetable @id=7;
