using System;
using System.Collections.Generic;
using System.Data.Entity;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;

namespace JournalForSchool
{
    public class AdminsRepository : IRepository<Admin>
    {
        private readonly DataAccessAdmin dataAccess;

        public AdminsRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<Admin>() as DataAccessAdmin;
        }

        public IEnumerable<Admin> GetAll()
        {
            return dataAccess.GetAll();
        }

        public Admin Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(Admin admin)
        {
            dataAccess.Create(admin);
        }

        public void Update(Admin admin)
        {
            dataAccess.Update(admin);
        }

        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }

        public Admin GetAdminByUserId(int id)
        {
            return dataAccess.GetAdminByUserId(id);
        }
    }
}
