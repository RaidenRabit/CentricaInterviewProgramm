
using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataAccess.IDataAccess
{
    public interface IDbDistrict
    {
        List<District> GetAllDistricts();
        int GetDistrictsCount();
        District GetDistrictDetails(int districtId);
    }
}
