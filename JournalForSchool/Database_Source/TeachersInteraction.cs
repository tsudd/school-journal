using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace JournalForSchool.Database_Source
{
    public static class TeachersInteraction
    {
        private static Context Db;
        public static void Insert_Teacher(User user, int subject_id)
        {
            var unitOfWork = UnitOfWork.GetInstance();

            Teacher teacher = new Teacher
            {
                SubjectId = subject_id,
                Id = user.Id
            };

            unitOfWork.Teachers.Create(teacher);
            unitOfWork.Save();
        }

        public static bool IsTeacher(User user)
        {
            var unitOfWork = UnitOfWork.GetInstance();
            var teacher = unitOfWork.Db.Teachers.FirstOrDefault(item => item.UserId == user.Id);

            /*
            foreach (var item in unitOfWork.Db.Teachers.ToList())
            {
                MessageBox.Show("Teacher id=" + item.Id + " user_id=" + item.User.Id + " find id=" + user.Id);
            }
          
            if (teacher == null) MessageBox.Show("Is not teacher");
            else MessageBox.Show("Is teacher");
            */

            if (teacher == null) return false;
            else return true;
            
        }

        public static Teacher GetTeacherModel(User user)
        {
            var unitOfWork = UnitOfWork.GetInstance();
            return unitOfWork.Db.Teachers.FirstOrDefault(item => item.UserId == user.Id);
        }

        public static User GetUserIdByTeachedId(int teacher_id)
        {
            var unitOfWork = UnitOfWork.GetInstance();

            var teacher_model = unitOfWork.Db.Teachers.FirstOrDefault(item => item.Id == teacher_id);

            return unitOfWork.Db.Users.FirstOrDefault(item => item.Id == teacher_model.UserId);            
        }
    }
}
