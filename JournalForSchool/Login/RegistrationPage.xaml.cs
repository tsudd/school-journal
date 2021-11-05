using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace JournalForSchool.Login
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public MainWindow mainWindow;
        UnitOfWork unitOfWork;

        public RegistrationPage(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            unitOfWork = UnitOfWork.GetInstance();

            Position_name.ItemsSource = new List<string>() { "Ученик", "Учитель" };
            The_class_name.ItemsSource = unitOfWork.TheClasses.GetTheDisctinctClassesNames();
            Specialisation.ItemsSource = unitOfWork.Subjects.GetSubjectsList();

            The_class_label.Visibility = Visibility.Collapsed;
            The_class_name.Visibility = Visibility.Collapsed;
            The_class_letter.Visibility = Visibility.Collapsed;

            Specialisation_label.Visibility = Visibility.Collapsed;
            Specialisation.Visibility = Visibility.Collapsed;
        }

        private bool PhoneValidation()
        {
            bool test = true;
            string regex = @"^\+375(17|29|33|44)[0-9]{7}$";
            string phone = PhoneBox.Text;
            if (Regex.IsMatch(phone, regex))
            {
                test = true;
            }
            else
            {
                test = false;
            }
            return test;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            
            string login = LoginBox.Text.ToLower();

            string password = PasswordBox.Password;
            string password_copy = PasswordBox_copy.Password;

            string first_name = First_NameBox.Text;
            string middle_name = Middle_NameBox.Text;
            string last_name = Last_NameBox.Text;

            string phone = PhoneBox.Text;

            int the_class = Int32.Parse(The_class_name.SelectedItem.ToString());
            string letter = The_class_letter.SelectedValue.ToString();

            if (Position_name.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите роль!");
                return;
            }

            if(!PhoneValidation())
            {
                MessageBox.Show("Значение номера телефона должно иметь следующий формат: +375(##)#######", "Ошибка");
                return;
            }
            
            if (login.Length == 0 || password.Length == 0 || password_copy.Length == 0 || first_name.Length == 0 || middle_name.Length == 0 || last_name.Length == 0)
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка");
                return;
            }
            else if (password != password_copy)
            {
                MessageBox.Show("Введённые пароли не совпадают!", "Ошибка");
                return;
            }

            User user = new User
            {
                FirstName = first_name,
                MiddleName = middle_name,
                LastName = last_name,

                Login = login,
                Password = PasswordInteraction.GetPasswordHash(password),

                Phone = phone,

                TheClassesId = unitOfWork.TheClasses.GetTheClassByNumber(the_class, letter)
            };
            if (Position_name.SelectedValue.ToString() == "Учитель") user.TheClassesId = null;



                if (UsersInteraction.RegisterRequestStatus(user) == false)
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован!", "Error");
                return;
            }

            if (Position_name.SelectedValue.ToString() == "Учитель")
            {
                int subject_id = Specialisation.SelectedIndex + 1;
                TeachersInteraction.Insert_Teacher(user, subject_id);
            }

            MessageBox.Show("Вы успешно зарегистрировались!", "Info");
            user = unitOfWork.Users.GetUserByName(first_name, last_name, middle_name);

            mainWindow.user = user;

            if (TeachersInteraction.IsTeacher(user))
                mainWindow.Navigation.Navigate(new TeacherPage(mainWindow, TeachersInteraction.GetTeacherModel(user)));
            else
                mainWindow.Navigation.Navigate(new UserPage(mainWindow, user));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new LoginPage(mainWindow));
        }

        private void Position_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Position_name.SelectedItem.ToString() == "Учитель")
            {
                The_class_label.Visibility = Visibility.Collapsed;
                The_class_name.Visibility = Visibility.Collapsed;
                The_class_letter.Visibility = Visibility.Collapsed;

                Specialisation_label.Visibility = Visibility.Visible;
                Specialisation.Visibility = Visibility.Visible;
            }
            else
            {
                The_class_label.Visibility = Visibility.Visible;
                The_class_name.Visibility = Visibility.Visible;
                The_class_letter.Visibility = Visibility.Visible;

                Specialisation_label.Visibility = Visibility.Collapsed;
                Specialisation.Visibility = Visibility.Collapsed;
            }
        }

        private void The_class_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            The_class_letter.ItemsSource = unitOfWork.TheClasses.GetTheClassesLetters((int)comboBox.SelectedValue);
        }
    }
}
