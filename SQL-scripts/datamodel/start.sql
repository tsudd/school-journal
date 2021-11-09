USE master
IF DB_ID (N'JournaForSchool') IS NOT NULL 
BEGIN
  ALTER DATABASE JournaForSchool SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE JournaForSchool;
END;
GO

CREATE DATEBASE JournaForSchool
GO
USE JournaForSchool
GO


CREATE TABLE TheClasses(
    Id INT IDENTITY(1, 1) NOT NULL,
    TheClass INT NOT NULL,
    ClassLetter NCHAR(1) NOT NULL,
    CONSTRAINT PK_TheClasses PRIMARY KEY CLUSTERED(Id ASC)
);


CREATE TABLE Users(
    Id INT IDENTITY(1, 1) NOT NULL,
    FirstName NVARCHAR(30) NOT NULL,
    MiddleName NVARCHAR(30) NOT NULL,
    LastName NVARCHAR(30) NOT NULL,
    TheClassesId INT NOT NULL,
    Login NVARCHAR(30) UNIQUE NOT NULL,
    Password NVARCHAR(30) NOT NULL,

    -- там стояло по дефолту 0 оставляем?
    Phone NVARCHAR(30) DEFAULT('0') NOT NULL,
    ImagePath VARCHAR(255) NULL,

    FOREIGN KEY(TheClassesId) REFERENCES TheClasses(Id) ON DELETE SET NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED(Id ASC)
)


CREATE TABLE Admins(
    Id INT IDENTITY(1, 1) NOT NULL,
    UserId INT NOT NULL UNIQUE,
    -- foreigh key for userid from users??? and cascade delete if delete ur serlve or what? then delete unique
    CONSTRAINT PK_Admins PRIMARY KEY CLUSTERED(Id ASC)
);



CREATE TABLE Marks(
    Id INT IDENTITY(1, 1) NOT NULL,
    UserId INT NOT NULL,
    -- откуда беретется timetableid по данным это не тоже самое что и timetableid в таблицах есть разные
    TimeTableId INT NOT NULL,
    SelectedIndex INT NOT NULL,
    -- какую дату лучше ставить? стринг или такую
    Date DATE NOT NULL,
    -- Cascade delete? лучше наверно да
    FOREIGN KEY(UserId) REFERENCES Users(Id) ON DELETE SET NULL,
    CONSTRAINT PK_Marks PRIMARY KEY CLUSTERED(Id ASC)
);

CREATE TABLE Subjects(
    Id INT NOT NULL,
    SubjectName NVARCHAR(50) UNIQUE NOT NULL,
    CONSTRAINT PK_Subjects PRIMARY KEY CLUSTERED(Id ASC)
);


CREATE TABLE Teachers(
    Id INT IDENTITY(1, 1) NOT NULL,
    UserId INT NOT NULL,
    SubjectId INT NOT NULL,
    -- в коде нигде нет класс ментор айди
    TheClassMentorId INT NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    -- могут быть проблемы если удалим предмет и учитель останется без предмета
    FOREIGN KEY(SubjectId) REFERENCES Subjects(Id) ON DELETE SET NULL,
    CONSTRAINT PK_Teachers PRIMARY KEY CLUSTERED(Id ASC)
);





CREATE TABLE Timetables(
    Id INT IDENTITY(1, 1) NOT NULL,
    LessonNumber INT NOT NULL,
    SubjectId INT NOT NULL,
    ClassId INT NOT NULL,
    TeacherID INT DEFAULT(1) NOT NULL,
    Monday BIT DEFAULT(0) NOT NULL,
    Tuesday BIT DEFAULT(0) NOT NULL,
    Wednesday BIT DEFAULT(0) NOT NULL,
    Thursday BIT DEFAULT(0) NOT NULL,
    Friday  BIT DEFAULT(0) NOT NULL,
    FOREIGN KEY(SubjectId) REFERENCES Subjects(Id) ON DELETE CASCADE,
    FOREIGN KEY(ClassId) REFERENCES TheClasses(Id) ON DELETE CASCADE,
    FOREIGN KEY(TeacherId) REFERENCES Teachers(Id) ON DELETE CASCADE,

    CONSTRAINT PK_Timetable PRIMARY KEY CLUSTERED(Id ASC)
);
