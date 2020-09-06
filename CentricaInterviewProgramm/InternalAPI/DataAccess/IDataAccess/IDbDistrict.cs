
using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataAccess.IDataAccess
{
    public interface IDbDistrict
    {
        int CreateDistrict(string name);
        int DeleteDistrict(int districtId);
        List<District> GetAllDistricts();
    }
}
