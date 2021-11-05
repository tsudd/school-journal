using JournalForSchool.Components;
using JournalForSchool.Database_Source;
using JournalForSchool.Login;
using JournalForSchool.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JournalForSchool
{
    public partial class LoginPage : Page
    {
        public MainWindow mainWindow;

        public LoginPage(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new RegistrationPage(mainWindow));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.ToLower();
            string password = passwordBox.Password;

            string hashedPassword = PasswordInteraction.GetPasswordHash(password);

            User user = mainWindow.unitOfWork.Users.GetUserByLoginAndPassword(login, hashedPassword);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Вы успешно авторизовались!", "Информация");

            mainWindow.user = user;

            if (TeachersInteraction.IsTeacher(user)) 
                mainWindow.Navigation.Navigate(new TeacherPage(mainWindow, TeachersInteraction.GetTeacherModel(user)));
            else if (AdminsInteraction.IsAdmin(user)) 
                mainWindow.Navigation.Navigate(new AdminPage(mainWindow));
            else 
                mainWindow.Navigation.Navigate(new UserPage(mainWindow, user));
        }

        private void ForgottenPassword_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.ToLower();
            if (login.Length != 0)
            {
                User user = mainWindow.unitOfWork.Users.GetUserByLogin(login);
                if (user == null)
                {
                    MessageBox.Show("Введённый вами логин некорректен. Пожалуйста, введите другой логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    PasswordRecoveryWindow recovery = new PasswordRecoveryWindow(mainWindow, user, login);
                    recovery.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Введите, пожалуйста, логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
