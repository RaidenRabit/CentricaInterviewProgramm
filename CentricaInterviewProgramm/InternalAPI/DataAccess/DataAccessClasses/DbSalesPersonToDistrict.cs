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
            string stmt = @"INSERT INTO SalesPersonsToDistrict (DistrictIdSPTDFK, SalesPersonIdSPTDFK, RelationTypeIdSPTDFK)
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

        public bool DeleteSalesPersonsToDistrict(RemoveSalesPersonToDistrict rsptd)
        {
            string stmt = @"DELETE FROM SalesPersonsToDistrict WHERE SalesPersonIdSPTDFK = 3 AND DistrictIdSPTDFK = 1";
            try
            {
                _con.BeginTransaction();
                foreach (var salesPersonId in rsptd.SalesPersonIds)
                {
                    using (var cmd = new SqlCommand(stmt, _con.GetConnection(), _con.GetTransaction()))
                    {
                        cmd.Parameters.AddWithValue("@0", salesPersonId);
                        cmd.Parameters.AddWithValue("@1", rsptd.DistrictId);
                        cmd.ExecuteNonQuery();
                    }
                }
                _con.GetTransaction().Commit();
                return true;
            }
            catch(SqlException ex)
            {
                _con.GetTransaction().Rollback();
                return false;
            }
        }
    }
}
