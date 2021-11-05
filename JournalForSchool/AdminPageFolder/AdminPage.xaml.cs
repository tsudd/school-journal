using JournalForSchool.AdminPageFolder;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>

    public partial class AdminPage : Page
    {

        MainWindow mainWindow;

        public AdminPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }

        private void Add_Class_Click(object sender, RoutedEventArgs e)
        {
            AdminAddClassWindow addClass = new AdminAddClassWindow(mainWindow);
            addClass.ShowDialog();
        }

        private void Add_Mark_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new AdminTimetableEditing(mainWindow));
        }

        private void Delete_Class_Click(object sender, RoutedEventArgs e)
        {
            AdminDeleteClassWindow deleteClass = new AdminDeleteClassWindow(mainWindow);
            deleteClass.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new AdminAddUserPage(mainWindow));
        }

        private void Delete_User_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new AdminDeleteUserPage(mainWindow));
        }
    }
}
