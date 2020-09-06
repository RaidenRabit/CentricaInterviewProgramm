using InternalAPI.Models;

namespace InternalAPI.DataAccess.IDataAccess
{
    public interface IDbSalesPersonToDistrict
    {
        int CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptdm);
    }
}
