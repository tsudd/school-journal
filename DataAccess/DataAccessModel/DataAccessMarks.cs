using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessMarks : IDataAccess<Mark>
    {
        public const string GET_ALL_PROC = "sp_GetAllMarks";
        public const string GET_BY_ID_PROC = "sp_GetMarkById";
        public const string CHECK_MARK_DEPS_PROC = "sp_CheckTimetableIdAndUserIdAreCorrect_MarksProc";
        public const string CHECK_MARK_FIELDS_PROC = "sp_CheckIfFieldsAreCorrect_MarksProc";
        public const string CREATE_PROC = "sp_CreateMark";
        public const string UPDATE_PROC = "sp_UpdateMark";
        public const string DELETE_PROC = "sp_DeleteMark";
        public const string GET_SELECTED_ID_PROC = "sp_GetMarkSelectedIndex";
        public const string TRY_DELETE_PROC = "sp_DeleteIfExist_MarksProc";

        public const string ID_ARGUMENT = "id";
        public const string USERID_ARGUMENT = "userId";
        public const string DATE_ARGUMENT = "date";
        public const string SELECTID_ARGUMENT = "selectedIndex";
        public const string TIMETABLEID_ARGUMENT = "timetableId";
        public const string LETTER_ARGUMENT = "classLetter";

        Db Connection;

        public DataAccessMarks()
        {
            Connection = Db.GetInstance();
        }

        public IEnumerable<Mark> GetAll()
        {
            return Connection.ExecuteCommand<Mark>(GET_ALL_PROC);
        }

        public Mark Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            var marks = Connection.ExecuteCommand<Mark>(GET_BY_ID_PROC, parameters);
            if (marks.Count > 0)
            {
                return marks[0];
            }
            return null;
        }

        public void Create(Mark item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USERID_ARGUMENT, item.UserId),
                new SqlParameter(DATE_ARGUMENT, item.Date),
                new SqlParameter(SELECTID_ARGUMENT, item.SelectedIndex),
                new SqlParameter(TIMETABLEID_ARGUMENT, item.TimeTableId)

            };
            Connection.ExecuteCommand<Mark>(CREATE_PROC, parameters, false);
        }

        public void Update(Mark item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USERID_ARGUMENT, item.UserId),
                new SqlParameter(DATE_ARGUMENT, item.Date),
                new SqlParameter(SELECTID_ARGUMENT, item.SelectedIndex),
                new SqlParameter(TIMETABLEID_ARGUMENT, item.TimeTableId)

            };
            Connection.ExecuteCommand<Mark>(UPDATE_PROC, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            Connection.ExecuteCommand<Mark>(DELETE_PROC, parameters, false);
        }

        public int GetMarkSelectedIndex(int userId, int timeTableId, string date)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USERID_ARGUMENT, userId),
                new SqlParameter(TIMETABLEID_ARGUMENT, timeTableId),
                new SqlParameter(DATE_ARGUMENT, date)
            };

            var marks = Connection.ExecuteCommand<Mark>(GET_SELECTED_ID_PROC, parameters);
            if (marks.Count > 0)
            {
                return marks[0].SelectedIndex;
            }
            return -1;
        }

        public void DeleteIfExists(int userId, int timeTableId, string date)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USERID_ARGUMENT, userId),
                new SqlParameter(TIMETABLEID_ARGUMENT, timeTableId),
                new SqlParameter(DATE_ARGUMENT, date)
            };
            Connection.ExecuteCommand<Mark>(TRY_DELETE_PROC, parameters, false);
        }
    }
}
