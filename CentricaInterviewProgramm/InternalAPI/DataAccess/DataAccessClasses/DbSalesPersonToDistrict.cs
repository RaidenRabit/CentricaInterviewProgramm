using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.Models;
using Microsoft.Data.SqlClient;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbSalesPersonToDistrict : IDbSalesPersonToDistrict
    {
        private DbConnection _con = null;
        public DbSalesPersonToDistrict()
        {
            _con = DbConnection.GetInstance();
        }

        public int CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptd)
        {
            string stmt = @"INSERT INTO SalesPersonsToDistrict (DistrictId, SalesPersonId, RelationTypeId)
                            OUTPUT INSERTED.SalesPersonToDistrictId VALUES (@0, @1, @2)";

            using (var cmd = new SqlCommand(stmt, _con.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@0", asptd.DistrictId);
                cmd.Parameters.AddWithValue("@1", asptd.SalesPersonId);
                cmd.Parameters.AddWithValue("@2", asptd.RelationTypeId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return reader.GetInt32(0);
                    }
                    return 0;
                }
            }
        }
    }
}
