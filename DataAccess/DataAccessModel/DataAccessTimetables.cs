using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessTimetables : IDataAccess<Timetable>
    {
        Db Connection;

        public const string CREATE_PROC = "sp_CreateTimetable";
        public const string UPDATE_PROC = "sp_UpdateTimetable";
        public const string DELETE_PROC = "sp_DeleteTimetable";
        public const string GET_ALL_PROC = "sp_GetAllTimetables";
        public const string GET_BY_ID_PROC = "sp_GetTimetableById";
        public const string GET_TIMETABLE_FOR_USER_PROC = "sp_GetTimetableForUser";
        public const string GET_TIMETABLE_FOR_TEACHER_PROC = "sp_GetTimetableForTeacher";
        public const string GET_TIMETABLE_MODEL_PROC = "sp_GetTimeTableModel";

        public const string ID_ARGUMENT = "id";
        public const string LESSON_NUMBER_ARGUMENT = "lessonNumber";
        public const string SUBJECT_ID_ARGUMENT = "subjectId";
        public const string CLASS_ID_ARGUMENT = "classId";
        public const string TEACHER_ID_ARGUMENT = "teacherId";
        public const string MONDAY_ARGUMENT = "monday";
        public const string TUESDAY_ARGUMENT = "tuesday";
        public const string WEDNESDAY_ARGUMENT = "wednesday";
        public const string THURSDAY_ARGUMENT = "thursday";
        public const string FRIDAY_ARGUMENT = "friday";
        public const string DAY_ARGUMENT = "day";

        public DataAccessTimetables()
        {
            Connection = Db.GetInstance();
        }

        public void Create(Timetable item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(LESSON_NUMBER_ARGUMENT, item.LessonNumber),
                new SqlParameter(SUBJECT_ID_ARGUMENT, item.SubjectId),
                new SqlParameter(CLASS_ID_ARGUMENT, item.ClassId),
                new SqlParameter(TEACHER_ID_ARGUMENT, item.TeacherId),
                new SqlParameter(MONDAY_ARGUMENT, item.Monday),
                new SqlParameter(TUESDAY_ARGUMENT, item.Tuesday),
                new SqlParameter(WEDNESDAY_ARGUMENT, item.Wednesday),
                new SqlParameter(THURSDAY_ARGUMENT, item.Thursday),
                new SqlParameter(FRIDAY_ARGUMENT, item.Friday)
            };
            Connection.ExecuteCommand<Admin>(CREATE_PROC, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            Connection.ExecuteCommand<Admin>(DELETE_PROC, parameters, false);
        }

        public Timetable Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            try
            {
                var result = Connection.ExecuteCommand<Timetable>(GET_BY_ID_PROC, parameters, true);
                return result[0];
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Timetable> GetAll()
        {
            return Connection.ExecuteCommand<Timetable>(GET_ALL_PROC, null, true);
        }

        public void Update(Timetable item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, item.Id),
                new SqlParameter(LESSON_NUMBER_ARGUMENT, item.LessonNumber),
                new SqlParameter(SUBJECT_ID_ARGUMENT, item.SubjectId),
                new SqlParameter(CLASS_ID_ARGUMENT, item.ClassId),
                new SqlParameter(TEACHER_ID_ARGUMENT, item.TeacherId),
                new SqlParameter(MONDAY_ARGUMENT, item.Monday),
                new SqlParameter(TUESDAY_ARGUMENT, item.Tuesday),
                new SqlParameter(WEDNESDAY_ARGUMENT, item.Wednesday),
                new SqlParameter(THURSDAY_ARGUMENT, item.Thursday),
                new SqlParameter(FRIDAY_ARGUMENT, item.Friday)
            };
            Connection.ExecuteCommand<Admin>(UPDATE_PROC, parameters, false);
        }

        public Timetable GetTimetableForUser(string day, int classId, int lessonNum)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(DAY_ARGUMENT, day),
                new SqlParameter(CLASS_ID_ARGUMENT, classId),
                new SqlParameter(LESSON_NUMBER_ARGUMENT, lessonNum)
            };
            var timetables = Connection.ExecuteCommand<Timetable>(GET_TIMETABLE_FOR_USER_PROC, parameters);
            if (timetables.Count > 0)
            {
                return timetables[0];
            }
            return null;
        }

        public Timetable GetTimetableForTeacher(string day, int teacherId, int lessonNum)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(DAY_ARGUMENT, day),
                new SqlParameter(TEACHER_ID_ARGUMENT, teacherId),
                new SqlParameter(LESSON_NUMBER_ARGUMENT, lessonNum)
            };
            var timetables = Connection.ExecuteCommand<Timetable>(GET_TIMETABLE_FOR_TEACHER_PROC, parameters);
            if (timetables.Count > 0)
            {
                return timetables[0];
            }
            return null;
        }

        public Timetable GetTimetableModel(int subjectId, int lessonNum, int classId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(SUBJECT_ID_ARGUMENT, subjectId),
                new SqlParameter(CLASS_ID_ARGUMENT, classId),
                new SqlParameter(LESSON_NUMBER_ARGUMENT, lessonNum)
            };
            var timetables = Connection.ExecuteCommand<Timetable>(GET_TIMETABLE_MODEL_PROC, parameters);
            if (timetables.Count > 0)
            {
                return timetables[0];
            }
            return null;
        }
    }
}
