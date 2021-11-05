using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System;
using JournalForSchool.Components;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace JournalForSchool
{
    /// <summary>
    /// Логика взаимодействия для TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Page
    {
        private MainWindow mainWindow;
        private Teacher teacher;
        UnitOfWork unitOfWork;

        public TeacherPage(MainWindow _mainWindow, Teacher _teacher)
        {
            InitializeComponent();
            unitOfWork = UnitOfWork.GetInstance();

            mainWindow = _mainWindow;
            teacher = _teacher;

            User userModel = unitOfWork.Users.GetUserById(teacher.UserId);

            TeacherName.Content = userModel.LastName + " " +  userModel.FirstName + " " + userModel.MiddleName;
            Specialization.Content = unitOfWork.Subjects.GetSubjectName(teacher.SubjectId);
            Phone.Content = userModel.Phone;

            if (mainWindow.user.Id != userModel.Id)
            {
                Upload_image.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrEmpty(userModel.ImagePath) == false)
            {
                imgPicture.Source = new BitmapImage(new Uri(userModel.ImagePath));
            }
        }

        private void Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StackPanel seletedItem;
                seletedItem = (StackPanel)e.AddedItems[0];

                string subject = ((TextBlock)seletedItem.Children[3]).Text;
                if (subject == "Предмет" || subject.Length == 0) return;

                int SubjectId = unitOfWork.Subjects.GetSubjectId(subject);
                int LessonNumber = Int32.Parse(((TextBlock)seletedItem.Children[0]).Text);

                string classDescription = ((TextBlock)seletedItem.Children[2]).Text;
                TheClasses curClass = unitOfWork.TheClasses.GetTheClassByFullName(classDescription);
 
                var timeTableModel = TimeTableInteraction.GetTimeTableModel(LessonNumber, SubjectId, curClass.Id);

                mainWindow.Navigation.Navigate(new SubjectMarks(mainWindow, timeTableModel));
            
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }
            
        }

        private void UpdateStackPanel(string day)
        {
            while (TimeTable.Items.Count > 1) TimeTable.Items.RemoveAt(1);

            List<StackPanel> list = TimeTableInteraction.GetStackPanelListForTeacher(day, teacher.Id);

            foreach (var item in list)
            {
                TimeTable.Items.Add(item);
            }
        }

        private void Monday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Monday");
        }

        private void Tuesday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Tuesday");
        }

        private void Wednesday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Wednesday");
        }

        private void Thursday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Thursday");
        }

        private void Friday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Friday");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new TeacherEditPage(mainWindow, teacher));
        }

        private void Upload_image_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == DialogResult.OK)
            {
                imgPicture.Source = new BitmapImage(new Uri(ofdPicture.FileName));
                UsersInteraction.SetImagePath(ofdPicture.FileName, TeachersInteraction.GetUserIdByTeachedId(teacher.Id));
            }
            
        }
    }
}
