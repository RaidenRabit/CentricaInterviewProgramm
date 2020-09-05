using System;
using Microsoft.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using InternalAPI.DataAccess.DataAccessClasses;

namespace APITests
{
    class DatabaseTesting
    {
        public static void ResetDatabase()
        {
            string insertScriptPath = "../../.. /../../../Others/InsertDataScript.sql";
            string createScriptPath = "../../../../Others/CreateTablesScript.sql";
            string insertScript = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, insertScriptPath));
            string createScript = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, createScriptPath));

            DbConnection con = DbConnection.GetInstance();
            using (SqlConnection conn = con.GetConnection())
            {
                Server db = new Server(new ServerConnection(conn));
                db.ConnectionContext.ExecuteNonQuery(createScript);
                db.ConnectionContext.ExecuteNonQuery(insertScript);
            }
        }
    }
}
