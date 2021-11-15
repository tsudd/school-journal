using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JournalForSchool
{
    public class AccessOrchestrator
    {
        public IDataAccess<T> GetModelAccess<T>()
        {
            var modelType = typeof(T);
            if (modelType == typeof(Admin))
            {
                return new DataAccessAdmin() as IDataAccess<T>;
            }
            if (modelType == typeof(TheClasses))
            {
                return new DataAccessClasses() as IDataAccess<T>;
            }
            if (modelType == typeof(Mark))
            {
                return new DataAccessMarks() as IDataAccess<T>;
            }
            if (modelType == typeof(Subjects))
            {
                return new DataAccessSubjects() as IDataAccess<T>;
            }
            if (modelType == typeof(Teacher))
            {
                return new DataAccessTeachers() as IDataAccess<T>;
            }
            if (modelType == typeof(Timetable))
            {
                return new DataAccessTimetables() as IDataAccess<T>;
            }
            if (modelType == typeof(User))
            {
                return new DataAccessUsers() as IDataAccess<T>;
            }
            return null;
        }
    }
}
