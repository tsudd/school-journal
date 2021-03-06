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
        public const string GET_CLASSES_NAMES_PROC = "sp_GetTheDisctinctClassesNames";

        public const string ID_ARGUMENT = "id";
        public const string USERID_ARGUMENT = "userId";
        public const string CLASS_ARGUMENT = "theClass";
        public const string CHECKTABLE_ARGUMENT = "checkTimetableId";
        public const string LETTER_ARGUMENT = "classLetter";

        Db Connection;

        public DataAccessClasses()
        {
            Connection = Db.GetInstance();
        }

        public void Create(TheClasses item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(CLASS_ARGUMENT, item.TheClass),
                new SqlParameter(LETTER_ARGUMENT, item.ClassLetter)
            };
            Connection.ExecuteCommand<Mark>(CREATE_PROC, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            Connection.ExecuteCommand<Admin>(DELETE_PROC, parameters, false);
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
                new SqlParameter(CHECKTABLE_ARGUMENT, item.TheClass),
                new SqlParameter(LETTER_ARGUMENT, item.ClassLetter)
            };
            Connection.ExecuteCommand<TheClasses>(UPDATE_PROC, parameters, false);
        }

        public List<string> GetClassesLetters(int num)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(CLASS_ARGUMENT, num)
            };
            var classes = Connection.ExecuteCommand<TheClasses>(GET_LETTERS_PROC, parameters);
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
                new SqlParameter(CLASS_ARGUMENT, item.TheClass),
                new SqlParameter(LETTER_ARGUMENT, item.ClassLetter)
            };
            var classes = Connection.ExecuteCommand<TheClasses>(GET_CLASS_BY_NUMBER, parameters);
            if (classes.Count > 0)
            {
                return classes[0];
            }
            return null;
        }

        public List<int> GetDistinctClassesNames()
        {
            var classes = Connection.ExecuteCommand<TheClasses>(GET_CLASSES_NAMES_PROC);
            var names = new List<int>();
            foreach (var one in classes)
            {
                names.Add(one.TheClass);
            }
            return names;
        }

        //TODO: CLass by full name?
    }
}
