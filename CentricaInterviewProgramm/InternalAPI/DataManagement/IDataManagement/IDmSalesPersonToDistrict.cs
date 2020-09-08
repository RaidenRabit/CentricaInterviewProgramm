using InternalAPI.Models;

namespace InternalAPI.DataManagement.IDataManagement
{
    public interface IDmSalesPersonToDistrict
    {
        string CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptd);
        string DeleteSalesPersonsToDistrict(RemoveSalesPersonToDistrict rsptd);
    }
}
