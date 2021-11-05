using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool
{
    public class SubjectsRepository : IRepository<Subjects>
    {
        private UnitOfWork unitOfWork;

        public SubjectsRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
        }

        public IEnumerable<Subjects> GetAll()
        {
            return unitOfWork.Db.Subjects;
        }

        public Subjects Get(int id)
        {
            return unitOfWork.Db.Subjects.Find(id);
        }

        public void Create(Subjects subject)
        {
            unitOfWork.Db.Subjects.Add(subject);
        }

        public void Update(Subjects subject)
        {
            unitOfWork.Db.Entry(subject).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Subjects subject = unitOfWork.Db.Subjects.Find(id);
            if (subject != null)
                unitOfWork.Db.Subjects.Remove(subject);
        }

        public List<string> GetSubjectsList()
        {
            var list = unitOfWork.Db.Subjects.Select(item => item.SubjectName);
            return list.ToList();
        }

        public string GetSubjectName(int subject_id)
        {
            Subjects subject = unitOfWork.Db.Subjects.FirstOrDefault(item => item.Id == subject_id);
            return subject.SubjectName;
        }

        public int GetSubjectId(string subject_name)
        {
            Subjects subject = unitOfWork.Db.Subjects.FirstOrDefault(item => item.SubjectName == subject_name);
            return subject.Id;
        }
    }
}
