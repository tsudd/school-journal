using JournalForSchool.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JournalForSchool.Database_Source
{
    public static class TimeTableInteraction
    {
        public static List<StackPanel> GetStackPanelListForUser(string day, int class_id)
        {
            var unitOfWork = UnitOfWork.GetInstance();

            List<StackPanel> list = new List<StackPanel>();
            List<string> timeTemplate = GetTimeTemplate();
            List<Timetable> timeTables = new List<Timetable>();

            for (int i = 0; i < 7; i++)
            {
                var curTimeTable = GetTimeTableForUser(day, class_id, i + 1);
                timeTables.Add(curTimeTable);
            }

            for (int i = 0; i < 7; i++)
            {
                StackPanel stackPanel = new StackPanel();

                TextBlock idBlock = new TextBlock();
                idBlock.Width = 25;
                idBlock.Text = (i + 1).ToString();

                TextBlock timeBlock = new TextBlock();
                timeBlock.Width = 120;
                timeBlock.Text = timeTemplate[i];

                TextBlock subjectBlock = new TextBlock();
                subjectBlock.Width = 150;
                    if (timeTables[i] != null) subjectBlock.Text = unitOfWork.Subjects.GetSubjectName(timeTables[i].SubjectId);
                    else subjectBlock.Text = "";

                TextBlock userNameBlock = new TextBlock();

                if (timeTables[i] != null)
                {
                    User teacher = unitOfWork.Users.GetUserNameById(TeachersInteraction.GetUserIdByTeachedId(timeTables[i].TeacherId).Id);
                    userNameBlock.Text = teacher.LastName + " " + teacher.FirstName + " " + teacher.MiddleName;
                }
                else
                {
                    userNameBlock.Text = "";
                }


                stackPanel.Children.Add(idBlock);
                stackPanel.Children.Add(timeBlock);
                stackPanel.Children.Add(subjectBlock);
                stackPanel.Children.Add(userNameBlock);

                stackPanel.Orientation = Orientation.Horizontal;
                list.Add(stackPanel);
            }
            return list;
            
          
        }

        public static Timetable GetTimeTableForUser(string day, int class_id, int lesson_number)
        {
            var unitOfWork = UnitOfWork.GetInstance();
            
            Timetable timetable = null;

            switch (day)
            {
                case "Monday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.ClassId == class_id &&
                                                                        item.Monday == true);
                    break;

                case "Tuesday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.ClassId == class_id &&
                                                                        item.Tuesday == true);
                    break;

                case "Wednesday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.ClassId == class_id &&
                                                                        item.Wednesday == true);
                    break;

                case "Thursday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.ClassId == class_id &&
                                                                        item.Thursday == true);
                    break;

                case "Friday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.ClassId == class_id &&
                                                                        item.Friday == true);
                    break;
                case "Default":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.ClassId == class_id &&
                                                                        item.Monday == true);
                    break;
            }

            return timetable;
        }

        public static Timetable GetTimeTableModel(int LessonNumber, int SubjectId, int ClassId) {

            var unitOfWork = UnitOfWork.GetInstance();
            Timetable timeTableModel = unitOfWork.Db.Timetable.First(item => item.LessonNumber == LessonNumber &&
                                                                            item.SubjectId == SubjectId &&
                                                                            item.ClassId == ClassId);

            return timeTableModel;
            
        }

        public static List<StackPanel> GetStackPanelListForTeacher(string day, int teacher_id)
        {
            List<StackPanel> list = new List<StackPanel>();
            List<string> timeTemplate = GetTimeTemplate();

            for (int i = 0; i < 7; i++)
            {
                StackPanel stackPanel = new StackPanel();

                TextBlock idBlock = new TextBlock();
                idBlock.Width = 25;
                idBlock.Text = (i + 1).ToString();

                TextBlock timeBlock = new TextBlock();
                timeBlock.Width = 100;
                timeBlock.Text = timeTemplate[i];

                TextBlock classBlock = new TextBlock();
                classBlock.Width = 65;
                TheClasses Class  = GetClassForTeacher(day, teacher_id, i + 1);
                if (Class != null) classBlock.Text = Class.TheClass + " " + Class.ClassLetter;
                else classBlock.Text = "";

                Timetable curTimeTable = null;
                if (Class != null) curTimeTable = GetTimeTableForUser(day, Class.Id, i + 1);

                TextBlock subjectBlock = new TextBlock();
                subjectBlock.Width = 130;

                var unitOfWork = UnitOfWork.GetInstance();

               if (Class != null && curTimeTable != null) subjectBlock.Text = unitOfWork.Subjects.GetSubjectName(curTimeTable.SubjectId);
                else subjectBlock.Text = "";
                

                TextBlock cabBlock = new TextBlock();
                cabBlock.Text = " - ";

                stackPanel.Children.Add(idBlock);
                stackPanel.Children.Add(timeBlock);
                stackPanel.Children.Add(classBlock);
                stackPanel.Children.Add(subjectBlock);
                stackPanel.Children.Add(cabBlock);

                stackPanel.Orientation = Orientation.Horizontal;
                list.Add(stackPanel);
            }


            return list;
        }

        public static TheClasses GetClassForTeacher(string day, int teacher_id, int lesson_number)
        {
            var unitOfWork = UnitOfWork.GetInstance();

            Timetable timetable = null;

            switch (day)
            {
                case "Monday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.TeacherId == teacher_id &&
                                                                        item.Monday == true);
                    break;

                case "Tuesday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.TeacherId == teacher_id &&
                                                                        item.Tuesday == true);
                    break;

                case "Wednesday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.TeacherId == teacher_id &&
                                                                        item.Wednesday == true);
                    break;

                case "Thursday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.TeacherId == teacher_id &&
                                                                        item.Thursday == true);
                    break;

                case "Friday":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.TeacherId == teacher_id &&
                                                                        item.Friday == true);
                    break;
                case "Defautlt":
                    timetable = unitOfWork.Db.Timetable.FirstOrDefault(item => item.LessonNumber == lesson_number &&
                                                                        item.TeacherId == teacher_id &&
                                                                        item.Monday == true);
                    break;
            }

            if (timetable == null) return null;
            return unitOfWork.TheClasses.GetTheClassById(timetable.ClassId);
        }
        

        public static void InsertTimeTable(int lessonNumber, int subjectId, int classId, int teacherId, bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday)
        {
            Timetable timetable = new Timetable
            {
                LessonNumber = lessonNumber,
                SubjectId = subjectId,
                ClassId = classId,
                TeacherId = teacherId,
                Monday = Monday,
                Tuesday = Tuesday,
                Wednesday = Wednesday,
                Thursday = Thursday,
                Friday = Friday,
            };

            var unitOfWork = UnitOfWork.GetInstance();

            unitOfWork.Db.Timetable.Add(timetable);
            unitOfWork.Db.SaveChanges();
        }


        private static List<string> GetTimeTemplate()
        {
            List<string> list = new List<string>();

            list.Add("08:30 – 09:15");
            list.Add("09:25 – 10:10");
            list.Add("10:20 – 11:05");
            list.Add("11:25 – 12:10");
            list.Add("12:25 – 13:10");
            list.Add("13:20 – 14:05");
            list.Add("14:15 – 15:00");

            return list;
        }

    }
}
