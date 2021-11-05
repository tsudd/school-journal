using JournalForSchool.Database_Source;
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
using System.Data.Entity;

namespace JournalForSchool.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminDeleteUserPage.xaml
    /// </summary>
    public partial class AdminDeleteUserPage : Page
    {
        MainWindow mainWindow;
        private UnitOfWork unitOfWork;
        User user;
        Teacher teacher;
        public List<User> listOfUsers { get; set; }
        public List<Teacher> listOfTeachers { get; set; }

        public AdminDeleteUserPage(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
            InitializeComponent();

            unitOfWork = UnitOfWork.GetInstance();

            GetAllPupilsAndUpdate();
            GetAllTeachersAndUpdate();
        }

        private void g_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                user = (User)g.SelectedItem;
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }

        private void teachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { 
                teacher = (Teacher)teachers.SelectedItem;
            }
            catch
            {
                MessageBox.Show("Error");
            }
}

        private void GetAllPupilsAndUpdate()
        {

            listOfUsers = unitOfWork.Db.Users.Include(p => p.TheClasses).Where(p => p.TheClasses != null).ToList();
            g.ItemsSource = listOfUsers;
        }

        private void GetAllTeachersAndUpdate()
        {

            listOfTeachers = unitOfWork.Db.Teachers.Include("User").Include(p => p.Subject).ToList();
            teachers.ItemsSource = listOfTeachers;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                unitOfWork.Users.Delete(user.Id);
                unitOfWork.Save();
                GetAllPupilsAndUpdate();
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите добавить нового пользователя?", "Подвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                mainWindow.Navigation.Navigate(new AdminAddUserPage(mainWindow));
            }
        }

        private void Delete_Teacher_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                unitOfWork.Teachers.Delete(teacher.Id);
                unitOfWork.Users.Delete(teacher.Id);
                unitOfWork.Save();
                GetAllTeachersAndUpdate();
            }
        }
    }
}

