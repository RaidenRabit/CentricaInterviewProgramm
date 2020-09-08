using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace InternalAPI.DataAccess.DataAccessClasses
{
    public class DbDistrict : IDbDistrict
    {
        private DbConnection _con = null;
        public DbDistrict()
        {
            _con = DbConnection.GetInstance();
        }

        public int GetDistrictsCount()
        {
            string stmt = @"SELECT COUNT(*) FROM District";
            using (var cmd = new SqlCommand(stmt, _con.GetConnection()))
            {
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

        public List<District> GetAllDistricts()
        {
            string stmt = @"SELECT * FROM District d 
                            FULL JOIN Store s ON d.DistrictId = s.DistrictIdSFK
                            FULL JOIN SalesPersonsToDistrict sptd ON sptd.DistrictIdSPTDFK = d.DistrictId
                            FULL JOIN RelationType rt ON rt.RelationTypeId = sptd.RelationTypeIdSPTDFK
                            FULL JOIN SalesPersons sp ON sp.SalesPersonId = sptd.SalesPersonIdSPTDFK
                            FULL JOIN SalesPersonToStore spts on spts.SalesPersonIdSPTSFK = sp.SalesPersonId
                            WHERE d.DistrictId IS NOT NULL";
            using (var cmd = new SqlCommand(stmt, _con.GetConnection()))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    return MapDistrictRelatedObjects(reader);
                }
            }
        }

        public District GetDistrictDetails(int districtId)
        {
            string stmt = @"SELECT * FROM District d 
                            FULL JOIN Store s ON d.DistrictId = s.DistrictIdSFK
                            FULL JOIN SalesPersonsToDistrict sptd ON sptd.DistrictIdSPTDFK = d.DistrictId
                            FULL JOIN RelationType rt ON rt.RelationTypeId = sptd.RelationTypeIdSPTDFK
                            FULL JOIN SalesPersons sp ON sp.SalesPersonId = sptd.SalesPersonIdSPTDFK
                            WHERE d.DistrictId = @0";
            using (var cmd = new SqlCommand(stmt, _con.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@0", districtId);
                using (var reader = cmd.ExecuteReader())
                {
                    var districts = MapDistrictRelatedObjects(reader);
                    if (districts.Any())
                    {
                        return districts.FirstOrDefault();
                    }
                    return null;
                }
            }
        }


        private List<District> MapDistrictRelatedObjects(SqlDataReader reader)
        {
            var dDistricts = new Dictionary<int, District>();
            var salesPersons = new Dictionary<int, SalesPerson>();
            var stores = new Dictionary<int, Store>();
            var sptdwrs = new Dictionary<int, SalesPersonToDistrictWithRelation>();

            while (reader.Read())
            {
                int colIndex = reader.GetOrdinal("SalesPersonId");
                if (!reader.IsDBNull(colIndex))
                {
                    var salesPerson = new SalesPerson
                    {
                        SalesPersonId = Int32.Parse(reader["SalesPersonId"].ToString()),
                        SalesPersonName = reader["SalesPersonName"].ToString(),
                        Stores = new List<Store>()
                    };
                    salesPersons[salesPerson.SalesPersonId] = salesPerson;
                }
                colIndex = reader.GetOrdinal("StoreId");
                if (!reader.IsDBNull(colIndex))
                {
                    var store = new Store
                    {
                        StoreId = Int32.Parse(reader["StoreId"].ToString()),
                        DistrictId = Int32.Parse(reader["DistrictIdSFK"].ToString()),
                        StoreName = reader["StoreName"].ToString()
                    };
                    stores[store.StoreId] = store;
                }
                colIndex = reader.GetOrdinal("DistrictId");
                if (!reader.IsDBNull(colIndex))
                {
                    var district = new District
                    {
                        DistrictId = Int32.Parse(reader["DistrictId"].ToString()),
                        DistrictName = reader["DistrictName"].ToString(),
                        Spwr = new List<SalesPersonWithRelation>(),
                        Stores = new List<Store>()
                    };
                    dDistricts[district.DistrictId] = district;
                }
                colIndex = reader.GetOrdinal("SalesPersonToDistrictId");
                if (!reader.IsDBNull(colIndex))
                {
                    var sptdwr = new SalesPersonToDistrictWithRelation
                    {
                        SalesPersonToDistrictId = Int32.Parse(reader["SalesPersonToDistrictId"].ToString()),
                        DistrictId = Int32.Parse(reader["DistrictIdSPTDFK"].ToString()),
                        SalesPersonId = Int32.Parse(reader["SalesPersonIdSPTDFK"].ToString()),
                        RelationName = reader["RelationTypeName"].ToString()
                    };
                    sptdwrs[sptdwr.SalesPersonToDistrictId] = sptdwr;
                }
            }

            foreach (var sptdwr in sptdwrs)
            {
                var spwr = new SalesPersonWithRelation
                {
                    SalesPerson = salesPersons[sptdwr.Value.SalesPersonId],
                    RelationName = sptdwr.Value.RelationName
                };
                dDistricts[sptdwr.Value.DistrictId].Spwr.Add(spwr);
            }

            foreach (var store in stores)
            {
                dDistricts[store.Value.DistrictId].Stores.Add(store.Value);
            }

            return dDistricts.Values.ToList();
        }
    }
}
