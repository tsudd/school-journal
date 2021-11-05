using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessUsers : IDataAccess<User>
    {
        Db Connection;
        public DataAccessUsers()
        {
            Connection = Db.GetInstance();
        }
        public void Create(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            return Connection.ExecuteCommand<User>("sp_GetUsers", null, true);
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
