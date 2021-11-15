using DataAccessLayer.Models;
using System.Linq;

namespace JournalForSchool.Database_Source
{
    public static class TeachersInteraction
    {
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
            var teacher = unitOfWork.Teachers.Get(user.Id);

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
            return unitOfWork.Teachers.GetTeacherByUserId(user.Id);
        }

        public static User GetUserIdByTeachedId(int teacher_id)
        {
            var unitOfWork = UnitOfWork.GetInstance();

            var teacher_model = unitOfWork.Teachers.Get(teacher_id);

            return unitOfWork.Users.Get(teacher_model.Id);            
        }
    }
}
