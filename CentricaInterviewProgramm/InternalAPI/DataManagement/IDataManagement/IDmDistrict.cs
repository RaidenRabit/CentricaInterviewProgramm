using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataManagement.IDataManagement
{
    public interface IDmDistrict
    {
        List<District> GetAllDistricts();
        int GetDistrictsCount();
    }
}
