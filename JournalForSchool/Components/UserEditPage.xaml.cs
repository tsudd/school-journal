using JournalForSchool.Database_Source;
using JournalForSchool.Login;
using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JournalForSchool.Components
{
    /// <summary>
    /// Логика взаимодействия для AdminEditPage.xaml
    /// </summary>
    public partial class UserEditPage : Page
    {
        public MainWindow mainWindow;
        UnitOfWork unitOfWork;
        private User user;

        public UserEditPage(MainWindow _mainWindow, User _user)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            user = _user;
            unitOfWork = UnitOfWork.GetInstance();

            initializeObjects();
        }

        private void initializeObjects()
        {
            First_NameBox.Text = user.FirstName;
            Middle_NameBox.Text = user.MiddleName;
            Last_NameBox.Text = user.LastName;

            LoginBox.Text = user.Login;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string first_name = First_NameBox.Text;
            string middle_name = Middle_NameBox.Text;
            string last_name = Last_NameBox.Text;

            string login = LoginBox.Text.ToLower();

            string password = PasswordBox.Password;
            string password_copy = PasswordBox_copy.Password;

            if (password.Length != 0 && password != password_copy)
            {
                MessageBox.Show("Введённые пароли не совпадают!", "Ошибка!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                return;
            }

            user.FirstName = first_name;
            user.MiddleName = middle_name;
            user.LastName = last_name;

            user.Login = login;
            if (password.Length != 0 && password_copy.Length != 0 && password == password_copy)
            {
                user.Password = PasswordInteraction.GetPasswordHash(password);
            }

            unitOfWork.Users.Update(user);
            unitOfWork.Db.SaveChanges();

            mainWindow.Navigation.Navigate(new UserPage(mainWindow, user));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }


    }
}

