using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalForSchool
{
    public class UsersRepository : IRepository<User>
    {
        private UnitOfWork unitOfWork;
        private Context Db;
        
        public UsersRepository()
        {
            unitOfWork = UnitOfWork.GetInstance();
            Db = unitOfWork.Db;
        }

        public IEnumerable<User> GetAll()
        {
            return Db.Users;
        }

        public User Get(int id)
        {
            return Db.Users.Find(id);
        }

        public void Create(User user)
        {
            Db.Users.Add(user);
        }

        public void Update(User user)
        {
            Db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = Db.Users.Find(id);
            if (user != null)
                Db.Users.Remove(user);
        }

        public bool IsLoginExist(User user)
        {
            User findUser = Db.Users.FirstOrDefault(item => item.Login == user.Login);

            if (findUser != null) return true;
            return false;
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

            return UnitOfWork.GetInstance().Db.Users.FirstOrDefault(item => item.Login == login && item.Password == password);
        }

        public User GetUserByLogin(string login)
        {
            return Db.Users.FirstOrDefault(item => item.Login == login);
            
        }

        public List<User> GetAllUsersByClassId(int class_id)
        {
            List<User> list = new List<User>();
            list = Db.Users.Where(item => item.TheClassesId == class_id).OrderBy(item => item.LastName).ToList();
            return list;
        }

        public User GetUserByName(string first_name, string last_name, string middle_name)
        {
           return Db.Users.FirstOrDefault(item => item.FirstName == first_name &&
                                                        item.LastName == last_name &&
                                                        item.MiddleName == middle_name);
        }

        public User GetUserNameById(int user_id)
        {
            return Db.Users.FirstOrDefault(item => item.Id == user_id);
        }

        public User GetUserById(int user_id)
        {
            return Db.Users.FirstOrDefault(item => item.Id == user_id);
        }
    }
}
