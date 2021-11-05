using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JournalForSchool
{
    public class Application 
    {
        public MainWindow mainWindow;
        UnitOfWork unitOfWork;
        public List<MyItem> UsersInClass { get; set; }

        public Application(int class_id, MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
            unitOfWork = UnitOfWork.GetInstance();
            List<User> listOfUsers = unitOfWork.Users.GetAllUsersByClassId(class_id);
            UsersInClass = new List<MyItem>();

            int idx_now = 1;
            foreach (var user in listOfUsers)
            {

                if (!TeachersInteraction.IsTeacher(user))
                {
                    UsersInClass.Add(new MyItem
                    {
                        User_name = $"{idx_now}. {user.LastName} {user.FirstName} {user.MiddleName}"
                    });
                    idx_now += 1;
                }
            }
        }


        public class MyItem
        { 
            public string User_name { get; set; }
      
            public List<string> Mark_list
            {
                get
                {
                    return new List<string> { "", "", "H", "10", "9", "8", "7", "6", "5", "4", "3", "2", "1" };
                }
            }
        }
    }
}
