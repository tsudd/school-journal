using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessSubjects : IDataAccess<Subjects>
    {
        public const string GET_ALL_PROC = "sp_GetAllSubjects";
        public const string GET_BY_ID_PROC = "sp_GetSubjectById";
        public const string GET_ALL_NAMES_PROC = "sp_GetAllSubjectNames";
        public const string GET_NAME_BY_ID_PROC = "sp_GetSubjectNameById";
        public const string GET_ID_BY_NAME_PROC = "sp_GetSubjectIdByName";
        public const string CREATE_PROC = "sp_CreateSubject";
        public const string UPDATE_PROC = "sp_UpdateSubject";
        public const string DELETE_PROC = "sp_DeleteSubject";

        public const string ID_ARGUMENT = "id";
        public const string NAME_ARGUMENT = "subjectName";

        Db Connection;

        public DataAccessSubjects()
        {
            Connection = Db.GetInstance();
        }

        public void Create(Subjects item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(NAME_ARGUMENT, item.SubjectName),
            };
            Connection.ExecuteCommand<Subjects>(CREATE_PROC, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id),
            };
            Connection.ExecuteCommand<Subjects>(DELETE_PROC, parameters, false);
        }

        public Subjects Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            var subjects = Connection.ExecuteCommand<Subjects>(GET_BY_ID_PROC, parameters);
            if (subjects.Count > 0)
            {
                return subjects[0];
            }
            return null;
        }

        public IEnumerable<Subjects> GetAll()
        {
            return Connection.ExecuteCommand<Subjects>(GET_ALL_PROC);
        }

        public void Update(Subjects item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, item.Id),
                new SqlParameter(NAME_ARGUMENT, item.SubjectName)
            };
            Connection.ExecuteCommand<Subjects>(UPDATE_PROC, parameters, false);
        }

        public string GetSubjectName(int id)
        {
            return Get(id)?.SubjectName;
        }

        public int GetSubjectIdByName(string subjectName)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(NAME_ARGUMENT, subjectName)
            };
            var subjects = Connection.ExecuteCommand<Subjects>(GET_ID_BY_NAME_PROC, parameters);
            if (subjects.Count > 0)
            {
                return subjects[0].Id;
            }
            return -1;
        }
    }
}
