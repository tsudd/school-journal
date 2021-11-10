using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataAccessLayer.DataAccessModel
{
    public class DataAccessUsers : IDataAccess<User>
    {
        Db Connection;
        public const string GET_ALL_PROC = "sp_GetAllUsers";
        public const string GET_BY_ID_PROC = "sp_GetUserById";
        public const string GET_BY_LOGIN_AND_PASSWORD_PROC = "sp_GetUserByLoginAndPassword";
        public const string GET_BY_LOGIN_PROC = "sp_GetUserByLogin";
        public const string GET_ALL_USERS_BY_CLASS_ID_PROC = "sp_GetAllUsersByClassId";
        public const string GET_BY_NAME = "sp_GetUserByName";

        public const string CREATE_USER = "sp_CreateUser";
        public const string UPDATE_PROC = "sp_UpdateUser";
        public const string DELETE_PROC = "sp_DeleteUser";
        public const string IS_LOGIN_EXISTS_PROC = "sp_IsLoginExistsForUser";

        public const string ID_ARGUMENT = "id";
        public const string FIRST_NAME_ARGUMENT = "firstName";
        public const string MIDDLE_NAME_ARGUMENT = "middleName";
        public const string LAST_NAME_ARGUMENT = "lastName";
        public const string THE_CLASSES_ARGUMENT = "theClassesId";
        public const string LOGIN_ARGUMENT = "login";
        public const string PASSWORD_ARGUMENT = "password";
        public const string PHONE_ARGUMENT = "phone";
        public const string IMAGE_PATH_ARGUMENT = "imagePath";
        
        public DataAccessUsers()
        {
            Connection = Db.GetInstance();
        }

        public void Create(User item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(FIRST_NAME_ARGUMENT, item.FirstName),
                new SqlParameter(MIDDLE_NAME_ARGUMENT, item.MiddleName),
                new SqlParameter(LAST_NAME_ARGUMENT, item.LastName),
                new SqlParameter(THE_CLASSES_ARGUMENT, item.TheClassesId),
                new SqlParameter(LOGIN_ARGUMENT, item.Login),
                new SqlParameter(PASSWORD_ARGUMENT, item.Password),
                new SqlParameter(PHONE_ARGUMENT, item.Phone),
                new SqlParameter(IMAGE_PATH_ARGUMENT, item.ImagePath)
            };
            Connection.ExecuteCommand<Admin>(CREATE_USER, parameters, false);
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            Connection.ExecuteCommand<Admin>(DELETE_PROC, parameters, false);
        }

        public User Get(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, id)
            };
            var result = Connection.ExecuteCommand<User>(GET_BY_ID_PROC, parameters, true);
            if (result == null)
                return null;
            else return result[0];
        }

        public IEnumerable<User> GetAll()
        {
            return Connection.ExecuteCommand<User>(GET_ALL_PROC, null, true);
        }

        public void Update(User item)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(ID_ARGUMENT, item.Id),
                new SqlParameter(FIRST_NAME_ARGUMENT, item.FirstName),
                new SqlParameter(MIDDLE_NAME_ARGUMENT, item.MiddleName),
                new SqlParameter(LAST_NAME_ARGUMENT, item.LastName),
                new SqlParameter(THE_CLASSES_ARGUMENT, item.TheClassesId),
                new SqlParameter(LOGIN_ARGUMENT, item.Login),
                new SqlParameter(PASSWORD_ARGUMENT, item.Password),
                new SqlParameter(PHONE_ARGUMENT, item.Phone),
                new SqlParameter(IMAGE_PATH_ARGUMENT, item.ImagePath)
            };
            Connection.ExecuteCommand<User>(UPDATE_PROC, parameters, false);
        }

        public bool IsLoginExist(User user)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(LOGIN_ARGUMENT, user.Login)
            };
            try
            {
                Connection.ExecuteCommand<User>(IS_LOGIN_EXISTS_PROC, parameters, false);
            }
            catch
            {
                return false;
            }
            return true;
        }
        
        public User GetUserByLoginAndPassword(string login, string password)
        {
            // ???? need hashing
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(LOGIN_ARGUMENT, login),
                new SqlParameter(PASSWORD_ARGUMENT, password)
            };
            try
            {
                var result = Connection.ExecuteCommand<User>(GET_BY_LOGIN_AND_PASSWORD_PROC, parameters, true);
                return result[0];
            }
            catch
            {
                return null;
            }
            /*
            // Hash all passwords in DataBase; DONT TOUCH!
            foreach (var item in Db.Users.ToList())
            {
                item.Password = PasswordInteraction.GetPasswordHash(item.Password); 
            }
            Db.SaveChanges();

            return UnitOfWork.GetInstance().Db.Users.FirstOrDefault(item => item.Login == login && item.Password == password);
            */
        }

        public User GetUserByLogin(string login)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(LOGIN_ARGUMENT, login)
            };
            try
            {
                var result = Connection.ExecuteCommand<User>(GET_BY_LOGIN_PROC, parameters, true);
                return result[0];
            }
            catch
            {
                return null;
            }
        }
        
        public List<User> GetAllUsersByClassId(int class_id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(THE_CLASSES_ARGUMENT, class_id)
            };
            try
            {
                var result = Connection.ExecuteCommand<User>(GET_ALL_USERS_BY_CLASS_ID_PROC, parameters, true);
                return result;
            }
            catch
            {
                return new List<User>(0);
            }
        }

        public User GetUserByName(string first_name, string last_name, string middle_name)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter(FIRST_NAME_ARGUMENT, first_name),
                new SqlParameter(MIDDLE_NAME_ARGUMENT, middle_name),
                new SqlParameter(LAST_NAME_ARGUMENT, last_name)
            };
            try
            {
                var result = Connection.ExecuteCommand<User>(GET_BY_NAME, parameters, true);
                return result[0];
            }catch
            {
                return null;
            }
        }

        public User GetUserById(int user_id)
        {
            // ???
            return Get(user_id);
        }

        public User GetUserNameById(int user_id)
        {
            // ???
            return Get(user_id);
        }
       
    }
}
