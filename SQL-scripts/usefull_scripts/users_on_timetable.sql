-- show users that were at specific timetable

SELECT TimeTables.Id as TimeTableID, TheClasses.Id as classid, Users.Firstname, Users.Lastname, Users.Id as UserID
FROM TimeTables
LEFT JOIN theclasses
ON TimeTables.Classid = TheClasses.Id
JOIN Users
ON Users.TheClassesId = TheClasses.Id