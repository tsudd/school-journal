using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataAccessModel
{
    class DataAccessAdmin : IDataAccess<Admin>
    {
        //private Context db;

        //public AdminsRepository(Context context)
        //{
        //    this.db = context;
        //}

        //public IEnumerable<Admin> GetAll()
        //{
        //    return db.Admins;
        //}

        //public Admin Get(int id)
        //{
        //    return db.Admins.Find(id);
        //}

        //public void Create(Admin admin)
        //{
        //    db.Admins.Add(admin);
        //}

        //public void Update(Admin admin)
        //{
        //    db.Entry(admin).State = EntityState.Modified;
        //}

        //public void Delete(int id)
        //{
        //    Admin admin = db.Admins.Find(id);
        //    if (admin != null)
        //        db.Admins.Remove(admin);
        //}
        //public DbConnection DbConnection => throw new NotImplementedException();

        //DbConnection Connection => throw new NotImplementedException();
        Db Connection;

        public DataAccessAdmin(String connectionString)
        {
            Connection = Db.GetInstance();
        }

        public void Create(Admin item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Admin Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Admin item)
        {
            throw new NotImplementedException();
        }
    }
}
