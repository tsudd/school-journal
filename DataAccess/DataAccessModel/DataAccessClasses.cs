using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessClasses : IDataAccess<TheClasses>
    {
        public const string GET_ALL_PROC = "sp_GetAllClasses";
        public const string GET_BY_ID_PROC = "sp_GetClassById";
        public const string CREATE_PROC = "sp_CreateClass";
        public const string UPDATE_PROC = "sp_UpdateClass";
        public const string DELETE_PROC = "sp_DeleteClass";
        public const string GET_LETTERS_PROC = "sp_GetTheClassesLetters";
        public const string GET_CLASS_BY_NUMBER = "sp_GetTheClassByNumber";

        public const string ID_ARGUMENT = "id";
        public const string USERID_ARGUMENT = "userId";
        public const string CLASS_AGRUMENT = "theClass";
        public const string LETTER_ARGUMENT = "classLetter";

        Db Connection;

        public DataAccessClasses()
        {
            Connection = Db.GetInstance();
        }

        public void Create(TheClasses item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TheClasses Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            var classes = Connection.ExecuteCommand<TheClasses>(GET_BY_ID_PROC, parameters);
            if (classes.Count > 0)
            {
                return classes[0];
            }
            return null;
        }

        public IEnumerable<TheClasses> GetAll()
        {
            return Connection.ExecuteCommand<TheClasses>(GET_ALL_PROC);
        }

        public void Update(TheClasses item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(CLASS_AGRUMENT, item.TheClass),
                new SqlParameter(LETTER_ARGUMENT, item.ClassLetter)
            };
            Connection.ExecuteCommand<TheClasses>(UPDATE_PROC, parameters, false);
        }

        public List<string> GetClassesLetters()
        {
            var classes = Connection.ExecuteCommand<TheClasses>(GET_LETTERS_PROC);
            var letters = new List<string>();
            foreach(var one in classes)
            {
                letters.Add(one.ClassLetter);
            }
            return letters;
        }

        public TheClasses GetClassByNumber(TheClasses item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(CLASS_AGRUMENT, item.TheClass),
                new SqlParameter(LETTER_ARGUMENT, item.ClassLetter)
            };
            var classes = Connection.ExecuteCommand<TheClasses>(GET_CLASS_BY_NUMBER, parameters);
            if (classes.Count > 0)
            {
                return classes[0];
            }
            return null;
        }

        //TODO: CLass by full name?
    }
}
