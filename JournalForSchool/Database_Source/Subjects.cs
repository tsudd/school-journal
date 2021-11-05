using JournalForSchool.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Database_Source
{
    public static class Subjects
    {

        public static List<SubjectModel> GetSubjectsList()
        {
            List<SubjectModel> list = new List<SubjectModel>();

            SqlConnection db = new SqlConnection(ConnectionSettings.GetMySqlConnectionSettings());
            db.Open();

            var command = new SqlCommand("SELECT * FROM Subjects", db);

            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {
                    SubjectModel item = new SubjectModel
                    {
                        subject_name = (string)oReader["Subject_name"]
                    };

                    list.Add(item);
                }
            }

            db.Close();
            return list;
        }

        public static string GetSubjectName(int subject_id)
        {
            string subject_name = "";

            SqlConnection db = new SqlConnection(ConnectionSettings.GetMySqlConnectionSettings());
            db.Open();

            var command = new SqlCommand("SELECT * FROM Subjects WHERE Id=@id", db);
            command.Parameters.AddWithValue("@id", subject_id);

            using (SqlDataReader oReader = command.ExecuteReader())
            {
                while (oReader.Read())
                {
                    subject_name = (string)oReader["Subject_name"];
                }
            }

            db.Close();
            return subject_name;
        }
    }
}
