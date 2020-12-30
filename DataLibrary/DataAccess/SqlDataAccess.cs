using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;


namespace DataLibrary.DataAccess
{
    public class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = 
            "BugCoreDB")
        {
            return "Server=(localdb)\\mssqllocaldb;Database=BugCoreDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            //return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql, string userId)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                Dapper.DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@userId", userId);
                return cnn.Query<T>(sql, parameter).AsList();
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static int DeleteData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                Dapper.DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", data);
                return cnn.Execute(sql, parameter);
            }
        }
    }
}
