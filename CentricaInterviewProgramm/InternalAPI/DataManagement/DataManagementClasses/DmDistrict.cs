using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.DataManagement.IDataManagement;
using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataManagement.DataManagementClasses
{
    public class DmDistrict : IDmDistrict
    {
        private IDbDistrict _dbDistrict;

        public DmDistrict(IDbDistrict dbDistrict)
        {
            _dbDistrict = dbDistrict;
        }

        public int CreateDistrict(string name)
        {
            return _dbDistrict.CreateDistrict(name);
        }

        public bool DeleteDistrict(int districtId)
        {
            return _dbDistrict.DeleteDistrict(districtId) > 0;
        }

        public List<District> GetAllDistricts()
        {
            return _dbDistrict.GetAllDistricts();
        }
    }
}
