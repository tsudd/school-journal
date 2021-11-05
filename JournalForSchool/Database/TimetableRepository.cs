using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JournalForSchool
{
    public class TimetableRepository : IRepository<Timetable>
    {
        private Context db;

        public TimetableRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Timetable> GetAll()
        {
            return db.Timetable;
        }

        public Timetable Get(int id)
        {
            return db.Timetable.Find(id);
        }

        public void Create(Timetable timetable)
        {
            db.Timetable.Add(timetable);
        }

        public void Update(Timetable timetable)
        {
            db.Entry(timetable).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Timetable timetable = db.Timetable.FirstOrDefault(item => item.Id == id);
            if (timetable != null)
            {
                db.Timetable.Remove(timetable);
                db.SaveChanges();
            }
        }
    }
}

