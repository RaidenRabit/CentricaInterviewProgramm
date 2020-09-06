using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataManagement.IDataManagement
{
    public interface IDmDistrict
    {
        int CreateDistrict(string name);
        bool DeleteDistrict(int districtId);
        List<District> GetAllDistricts();
        int GetDistrictsCount();
    }
}
