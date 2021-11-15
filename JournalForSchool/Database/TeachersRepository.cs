using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace JournalForSchool
{
    public class TeachersRepository : IRepository<Teacher>
    {
        private readonly DataAccessTeachers dataAccess;

        public TeachersRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<Teacher>() as DataAccessTeachers;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return dataAccess.GetAll();
        }

        public Teacher Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(Teacher teacher)
        {
            dataAccess.Create(teacher);
        }

        public void Update(Teacher teacher)
        {
            dataAccess.Update(teacher);
        }

        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }

        public List<string> GetAllTeachersNames()
        {
            return dataAccess.GetAllTeachersNames();
        }

        public Teacher GetTeacherByUserId(int id)
        {
            return dataAccess.GetTeacherByUserId(id);
        }
    }
}
