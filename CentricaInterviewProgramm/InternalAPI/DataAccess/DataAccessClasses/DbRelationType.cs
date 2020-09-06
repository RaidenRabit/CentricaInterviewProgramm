using InternalAPI.DataAccess.DataAccessClasses;
using InternalAPI.Models;
using Microsoft.Data.SqlClient;
using System;

namespace InternalAPI.DataAccess.IDataAccess
{
    public class DbRelationType : IDbRelationType
    {
        private DbConnection _con = null;
        public DbRelationType()
        {
            _con = DbConnection.GetInstance();
        }
        public RelationType GetRelationTypeById(int relationTypeId)
        {
            string stmt = @"SELECT TOP 1 * FROM RelationType WHERE RelationTypeId = @0";
            using (var cmd = new SqlCommand(stmt, _con.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@0", relationTypeId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        int colIndex = reader.GetOrdinal("RelationTypeId");
                        if (!reader.IsDBNull(colIndex))
                        {
                            var relationType = new RelationType
                            {
                                RelationTypeId = Int32.Parse(reader["RelationTypeId"].ToString()),
                                RelationTypeName = reader["RelationTypeName"].ToString()
                            };

                            return relationType;
                        }
                    }
                    return null;
                }
            }
        }
    }
}
