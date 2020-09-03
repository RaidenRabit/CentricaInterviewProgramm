using System;
using System.Data.SqlClient;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbConnection
    {
        private static DbConnection instance;
        private SqlConnection con = null;

        public static DbConnection GetInstance()
        {
            if (instance == null)
            {
                instance = new DbConnection();
            }
            return instance;
        }

        private DbConnection()
        {
           con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CentricaProgram;Integrated Security=True");
           con.Open();
        }

        #region Connection
        public SqlConnection GetConnection()
        {
            return con;
        }

        public bool CloseConnection()
        {
            try
            {
                con.Close();
                instance = null;
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
