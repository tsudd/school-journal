using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace JournalForSchool
{
    public class SubjectsRepository : IRepository<Subjects>
    {
        private readonly DataAccessSubjects dataAccess;

        public SubjectsRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<Subjects>() as DataAccessSubjects;
        }

        public IEnumerable<Subjects> GetAll()
        {
            return dataAccess.GetAll();
        }

        public Subjects Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(Subjects subject)
        {
            dataAccess.Create(subject);
        }

        public void Update(Subjects subject)
        {
            dataAccess.Update(subject);
        }

        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }

        public List<string> GetSubjectsList()
        {
            return dataAccess.GetSubjectNames();
        }

        public string GetSubjectName(int subject_id)
        {
            return dataAccess.GetSubjectName(subject_id);
        }

        public int GetSubjectId(string subject_name)
        {
            return dataAccess.GetSubjectIdByName(subject_name);
        }
    }
}
