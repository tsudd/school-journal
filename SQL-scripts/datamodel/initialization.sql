USE Journal
GO

-- Insertng some classes
INSERT INTO TheClasses VALUES(1, 'A');
INSERT INTO TheClasses VALUES(1, 'B');
INSERT INTO TheClasses VALUES(2, 'A');
INSERT INTO TheClasses VALUES(2, 'B');
INSERT INTO TheClasses VALUES(3, 'A');
INSERT INTO TheClasses VALUES(3, 'B');
INSERT INTO TheClasses VALUES(4, 'A');
INSERT INTO TheClasses VALUES(4, 'B');
INSERT INTO TheClasses VALUES(5, 'B');
INSERT INTO TheClasses VALUES(5, 'A');
INSERT INTO TheClasses VALUES(6, 'B');
INSERT INTO TheClasses VALUES(6, 'A');
INSERT INTO TheClasses VALUES(7, 'B');
INSERT INTO TheClasses VALUES(7, 'A');
INSERT INTO TheClasses VALUES(8, 'B');
INSERT INTO TheClasses VALUES(8, 'A');
INSERT INTO TheClasses VALUES(9, 'B');
INSERT INTO TheClasses VALUES(9, 'A');
INSERT INTO TheClasses VALUES(10, 'C');
INSERT INTO TheClasses VALUES(10, 'B');
INSERT INTO TheClasses VALUES(10, 'A');
INSERT INTO TheClasses VALUES(11, 'C');
INSERT INTO TheClasses VALUES(11, 'B');
INSERT INTO TheClasses VALUES(11, 'A');


-- Inserting Some Subjects
INSERT INTO Subjects VALUES('English');
INSERT INTO Subjects VALUES('Russian');
INSERT INTO Subjects VALUES('French');
INSERT INTO Subjects VALUES('Math');
INSERT INTO Subjects VALUES('Chemistry');
INSERT INTO Subjects VALUES('Biology');
INSERT INTO Subjects VALUES('Physics');
INSERT INTO Subjects VALUES('History');
INSERT INTO Subjects VALUES('Georgraphy');
INSERT INTO Subjects VALUES('Data Science');
INSERT INTO Subjects VALUES('ASAP');
INSERT INTO Subjects VALUES('Programming');
INSERT INTO Subjects VALUES('High Math');
INSERT INTO Subjects VALUES('Belarussian');



-- Inserting some users
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Mark', 'Vasiliev', 'Fishman', 1, 'fishman', 'Strong123', '+375297823923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Vasya', 'Stanislavovich', 'Zuck', 2, 'vasyak', 'Strong123', '+375297748923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Sergey', 'Sergeevich', 'Cloyn', 4, 'sergich', 'Strong123', '+375295378923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Vadim', 'Vasiliev', 'Lokar', 5, 'vadvad', 'Strong123', '+375297772383');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Sasha', 'Vladislavovich', 'Mostov', 2, 'sanekk', 'Strong123', '+375297342393');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Vova', 'Markovich', 'Pipkin', 11, 'vovich', 'Strong123', '+375297774567');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Maksim', 'Aleksandrov', 'Hrukin', 9, 'petuh', 'Strong123', '+375297555923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Danila', 'Vadimovich', 'Lambrov', 20, 'gitik', 'Strong123', '+375297228923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Igor', 'Vadimovich', 'Helomov', 24, 'lomba', 'Strong123', '+375297235923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Slava', 'Sergeevich', 'Hahol', 24, 'qwerty123', 'Strong123', '+375297222923');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Aleksiy', 'Andreevich', 'Takirov', 24, 'someone', 'Strong123', '+375297378923');

-- Teachers
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Maria', 'Ivanovna', 'Buka', NULL, 'teacher1', 'Strong123', '+37529736543');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Maria', 'Vorobiova', 'Buka', NULL, 'teacher2', 'Strong123', '+375297334523');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Maria', 'Petrovna', 'Buka', NULL, 'teacher3', 'Strong123', '+375297372333');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Irina', 'Mihailovna', 'Buka', NULL, 'teacher4', 'Strong123', '+375297372343');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Milena', 'Genadievna', 'Rabchuk', NULL, 'teacher5', 'Strong123', '+375337372333');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Svetlana', 'Nikolaevna', 'Shuhar', NULL, 'teacher6', 'Strong123', '+375297552333');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Elena', 'Richardovna', 'Galka', NULL, 'teacher7', 'Strong123', '+375291234567');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Vadim', 'Kuku', 'Vladimcev', NULL, 'teacher8', 'Strong123', '+3753331335254');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Vladimir', 'Nikolaevich', 'Tesluk', NULL, 'teacher9', 'Strong123', '+375336666666');
INSERT INTO Users(FirstName, MiddleName, LastName, TheClassesId, Login, Password, Phone) VALUES('Anna', 'Borisovna', 'Taic', NULL, 'teacher10', 'Strong123', '+375339999999');


-- Inserting some teachers
INSERT INTO Teachers(UserId, SubjectId) VALUES(12, 6);
INSERT INTO Teachers(UserId, SubjectId) VALUES(13, 3);
INSERT INTO Teachers(UserId, SubjectId) VALUES(14, 5);
INSERT INTO Teachers(UserId, SubjectId) VALUES(15, 4);
INSERT INTO Teachers(UserId, SubjectId) VALUES(16, 2);
INSERT INTO Teachers(UserId, SubjectId) VALUES(17, 14);
INSERT INTO Teachers(UserId, SubjectId) VALUES(18, 8);
INSERT INTO Teachers(UserId, SubjectId) VALUES(19, 12);
INSERT INTO Teachers(UserId, SubjectId) VALUES(20, 13);
INSERT INTO Teachers VALUES(21, 1, 18);


-- inserting admins
INSERT INTO Admins VALUES(1);


-- inserting timetable

-- Monday
-- FOR 11 B
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(1, 1, 23, 10, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(2, 2, 23, 5, 1);
-- FOR 11 C
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(1, 12, 22, 8, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(2, 13, 22, 9, 1);

-- Tuesday
-- FOR 11 B
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(1, 12, 23, 8, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(2, 13, 23, 9, 1);
-- FOR 11 C
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Tuesday) VALUES(1, 1, 22, 10, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Tuesday) VALUES(2, 2, 22, 5, 1);

-- Wednesay
-- FOR 11 A
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Wednesday) VALUES(1, 6, 24, 1, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Wednesday) VALUES(2, 5, 24, 3, 1);

-- Thursday
-- FOR 11 A
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Thursday) VALUES(1, 8, 24, 7, 2);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Thursday) VALUES(2, 1, 24, 10, 2);

-- Friday
-- FOR 11 B
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(1, 4, 23, 4, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(2, 12, 23, 8, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(3, 13, 23, 9, 1);
-- FOR 11 C
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(1, 12, 22, 8, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(2, 13, 22, 9, 1);
INSERT INTO Timetables(LessonNumber, SubjectId, ClassId, TeacherId, Monday) VALUES(3, 4, 22, 4, 1);



-- Inserting some marks
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(9, 9, '2021-09-01');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(9, 10, '2021-09-02');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(9, 11, '2021-09-03');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(9, 12, '2021-09-04');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(10, 9, '2021-10-05');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(10, 10, '2021-10-08');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(10, 11, '2021-10-08');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(10, 12, '2021-10-08');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(11, 9, '2021-11-08');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(11, 10, '2021-11-10');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(11, 11, '2021-11-11');
INSERT INTO Marks(UserId, TimeTableId, Date) VALUES(11, 12, '2021-11-11');