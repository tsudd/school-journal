using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JournalForSchool
{
    public class TeachersRepository : IRepository<Teacher>
    {
        private Context db;

        public TeachersRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return db.Teachers;
        }

        public Teacher Get(int id)
        {
            return db.Teachers.Find(id);
        }

        public void Create(Teacher teacher)
        {
            db.Teachers.Add(teacher);
        }

        public void Update(Teacher teacher)
        {
            db.Entry(teacher).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher != null)
                db.Teachers.Remove(teacher);
        }

        public List<string> GetAllTeachersNames()
        {
            List<string> listNames = new List<string>();
            UnitOfWork unitOfWork = UnitOfWork.GetInstance();
            
            var allTeachers = db.Teachers.ToList();

            foreach (var item in allTeachers)
            {
                var user = unitOfWork.Users.GetUserById(item.UserId);
                listNames.Add(user.LastName + " " + user.FirstName + " " + user.MiddleName);
            }

            return listNames;
        }
    }
}
