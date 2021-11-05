using JournalForSchool.Components;
using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JournalForSchool
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private MainWindow mainWindow;
        private User user;

        public UserPage(MainWindow _mainWindow, User _user)
        {
            InitializeComponent();


            mainWindow = _mainWindow;
            user = _user;
                
            TheClasses Class = mainWindow.unitOfWork.TheClasses.GetTheClassById(user.TheClassesId.Value, mainWindow);
            
            User_class_label.Content = Class.TheClass.ToString() + "-ого " + Class.ClassLetter;
            User_name_label.Content = user.LastName + " " + user.FirstName + " " + user.MiddleName;
            Number_label.Content = user.Phone;

            if (mainWindow.user.Id != _user.Id)
            {
                Upload_image.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrEmpty(user.ImagePath) == false)
            {
                imgPicture.Source = new BitmapImage(new Uri(user.ImagePath));
            }

            if (mainWindow.user.Id != user.Id)
            {
                Admin_Button.Visibility = Visibility.Hidden;
            }
                 
        }

        private void Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StackPanel seletedItem;
                seletedItem = (StackPanel)e.AddedItems[0];

                string subject = ((TextBlock)seletedItem.Children[2]).Text;
                if (subject == "Урок" || subject.Length == 0) return;

                if (user.TheClassesId == null)
                    user.TheClassesId = 0;
                else
                    user.TheClassesId = user.TheClassesId.Value;

                int SubjectId = mainWindow.unitOfWork.Subjects.GetSubjectId(subject);
                int LessonNumber = Int32.Parse(((TextBlock)seletedItem.Children[0]).Text);

                var timeTableModel = TimeTableInteraction.GetTimeTableModel(LessonNumber, SubjectId, user.TheClassesId.Value);

                // mainWindow.Navigation.Navigate(new SubjectMarks(mainWindow, "11 A", subject, user.The_class_id.Value));
                mainWindow.Navigation.Navigate(new SubjectMarks(mainWindow, timeTableModel));
            }
            catch (Exception)
            {
                return;
            }
        }

        private void UpdateStackPanel(string day)
        {
            while (TimeTable.Items.Count > 1) TimeTable.Items.RemoveAt(1);

            List<StackPanel> list = TimeTableInteraction.GetStackPanelListForUser(day, user.TheClassesId.Value);

            foreach (var item in list)
            {
                TimeTable.Items.Add(item);
            }
        }

        private void Monday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Monday");
            weekDay.SetResourceReference(System.Windows.Controls.Button.ContentProperty, "monday");
        }

        private void Tuesday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Tuesday");
            weekDay.SetResourceReference(System.Windows.Controls.Button.ContentProperty, "tuesday");
        }

        private void Wednesday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Wednesday");
            weekDay.SetResourceReference(System.Windows.Controls.Button.ContentProperty, "wednesday");
        }

        private void Thursday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Thursday");
            weekDay.SetResourceReference(System.Windows.Controls.Button.ContentProperty, "thursday");
        }

        private void Friday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Friday");
            weekDay.SetResourceReference(System.Windows.Controls.Button.ContentProperty, "friday");
        }

        private void TheClassPage_Button_Click(object sender, RoutedEventArgs e)
        { 
            mainWindow.Navigation.Navigate(new TheClassPage(mainWindow, user));
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }

        private void Admin_Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.Navigate(new UserEditPage(mainWindow, user));
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
                UsersInteraction.SetImagePath(ofdPicture.FileName, user);
            }
        }
    }
}
