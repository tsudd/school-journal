﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer
{
    class Db : IDisposable
    {
        string connectionString = "";
        private readonly SqlConnection connection;
        private static Db instance;
        private bool disposed = false;
        public static Db GetInstance()
        {
            if (instance == null)
            {
                instance = new Db("Data Source=WIN-M0F5PG83R5T;Initial Catalog=JournalForSchool;Integrated Security=True");
            }
            return instance;
        }
        private Db(string connectionStr)
        {
            string connectionString = connectionStr;
            using (var scope = new TransactionScope())
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                scope.Complete();
            }
        }

        public List<TModel> ExecuteCommand<TModel>(string storedProcedureName, SqlParameterCollection sqlParameters = null, bool execRead = true) where TModel : new()
        {
            var modelType = typeof(TModel);
            var modelFields = modelType.GetProperties();
            var response = new List<TModel>();
            try
            {
                var command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (sqlParameters != null)
                {
                    foreach(var p in sqlParameters)
                    {
                        command.Parameters.Add(p);
                    }
                }
                using (var scope = new TransactionScope())
                {
                    scope.Complete();
                    if (execRead)
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new TModel();
                                foreach (var modelField in modelFields)
                                {
                                    object valueFromReader;
                                    try
                                    {
                                        valueFromReader = reader[modelField.Name];
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                    if (valueFromReader is DBNull)
                                    {
                                        valueFromReader = null;
                                    }
                                    modelField.SetValue(model, valueFromReader);
                                }
                                response.Add(model);
                            }
                        }
                    } 
                    else
                    {
                        _ = command.ExecuteScalar();
                        response = null;
                    }
                    return response;
                }
            }
            catch
            {
                throw new Exception("invalid model or command");
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                 
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
