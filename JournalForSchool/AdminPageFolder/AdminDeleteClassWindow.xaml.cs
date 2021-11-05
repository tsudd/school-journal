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
    /// Логика взаимодействия для AdminDeleteClassWindow.xaml
    /// </summary>
    public partial class AdminDeleteClassWindow : Window
    {
        MainWindow mainWindow;
        private UnitOfWork unitOfWork;

        public AdminDeleteClassWindow(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow; 
            InitializeComponent();

            unitOfWork = UnitOfWork.GetInstance();
            The_class_name.ItemsSource = unitOfWork.TheClasses.GetTheDisctinctClassesNames();
        }

        private void The_class_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            The_class_letter.ItemsSource = unitOfWork.TheClasses.GetTheClassesLetters((int)comboBox.SelectedValue);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int the_class = Int32.Parse(The_class_name.SelectedItem.ToString());
            string letter = The_class_letter.SelectedValue.ToString();
            int The_class_id = unitOfWork.TheClasses.GetTheClassByNumber(the_class, letter);
            using (var unitOfWork = UnitOfWork.GetInstance())
            {
                unitOfWork.TheClasses.Delete(The_class_id);
                unitOfWork.Save();
            }
            MessageBox.Show("Класс был успешно удален!");
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
