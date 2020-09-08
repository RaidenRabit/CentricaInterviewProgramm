using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbConnection
    {
        private static DbConnection _instance;
        private SqlConnection _con = null;
        private SqlTransaction _transaction = null;
        string _connectionString = @"Data Source=localhost;Initial Catalog=CentricaProgram;User ID=sa; password=Password12!";

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

        public SqlTransaction BeginTransaction()
        {
            _transaction = _con.BeginTransaction(IsolationLevel.RepeatableRead);
            return _transaction;
        }

        public SqlTransaction GetTransaction()
        {
            return _transaction;
        }
    }
}
