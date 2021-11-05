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
using System.Windows.Shapes;

namespace JournalForSchool.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminAddClassWindow.xaml
    /// </summary>
    public partial class AdminAddClassWindow : Window
    {

        MainWindow mainWindow;

        public AdminAddClassWindow(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
            The_class_name.ItemsSource = TheClassesInteraction.GetAllClassesNames();
        }

        private void The_class_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            The_class_letter.ItemsSource = TheClassesInteraction.GetTheClassesLettersByName((int)The_class_name.SelectedValue);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TheClasses TheClass = new TheClasses();

            TheClass.TheClass = (int)The_class_name.SelectedValue;
            TheClass.ClassLetter = (string)The_class_letter.SelectedValue;

            if (The_class_letter.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите другой класс!");
                return;
            }

            
            using (var unitOfWork = UnitOfWork.GetInstance())
            {
                unitOfWork.TheClasses.Create(TheClass);
                unitOfWork.Save();
            }

            The_class_letter.ItemsSource = TheClassesInteraction.GetTheClassesLettersByName((int)The_class_name.SelectedValue);
            The_class_letter.SelectedIndex = -1;

            MessageBox.Show("Новый класс был успешно добавлен!");
            this.Close();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите закрыть окно?", "Подвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}