using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessTeachers : IDataAccess<Teacher>
    {


        Db Connection;
        public const string CREATE_PROC = "sp_CreateTeacher";
        public const string UPDATE_PROC = "sp_UpdateTeacher";
        public const string DELETE_PROC = "sp_DeleteTeacher";
        public const string GET_ALL_PROC = "sp_GetAllTeachers";
        public const string GET_BY_ID_PROC = "sp_GetTeacherById";
        public const string GET_TEACHER_BY_USER_ID_PROC = "sp_GetTeacherByUserId";

        public const string ID_ARGUMENT = "id";
        public const string USER_ID_ARGUMENT = "userId";
        public const string SUBJECT_ID_ARGUMENT = "subjectId";
        public const string THE_CLASS_MENTOR_ID_ARGUMENT = "theClassMentorId";

        public DataAccessTeachers()
        {
            Connection = Db.GetInstance();
        }

        public void Create(Teacher item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USER_ID_ARGUMENT, item.UserId),
                new SqlParameter(SUBJECT_ID_ARGUMENT, item.SubjectId),
                new SqlParameter(THE_CLASS_MENTOR_ID_ARGUMENT, null)
            };
            Connection.ExecuteCommand<Teacher>(CREATE_PROC, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            Connection.ExecuteCommand<Admin>(DELETE_PROC, parameters, false);
        }

        public Teacher Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            try
            {
                var result = Connection.ExecuteCommand<Teacher>(GET_BY_ID_PROC, parameters, true);
                return result[0];
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Teacher> GetAll()
        {
            return Connection.ExecuteCommand<Teacher>(GET_ALL_PROC, null, true);
        }

        public void Update(Teacher item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, item.Id),
                new SqlParameter(USER_ID_ARGUMENT, item.UserId),
                new SqlParameter(SUBJECT_ID_ARGUMENT, item.SubjectId),
                new SqlParameter(THE_CLASS_MENTOR_ID_ARGUMENT, null)
            };
            Connection.ExecuteCommand<Teacher>(UPDATE_PROC, parameters, false);
        }

        public List<string> GetAllTeachersNames()
        {
            var users = new DataAccessUsers();
            List<string> response = new List<string>();
            foreach(var teacher in GetAll())
            {
                var user = users.Get(teacher.UserId);
                response.Add(user.LastName + " " + user.FirstName + " " + user.MiddleName);
            }
            return response;
        }

        public Teacher GetTeacherByUserId(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USER_ID_ARGUMENT, id)
            };
            var teachers = Connection.ExecuteCommand<Teacher>(GET_TEACHER_BY_USER_ID_PROC, parameters);
            if (teachers.Count > 0)
            {
                return teachers[0];
            }
            return null;
        }
        
    }
}
