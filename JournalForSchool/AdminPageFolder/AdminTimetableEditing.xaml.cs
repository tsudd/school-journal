using JournalForSchool.Database_Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccessLayer.Models;

namespace JournalForSchool.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminTimetableEditing.xaml
    /// </summary>
    public partial class AdminTimetableEditing : Page
    {
        MainWindow mainWindow;
        UnitOfWork unitOfWork;

        TheClasses curClass;

        private bool isMonday;
        private bool isTuesday;
        private bool isWednesday;
        private bool isThurday;
        private bool isFriday;

        public AdminTimetableEditing(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            unitOfWork = UnitOfWork.GetInstance();
            The_class_name.ItemsSource = unitOfWork.TheClasses.GetTheDisctinctClassesNames();

            Subject_delete.ItemsSource = unitOfWork.Subjects.GetSubjectsList();
            Teacher_delete.ItemsSource = unitOfWork.Teachers.GetAllTeachersNames();
        }

        private void Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StackPanel seletedItem;
                seletedItem = (StackPanel)e.AddedItems[0];

                string subject = ((TextBlock)seletedItem.Children[2]).Text;
                if (subject == "Урок" || subject.Length == 0) return;

                int SubjectId = mainWindow.unitOfWork.Subjects.GetSubjectId(subject);
                int LessonNumber = Int32.Parse(((TextBlock)seletedItem.Children[0]).Text);

                var timeTableModel = TimeTableInteraction.GetTimeTableModel(LessonNumber, SubjectId, curClass.Id);

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

            List<StackPanel> list = TimeTableInteraction.GetStackPanelListForUser(day, curClass.Id);

            foreach (var item in list)
            {
                TimeTable.Items.Add(item);
            }
        }

        private void Monday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Monday");

            ClearDaySelection();
            isMonday = true;
        }

        private void Tuesday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Tuesday");

            ClearDaySelection();
            isTuesday = true;
        }

        private void Wednesday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Wednesday");

            ClearDaySelection();
            isWednesday = true;
        }

        private void Thursday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Thursday");

            ClearDaySelection();
            isThurday = true;
        }

        private void Friday_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateStackPanel("Friday");

            ClearDaySelection();
            isFriday = true;
        }

        private void ClearDaySelection()
        {
            isMonday = false;
            isTuesday = false;
            isThurday = false;
            isWednesday = false;
            isFriday = false;
        }

        private void The_class_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            The_class_letter.ItemsSource = unitOfWork.TheClasses.GetTheClassesLetters((int)comboBox.SelectedValue);
            The_class_letter.SelectedIndex = 0;

            int theClassName = (int)The_class_name.SelectedValue;
            string theClassLetter = (string)The_class_letter.SelectedValue;

            curClass = unitOfWork.TheClasses.GetTheClassByFullName(theClassName + " " + theClassLetter);
        }

        private void The_class_letter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int theClassName = (int)The_class_name.SelectedValue;
            string theClassLetter = (string)The_class_letter.SelectedValue;

            curClass = unitOfWork.TheClasses.GetTheClassByFullName(theClassName + " " + theClassLetter);

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int Id = 0;

            try
            {
                Id = Int32.Parse(Id_Box.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Id должно быть числом!");
                return;
            }
            if (Id_Box.Text == "")
            {
                MessageBox.Show("Id поле не должно быть пустым!");
                return;
            }

            if (Id <= 0 || Id > TimeTable.Items.Count)
            {
                MessageBox.Show("Расписание с таким Id не существует!", "Error");
                return;
            }

            StackPanel seletedItem = (StackPanel)TimeTable.Items[Id];

            string subject = ((TextBlock)seletedItem.Children[2]).Text;
            if (subject == "Урок" || subject.Length == 0) return;

            int SubjectId = mainWindow.unitOfWork.Subjects.GetSubjectId(subject);
            int LessonNumber = Int32.Parse(((TextBlock)seletedItem.Children[0]).Text);

            // MessageBox.Show("subj=" + subject + " less numb=" + LessonNumber);

            var timeTableModel = TimeTableInteraction.GetTimeTableModel(LessonNumber, SubjectId, curClass.Id);

            unitOfWork.Timetable.Delete(timeTableModel.Id);
            UpdateAllStackPandel();
        }

        public void UpdateAllStackPandel()
        {
            if (isMonday == true) UpdateStackPanel("Monday");
            if (isTuesday == true) UpdateStackPanel("Tuesday");
            if (isWednesday == true) UpdateStackPanel("Wednesday");
            if (isThurday == true) UpdateStackPanel("Thurday");
            if (isFriday == true) UpdateStackPanel("Friday");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int Id;

            try
            {
                Id = Int32.Parse(Id_Box.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Id должно быть числом!");
                return;
            }
            if (Id_Box.Text == "")
            {
                MessageBox.Show("Id поле не должно быть пустым!");
                return;
            }

            if (Id <= 0 || Id > TimeTable.Items.Count)
            {
                MessageBox.Show("Расписание с таким Id не существует!", "Error");
            }

            StackPanel seletedItem = (StackPanel)TimeTable.Items[Id];

            string subject = ((TextBlock)seletedItem.Children[2]).Text;
            if (subject == "Урок" || subject.Length != 0) return;


            string subjectNew = (string)Subject_delete.Text;
            int teacherId = 0;

            var allTeachers = unitOfWork.Db.Teachers.ToList();
            
            foreach (var item in allTeachers)
            {
                var user = unitOfWork.Users.GetUserById(item.UserId);
                if (user.LastName + " " + user.FirstName + " " + user.MiddleName == (string)Teacher_delete.SelectedValue) teacherId = item.Id;
            }


            TimeTableInteraction.InsertTimeTable(
                Id,
                unitOfWork.Subjects.GetSubjectId(subjectNew),
                curClass.Id,
                teacherId,
                isMonday,
                isTuesday,
                isWednesday,
                isTuesday,
                isFriday
            );

            UpdateAllStackPandel();

            MessageBox.Show("Расписание успешно добавлено!");

        }
    }
}
