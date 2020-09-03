using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbDistrict : IDbDistrict
    {
        private DbConnection con = null;
        public DbDistrict()
        {
            con = DbConnection.GetInstance();
        }

        public int CreateDistrict(string name)
        {
            string stmt = "INSERT INTO District (Name) OUTPUT INSERTED.Id VALUES(@0);";

            using (SqlCommand cmd = new SqlCommand(stmt, con.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@0", name);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }

        public int DeleteDistrict(int districtId)
        {
            string stmt = "DELETE FROM District WHERE Id = @0; ";
            using (SqlCommand cmd = new SqlCommand(stmt, con.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@0", districtId);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<District> GetAllDistricts()
        {
            string stmt = "SELECT * FROM District";
            List<District> districts = new List<District>();

            using (SqlCommand cmd = new SqlCommand(stmt, con.GetConnection()))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    District district = null;
                    while (reader.Read())
                    {
                        district = new District
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString()
                        };
                        districts.Add(district);
                    }
                }
            }
            return districts;
        }
    }
}
