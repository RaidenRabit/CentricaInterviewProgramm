using InternalAPI.Models;

namespace InternalAPI.DataManagement.IDataManagement
{
    public interface IDmSalesPersonToDistrict
    {
        bool CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptd);
        bool DeleteSalesPersonsToDistrict(RemoveSalesPersonToDistrict rsptd);
    }
}
