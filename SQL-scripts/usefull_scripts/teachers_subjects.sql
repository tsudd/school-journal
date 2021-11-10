-- show all teachers and theirs subjects

SELECT Users.id as userId, Users.FirstName as name, Users.MiddleName as middlename, Users.LastName as surname, Teachers.Id as TeacherId, Subjects.SubjectName as SubjectName, Subjects.Id as SubjectId
FROM Teachers
JOIN Subjects
ON Teachers.SubjectId = Subjects.Id
JOIN Users
ON Users.Id = Teachers.UserId;