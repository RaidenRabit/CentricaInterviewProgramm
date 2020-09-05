using System;
using Microsoft.Data.SqlClient;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbConnection
    {
        private static DbConnection _instance;
        private SqlConnection _con = null;
        private string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CentricaProgram;Integrated Security=SSPI";

        public static DbConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DbConnection();
            }
            return _instance;
        }

        private DbConnection()
        {
           _con = new SqlConnection(_connectionString);
           _con.Open();
        }

        #region Connection
        public SqlConnection GetConnection()
        {
            return _con;
        }

        public bool CloseConnection()
        {
            try
            {
                _con.Close();
                _instance = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
