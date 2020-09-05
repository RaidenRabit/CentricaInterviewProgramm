using System;
using Microsoft.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace APITests
{
    class DatabaseTesting
    {
        public static void ResetDatabase()
        {
            string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CentricaProgram;Integrated Security=true;User Id=sa;Password=Password12!";
            string insertScriptPath = "../../.. /../../../Others/InsertDataScript.sql";
            string createScriptPath = "../../../../Others/CreateTablesScript.sql";
            string insertScript = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, insertScriptPath));
            string createScript = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, createScriptPath));

            using (SqlConnection conn = new SqlConnection(connection))
            {
                Server db = new Server(new ServerConnection(conn));
                db.ConnectionContext.ExecuteNonQuery(createScript);
                db.ConnectionContext.ExecuteNonQuery(insertScript);
            }
        }
    }
}
