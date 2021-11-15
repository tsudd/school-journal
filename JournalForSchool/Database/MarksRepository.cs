using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace JournalForSchool.Repositories
{
    public class MarksRepository : IRepository<Mark>
    {
        private readonly DataAccessMarks dataAccess;

        public MarksRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<Mark>() as DataAccessMarks;
        }

        public IEnumerable<Mark> GetAll()
        {
            return dataAccess.GetAll();
        }

        public Mark Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(Mark mark)
        {
            dataAccess.Create(mark);
        }

        public void Update(Mark mark)
        {
            dataAccess.Update(mark);
        }

        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }
        
        public int GetMarkSelectedIndex(int User_id, int TimeTable_id, string Date)
        {
            return dataAccess.GetMarkSelectedIndex(User_id, TimeTable_id, Date);
        }

        public void DeleteIfExist(int User_id, int TimeTable_id, string Date)
        {
            dataAccess.DeleteIfExists(User_id, TimeTable_id, Date);
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

            Create(markModel);
        }
    }
}
