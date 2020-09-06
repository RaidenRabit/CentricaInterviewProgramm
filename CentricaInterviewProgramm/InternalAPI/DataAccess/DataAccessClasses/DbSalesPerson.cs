using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.Models;
using Microsoft.Data.SqlClient;
using System;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbSalesPerson : IDbSalesPerson
    {

        private DbConnection _con = null;
        public DbSalesPerson()
        {
            _con = DbConnection.GetInstance();
        }
        public SalesPerson GetSalesPersonById(int salesPersonId)
        {
            string stmt = @"SELECT TOP 1 * FROM  SalesPersons WHERE  SalesPersonId = @0";
            using (var cmd = new SqlCommand(stmt, _con.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@0", salesPersonId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int colIndex = reader.GetOrdinal("SalesPersonId");
                        if (!reader.IsDBNull(colIndex))
                        {
                            return new SalesPerson
                            {
                                SalesPersonId = Int32.Parse(reader["SalesPersonId"].ToString()),
                                SalesPersonName = reader["SalesPersonName"].ToString()
                            };
                        }
                    }
                    return null;
                }
            }
        }
    }
}
