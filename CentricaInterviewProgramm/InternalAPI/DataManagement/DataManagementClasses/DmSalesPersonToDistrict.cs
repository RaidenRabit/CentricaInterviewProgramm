using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.DataManagement.IDataManagement;
using InternalAPI.Models;

namespace InternalAPI.DataManagement.DataManagementClasses
{
    public class DmSalesPersonToDistrict : IDmSalesPersonToDistrict
    {
        private IDbSalesPerson _dbSalesPerson;
        private IDbDistrict _dbDistrict;
        private IDbSalesPersonToDistrict _dbSalesPersonToDistrict;
        private IDbRelationType _dbRelationType;

        public DmSalesPersonToDistrict
            (IDbSalesPerson dbSalesPerson, 
            IDbDistrict dbDistrict, 
            IDbSalesPersonToDistrict dbSalesPersonToDistrict,
            IDbRelationType dbRelationType)
        {
            _dbSalesPerson = dbSalesPerson;
            _dbDistrict = dbDistrict;
            _dbSalesPersonToDistrict = dbSalesPersonToDistrict;
            _dbRelationType = dbRelationType;
        }

        public bool CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptd)
        {
            var district = _dbDistrict.GetDistrictDetails(asptd.DistrictId);
            if (district == null)
            {
                return false;
            }

            var salesPerson = _dbSalesPerson.GetSalesPersonById(asptd.SalesPersonId);
            if (salesPerson == null)
            {
                return false;
            }

            var relationType = _dbRelationType.GetRelationTypeById(asptd.RelationTypeId);
            if (relationType == null)
            {
                return false;
            }

            foreach (var salesPersonRelation in district.Spwr)
            {
                if (salesPersonRelation.SalesPerson.SalesPersonId == asptd.SalesPersonId)
                {
                    return false;
                }

                if (salesPersonRelation.RelationName.Equals("Primary") && asptd.RelationTypeId == 1)
                {
                    return false;
                }
            }

            if (_dbSalesPersonToDistrict.CreateSalesPersonToDistrict(asptd) != 0)
            { 
                return true; 
            }
            return false;
        }
    }
}
