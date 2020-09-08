using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataAccess.IDataAccess
{
    public interface IDbSalesPersonToDistrict
    {
        int CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptdm);
        bool DeleteSalesPersonsToDistrict(RemoveSalesPersonToDistrict rsptd);
    }
}
