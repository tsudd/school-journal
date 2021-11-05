using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Repositories
{
    public class MarksRepository : IRepository<Mark>
    {
        private Context db;

        public MarksRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Mark> GetAll()
        {
            return db.Marks;
        }

        public Mark Get(int id)
        {
            return db.Marks.Find(id);
        }

        public void Create(Mark mark)
        {
            db.Marks.Add(mark);
        }

        public void Update(Mark mark)
        {
            db.Entry(mark).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Mark mark = db.Marks.Find(id);
            if (mark != null)
                db.Marks.Remove(mark);
        }
        
        public int GetMarkSelectedIndex(int User_id, int TimeTable_id, string Date)
        {
            var markModel = db.Marks.FirstOrDefault(item => item.UserId == User_id &&
                                                           item.TimeTableId == TimeTable_id &&
                                                           item.Date == Date);

            if (markModel == null) return -1;
            else return markModel.SelectedIndex;
        }

        public void DeleteIfExist(int User_id, int TimeTable_id, string Date)
        {
            var markModel = db.Marks.FirstOrDefault(item => item.UserId == User_id &&
                                                           item.TimeTableId == TimeTable_id &&
                                                           item.Date == Date);
            if (markModel != null)
            {
                db.Marks.Remove(markModel);
                db.SaveChanges();
            }
        }


        public  void InsertMark(int User_id, int TimeTable_id, string Date, int Selected_index)
        {
            var markModel = new Mark
            {
                UserId = User_id,
                TimeTableId = TimeTable_id,
                Date = Date,
                SelectedIndex = Selected_index
            };

            db.Marks.Add(markModel);
            db.SaveChanges();
        }
    }
}
