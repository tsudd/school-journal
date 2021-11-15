using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace JournalForSchool
{
    public class TimetableRepository : IRepository<Timetable>
    {
        private readonly DataAccessTimetables dataAccess; 

        public TimetableRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<Timetable>() as DataAccessTimetables;
        }

        public IEnumerable<Timetable> GetAll()
        {
            return dataAccess.GetAll();
        }

        public Timetable Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(Timetable timetable)
        {
            dataAccess.Create(timetable);
        }

        public void Update(Timetable timetable)
        {
            dataAccess.Update(timetable);
        }

        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }

        public Timetable GetTimetableForUser(string day, int classId, int lessonNum)
        {
            return dataAccess.GetTimetableForUser(day, classId, lessonNum);
        }

        public Timetable GetTimetableForTeacher(string day, int teacherId, int lessonNum)
        {
            return dataAccess.GetTimetableForTeacher(day, teacherId, lessonNum);
        }

        public Timetable GetTimetableModel(int subjectId, int lessonNum, int classId)
        {
            return dataAccess.GetTimetableModel(subjectId, lessonNum, classId);
        }
    }
}

