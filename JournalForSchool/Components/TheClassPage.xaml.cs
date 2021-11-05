using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JournalForSchool
{
    /// <summary>
    /// Логика взаимодействия для TheClassPage.xaml
    /// </summary>
    public partial class TheClassPage : Page
    {
        private MainWindow mainWindow;
        private User user;
        UnitOfWork unitOfWork;

        public TheClassPage(MainWindow _mainWindow, User _user)
        {
            InitializeComponent();
            unitOfWork = UnitOfWork.GetInstance();

            mainWindow = _mainWindow;
            user = _user;
            TheClasses Class = unitOfWork.TheClasses.GetTheClassById(user.TheClassesId.Value);

            Class_label.Content = Class.TheClass.ToString() + " " + Class.ClassLetter;
            UpdateStackPanel();
        }

        private void UpdateStackPanel()
        {
            while (AllUsers.Items.Count > 1) AllUsers.Items.RemoveAt(1);

            List<StackPanel> list = UsersInteraction.GetStackPanelList(user.TheClassesId.Value);

            foreach (var item in list)
            {
                AllUsers.Items.Add(item);
            }
        }

        private void Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel seletedItem;
            seletedItem = (StackPanel)e.AddedItems[0];

            string last_name = ((TextBlock)seletedItem.Children[1]).Text;
            string first_name = ((TextBlock)seletedItem.Children[2]).Text;
            string middle_name = ((TextBlock)seletedItem.Children[3]).Text;
            if (first_name == "Имя") return;

            User user = unitOfWork.Users.GetUserByName(first_name, last_name, middle_name);
            mainWindow.Navigation.Navigate(new UserPage(mainWindow, user));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }
    }
}
