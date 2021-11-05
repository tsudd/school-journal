using JournalForSchool.Login;
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
    public static class UsersInteraction
    {
        public static bool RegisterRequestStatus(User user)
        {
            var unitOfWork = UnitOfWork.GetInstance();
            
            if (unitOfWork.Users.IsLoginExist(user) == true) return false;

            unitOfWork.Users.Create(user);
            unitOfWork.Save();
            
            return true; 
        }

        public static void SetImagePath(string path, User user)
        {
            var unitOfWork = UnitOfWork.GetInstance();
            user.ImagePath = path;

            unitOfWork.Db.SaveChanges();
        }

        public static List<StackPanel> GetStackPanelList(int class_id)
        {
            List<StackPanel> list = new List<StackPanel>();
            var unitOfWork = UnitOfWork.GetInstance();
            
            List<User> AllUsersList = unitOfWork.Users.GetAllUsersByClassId(class_id);

            int idx_now = 0;
            foreach (var item in AllUsersList)
            {
                StackPanel stackPanel = new StackPanel();
                idx_now += 1;

                TextBlock idBlock = new TextBlock();
                idBlock.Width = 25;
                idBlock.Text = idx_now.ToString();

                TextBlock nameBlock = new TextBlock();
                nameBlock.Width = 75;
                nameBlock.Text = item.FirstName;

                TextBlock lastNameBlock = new TextBlock();
                lastNameBlock.Width = 100;
                lastNameBlock.Text = item.LastName;

                TextBlock middleNameBlock = new TextBlock();
                middleNameBlock.Width = 100;
                middleNameBlock.Text = item.MiddleName;

                stackPanel.Children.Add(idBlock);
                stackPanel.Children.Add(lastNameBlock);
                stackPanel.Children.Add(nameBlock);
                stackPanel.Children.Add(middleNameBlock);

                stackPanel.Orientation = Orientation.Horizontal;
                list.Add(stackPanel);
            }
            

            return list;
        }

       

    }
}
