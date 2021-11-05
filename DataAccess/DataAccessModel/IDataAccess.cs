using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataAccessLayer.DataAccessModel
{
    interface IDataAccess<T>
    { 
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
