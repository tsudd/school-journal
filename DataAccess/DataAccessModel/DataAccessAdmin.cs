using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessAdmin : IDataAccess<Admin>
    {
        public const string GET_ALL_PROC = "sp_GetAllAdmins";
        public const string GET_BY_ID_PROC = "sp_GetAdminById";
        public const string CREATE_PROC = "sp_CreateAdmin";
        public const string UPDATE_PROC = "sp_UpdateAdmin";
        public const string DELETE_PROC = "sp_DeleteAdmin";
        public const string GET_ADMIN_BY_USER_ID = "sp_GetAdminByUserId";

        public const string ID_ARGUMENT = "id";
        public const string USERID_ARGUMENT = "userId";

        Db Connection;

        public DataAccessAdmin()
        {
            Connection = Db.GetInstance();
        }



        public void Create(Admin item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USERID_ARGUMENT, item.UserId)
            };
            Connection.ExecuteCommand<Admin>(CREATE_PROC, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            Connection.ExecuteCommand<Admin>(DELETE_PROC, parameters, false);   
        }

        public Admin Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            var admins = Connection.ExecuteCommand<Admin>(GET_BY_ID_PROC, parameters);
            if (admins.Count > 0)
            {
                return admins[0];
            }
            return null;
        }

        public Admin GetAdminByUserId(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(USERID_ARGUMENT, id)
            };
            var admins = Connection.ExecuteCommand<Admin>(GET_ADMIN_BY_USER_ID, parameters);
            if (admins.Count > 0)
            {
                return admins[0];
            }
            return null;
        }

        public IEnumerable<Admin> GetAll()
        {
            return Connection.ExecuteCommand<Admin>(GET_ALL_PROC);
        }

        public void Update(Admin item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, item.Id),
                new SqlParameter(USERID_ARGUMENT, item.UserId)
            };
            Connection.ExecuteCommand<Admin>(UPDATE_PROC, parameters, false);
        }
    }
}
