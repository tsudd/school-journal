using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool.Repositories
{
    public class AdminsRepository : IRepository<Admin>
    {
        private Context db;

        public AdminsRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Admin> GetAll()
        {
            return db.Admins;
        }

        public Admin Get(int id)
        {
            return db.Admins.Find(id);
        }

        public void Create(Admin admin)
        {
            db.Admins.Add(admin);
        }

        public void Update(Admin admin)
        {
            db.Entry(admin).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Admin admin = db.Admins.Find(id);
            if (admin != null)
                db.Admins.Remove(admin);
        }
    }
}
