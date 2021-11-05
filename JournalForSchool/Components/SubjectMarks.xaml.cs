using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using MySqlX.XDevAPI.Relational;
using Microsoft.Win32;
using System.IO;
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

namespace JournalForSchool
{
    /// <summary>
    /// Логика взаимодействия для SubjectMarks.xaml
    /// </summary>
    public partial class SubjectMarks : Page
    {
        private MainWindow mainWindow;
        private Timetable curTimeTable;
        UnitOfWork unitOfWork;

        private string the_class_name;
        private string subject_name;

        List<string> DatesRow;

        public SubjectMarks(MainWindow _mainWindow, Timetable timeTableModel)
        {
            InitializeComponent();
            unitOfWork = UnitOfWork.GetInstance();
            DatesRow = new List<string>();

            mainWindow = _mainWindow;
            curTimeTable = timeTableModel;

            TheClasses TheClass = unitOfWork.TheClasses.GetTheClassById(timeTableModel.ClassId);

            the_class_name = TheClass.TheClass.ToString() + " " + TheClass.ClassLetter;
            subject_name = unitOfWork.Subjects.GetSubjectName(timeTableModel.SubjectId);

            class_name.Content = the_class_name;
            subject.Content = subject_name;

            InitColumns(20);

            DataContext = new Application(timeTableModel.ClassId, mainWindow);

        }
            
        private void AddCol(string name, int combo_idx)
        {
            string combo_name = "K" + combo_idx.ToString();
            var col = new DataGridTemplateColumn { Header = name };
            var dt = new DataTemplate();

            var stkpnl = new FrameworkElementFactory(typeof(StackPanel));
            var comboBox = new FrameworkElementFactory(typeof(ComboBox));

            comboBox.SetValue(ComboBox.ItemsSourceProperty, new Binding("Mark_list"));
            comboBox.SetValue(ComboBox.NameProperty, combo_name);
            comboBox.SetValue(ComboBox.SelectedIndexProperty, 0);

            if (TeachersInteraction.IsTeacher(mainWindow.user) == false ||
                    (TeachersInteraction.IsTeacher(mainWindow.user) == true &&
                    TeachersInteraction.GetTeacherModel(mainWindow.user).Id != curTimeTable.TeacherId))
            {
                comboBox.SetValue(ComboBox.IsEnabledProperty, false);
            }

            comboBox.AddHandler(ComboBox.SelectionChangedEvent, new SelectionChangedEventHandler(ComboBox_Selected));

            stkpnl.AppendChild(comboBox);

            dt.VisualTree = stkpnl;
            col.CellTemplate = dt;

            MarksGrid.Columns.Add(col);
        }

        private void ComboBox_Selected(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            var cb = (ComboBox)sender;
            DataGridRow dataGridRow = FindParent<DataGridRow>(cb);

            int index = dataGridRow.GetIndex();
            int row = Int32.Parse(comboBox.Name.Substring(1));
            var listOfUsers = unitOfWork.Users.GetAllUsersByClassId(curTimeTable.ClassId);

            var markSelectedIdx = unitOfWork.Marks.GetMarkSelectedIndex(listOfUsers[index].Id, curTimeTable.Id, DatesRow[row]);
            if (comboBox.SelectedIndex == markSelectedIdx) return;
  
            if (comboBox.SelectedIndex == 0)
            {
                markSelectedIdx = unitOfWork.Marks.GetMarkSelectedIndex(listOfUsers[index].Id, curTimeTable.Id, DatesRow[row]);
                if (markSelectedIdx == comboBox.SelectedIndex) return;

                if (markSelectedIdx == -1)
                {
                    unitOfWork.Marks.DeleteIfExist(listOfUsers[index].Id, curTimeTable.Id, DatesRow[row]);
                    unitOfWork.Marks.InsertMark(listOfUsers[index].Id, curTimeTable.Id, DatesRow[row], comboBox.SelectedIndex);
                }
                else comboBox.SelectedIndex = markSelectedIdx;
            }
            else
            {
                unitOfWork.Marks.DeleteIfExist(listOfUsers[index].Id, curTimeTable.Id, DatesRow[row]);
                unitOfWork.Marks.InsertMark(listOfUsers[index].Id, curTimeTable.Id, DatesRow[row], comboBox.SelectedIndex);
            }
        }

        private void MarksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MarksGrid.UnselectAllCells();
        }

        public static Parent FindParent<Parent>(DependencyObject child) where Parent : DependencyObject
        {
            DependencyObject parentObject = child;

            while (!((parentObject is System.Windows.Media.Visual)
                    || (parentObject is System.Windows.Media.Media3D.Visual3D)))
            {
                if (parentObject is Parent || parentObject == null)
                {
                    return parentObject as Parent;
                }
                else
                {
                    parentObject = (parentObject as FrameworkContentElement).Parent;
                }
            }

            //We have not found parent yet , and we have now visual to work with.
            parentObject = VisualTreeHelper.GetParent(parentObject);

            //check if the parent matches the type we're looking for
            if (parentObject is Parent || parentObject == null)
            {
                return parentObject as Parent;
            }
            else
            {
                //use recursion to proceed with next level
                return FindParent<Parent>(parentObject);
            }
        }

        private void InitColumns(int RowsCount)
        {
            int daysToAdd = (0 - (int)DateTime.Now.DayOfWeek - 7) % 7;
            DateTime currentDay = DateTime.Now.AddDays(daysToAdd);

            int rowNow = 0;
            while (true)
            {
                currentDay = currentDay.AddDays(1);

                if (rowNow == RowsCount) break;
               
                if ((int)currentDay.DayOfWeek == 1 && curTimeTable.Monday == false) continue;
                if ((int)currentDay.DayOfWeek == 2 && curTimeTable.Tuesday == false) continue;
                if ((int)currentDay.DayOfWeek == 3 && curTimeTable.Wednesday == false) continue;
                if ((int)currentDay.DayOfWeek == 4 && curTimeTable.Thursday == false) continue;
                if ((int)currentDay.DayOfWeek == 5 && curTimeTable.Friday == false) continue;

                if ((int)currentDay.DayOfWeek >= 6 || (int)currentDay.DayOfWeek == 0) continue;
                
                AddCol(currentDay.ToString(" ddd \n dd.MM"), rowNow);
                DatesRow.Add(currentDay.ToString(" ddd \n dd.MM"));
                rowNow += 1;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigation.GoBack();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из системы?", "Подвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                    else
                        doc.Save(fs, DataFormats.Xaml);
                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                    else
                        doc.Load(fs, DataFormats.Xaml);
                }
            }
        }
    }
}
