using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.DataAccessModel;
using DataAccessLayer.Models;
namespace JournalForSchool
{
    public class UsersRepository : IRepository<User>
    {
        private readonly DataAccessUsers dataAccess;
        
        public UsersRepository(AccessOrchestrator orchestrator = null)
        {
            if (orchestrator == null)
            {
                throw new ArgumentException();
            }
            dataAccess = orchestrator.GetModelAccess<User>() as DataAccessUsers;
        }

        public IEnumerable<User> GetAll()
        {
            return dataAccess.GetAll();
        }

        public User Get(int id)
        {
            return dataAccess.Get(id);
        }

        public void Create(User user)
        {
            dataAccess.Create(user);
        }

        public void Update(User user)
        {
            dataAccess.Update(user);
        }

        public void Delete(int id)
        {
            dataAccess.Delete(id);
        }

        public bool IsLoginExist(User user)
        {
            return dataAccess.IsLoginExist(user);
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            /*
            // Hash all passwords in DataBase; DONT TOUCH!
            foreach (var item in Db.Users.ToList())
            {
                item.Password = PasswordInteraction.GetPasswordHash(item.Password); 
            }
            Db.SaveChanges();
            */

            return dataAccess.GetUserByLoginAndPassword(login, password);
        }

        public User GetUserByLogin(string login)
        {
            return dataAccess.GetUserByLogin(login);
            
        }

        public List<User> GetAllUsersByClassId(int class_id)
        {
            return dataAccess.GetAllUsersByClassId(class_id);
        }

        public User GetUserByName(string first_name, string last_name, string middle_name)
        {
            return dataAccess.GetUserByName(first_name, last_name, middle_name);
        }

        public User GetUserNameById(int user_id)
        {
            return dataAccess.GetUserNameById(user_id);
        }

        public User GetUserById(int user_id)
        {
            return GetUserById(user_id);
        }

        public List<User> GetPupils()
        {
            return dataAccess.GetPupils();
        }
    }
}
