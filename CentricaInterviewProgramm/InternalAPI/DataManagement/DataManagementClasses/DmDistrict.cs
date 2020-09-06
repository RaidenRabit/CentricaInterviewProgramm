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

        public List<District> GetAllDistricts()
        {
            return _dbDistrict.GetAllDistricts();
        }

        public int GetDistrictsCount()
        {
            return _dbDistrict.GetDistrictsCount();
        }
    }
}
