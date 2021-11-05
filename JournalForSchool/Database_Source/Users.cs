using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JournalForSchool.Database_Source
{
    public static class Users
    {

        //public static bool RegisterRequestStatus(UserModel user)
        //{
        //    if (IsLoginExist(user) == true) return false;

        //    SqlConnection db = new SqlConnection(ConnectionSettings.GetMySqlConnectionSettings());
        //    db.Open();

        //    var command = new SqlCommand("INSERT into users (First_name, Middle_name, Last_name, Login, Password) " +
        //        "VALUES (@first_name, @Middle_name, @Last_name, @Login, @Password)", db);

        //    command.Parameters.AddWithValue("@first_name", user.First_name);
        //    command.Parameters.AddWithValue("@Middle_name", user.Middle_name);
        //    command.Parameters.AddWithValue("@Last_name", user.Last_name);
        //    command.Parameters.AddWithValue("@Login", user.Login);
        //    command.Parameters.AddWithValue("@Password", user.Password);

        //    command.ExecuteNonQuery();

        //    db.Close();

        //    return true;
        //}

        //public static bool IsLoginExist(UserModel user)
        //{



        //    return false;
        //}

        //public static UserModel GetUserModelByLogin(string login, string password)
        //{
        //    UserModel user = null;

        //    SqlConnection db = new SqlConnection(ConnectionSettings.GetMySqlConnectionSettings());
        //    db.Open();

        //    var command = new SqlCommand("SELECT * FROM Users WHERE " +
        //        "Login=@login and Password=@password", db);

        //    command.Parameters.AddWithValue("@login", login);
        //    command.Parameters.AddWithValue("@password", password);

        //    int The_class_id;

        //    using (SqlDataReader oReader = command.ExecuteReader())
        //    {
        //        while (oReader.Read())
        //        {
        //            user = new UserModel
        //            {
        //                First_name = (string)oReader["First_name"],
        //                Middle_name = (string)oReader["Middle_name"],
        //                Last_name = (string)oReader["Last_name"],

        //                Login = (string)oReader["Login"],
        //                Password = (string)oReader["Password"],

        //            };

        //            if (oReader["The_class_id"].ToString().Length != 0)
        //            {
        //                The_class_id = Int32.Parse((string)oReader["The_class_id"]);
        //                user.Class_id = The_class_id;
        //            }
        //            else user.Class_id = 0;
        //        }
        //    }

        //    db.Close();

        //    user.The_class = 11;
        //    user.Letter = 'А';

        //    return user;
        //}

        //public static List<UserModel> GetAllUsersByClassId(int class_id)
        //{
        //    List<UserModel> list = new List<UserModel>();

        //    SqlConnection db = new SqlConnection(ConnectionSettings.GetMySqlConnectionSettings());
        //    db.Open();

        //    var command = new SqlCommand("SELECT * FROM Users WHERE The_class_id=@class_id", db);

        //    command.Parameters.AddWithValue("@class_id", class_id);

        //    using (SqlDataReader oReader = command.ExecuteReader())
        //    {
        //        while (oReader.Read())
        //        {
        //            UserModel user = new UserModel
        //            {
        //                First_name = (string)oReader["First_name"],
        //                Middle_name = (string)oReader["Middle_name"],
        //                Last_name = (string)oReader["Last_name"],

        //                Login = (string)oReader["Login"],
        //                Password = (string)oReader["Password"],

        //            };

        //            if (oReader["The_class_id"].ToString().Length != 0)
        //            {
        //                user.Class_id = Int32.Parse(oReader["The_class_id"].ToString());
        //            }
        //            else user.Class_id = 0;

        //            list.Add(user);
        //        }
        //    }

        //    db.Close();

        //    return list;
        //}

        //public static UserModel GetUserByName(string first_name, string last_name, string middle_name)
        //{
        //    UserModel user = null;

        //    SqlConnection db = new SqlConnection(ConnectionSettings.GetMySqlConnectionSettings());
        //    db.Open();

        //    var command = new SqlCommand("SELECT * FROM Users WHERE " +
        //        "First_name=@first_name and Last_name=@last_name and Middle_name=@middle_name", db);

        //    command.Parameters.AddWithValue("@first_name", first_name);
        //    command.Parameters.AddWithValue("@last_name", last_name);
        //    command.Parameters.AddWithValue("@middle_name", middle_name);

        //    int The_class_id;

        //    using (SqlDataReader oReader = command.ExecuteReader())
        //    {
        //        while (oReader.Read())
        //        {
        //            user = new UserModel
        //            {
        //                First_name = (string)oReader["First_name"],
        //                Middle_name = (string)oReader["Middle_name"],
        //                Last_name = (string)oReader["Last_name"],

        //                Login = (string)oReader["Login"],
        //                Password = (string)oReader["Password"],

        //            };

        //            if (oReader["The_class_id"].ToString().Length != 0)
        //            {
        //                The_class_id = Int32.Parse((string)oReader["The_class_id"]);
        //                user.Class_id = The_class_id;
        //            }
        //            else user.Class_id = 0;
        //        }
        //    }

        //    db.Close();

        //    user.The_class = 11;
        //    user.Letter = 'А';

        //    return user;
        //}

        public static List<StackPanel> GetStackPanelList(int class_id)
        {
            List<StackPanel> list = new List<StackPanel>();
            //List<UserModel> AllUsersList = GetAllUsersByClassId(class_id);

            int idx_now = 0;
            //foreach (var item in AllUsersList)
            //{
            //    StackPanel stackPanel = new StackPanel();
            //    idx_now += 1;

            //    TextBlock idBlock = new TextBlock();
            //    idBlock.Width = 25;
            //    idBlock.Text = idx_now.ToString();

            //    TextBlock nameBlock = new TextBlock();
            //    nameBlock.Width = 75;
            //    nameBlock.Text = item.First_name;

            //    TextBlock lastNameBlock = new TextBlock();
            //    lastNameBlock.Width = 100;
            //    lastNameBlock.Text = item.Last_name;

            //    TextBlock middleNameBlock = new TextBlock();
            //    middleNameBlock.Width = 100;
            //    middleNameBlock.Text = item.Middle_name;

            //    stackPanel.Children.Add(idBlock);
            //    stackPanel.Children.Add(nameBlock);
            //    stackPanel.Children.Add(lastNameBlock);
            //    stackPanel.Children.Add(middleNameBlock);

            //    stackPanel.Orientation = Orientation.Horizontal;
            //    list.Add(stackPanel);
            //}


            return list;
        }

    }
}
