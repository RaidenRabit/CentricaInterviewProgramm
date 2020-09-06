using InternalAPI.Models;

namespace InternalAPI.DataAccess.IDataAccess
{
    public interface IDbSalesPerson
    {
        SalesPerson GetSalesPersonById(int salesPersonId);
    }
}
