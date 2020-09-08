using InternalAPI.DataAccess.IDataAccess;
using InternalAPI.DataManagement.IDataManagement;
using InternalAPI.Models;
using System.Collections.Generic;

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

        public string CreateSalesPersonToDistrict(AddSalesPersonToDistrictModel asptd)
        {
            var district = _dbDistrict.GetDistrictDetails(asptd.DistrictId);
            if (district == null)
            {
                return "District not found";
            }

            var salesPerson = _dbSalesPerson.GetSalesPersonById(asptd.SalesPersonId);
            if (salesPerson == null)
            {
                return "SalesPerson not found";
            }

            var relationType = _dbRelationType.GetRelationTypeById(asptd.RelationTypeId);
            if (relationType == null)
            {
                return "RelationType not found";
            }

            foreach (var salesPersonRelation in district.Spwr)
            {
                if (salesPersonRelation.SalesPerson.SalesPersonId == asptd.SalesPersonId)
                {
                    return "SalesPerson already linked to District";
                }

                if (salesPersonRelation.RelationName.Equals("Primary") && asptd.RelationTypeId == 1)
                {
                    return "District can only have 1 Primary SalesPerson";
                }
            }

            if (_dbSalesPersonToDistrict.CreateSalesPersonToDistrict(asptd) != 0)
            { 
                return ""; 
            }
            return "Something went wrong";
        }

        public string DeleteSalesPersonsToDistrict(RemoveSalesPersonToDistrict rsptd)
        {
            var district = _dbDistrict.GetDistrictDetails(rsptd.DistrictId);
            if (district == null)
            {
                return "District not found";
            }

            foreach (var salesPersonId in rsptd.SalesPersonIds)
            {
                var salesPerson = _dbSalesPerson.GetSalesPersonById(salesPersonId);
                if (salesPerson == null)
                {
                    return $"SalesPerson with id:{salesPersonId} not found";
                }
            }

            var response = _dbSalesPersonToDistrict.DeleteSalesPersonsToDistrict(rsptd);
            if (response)
            {
                return "";
            }
            return "Something went wrong";
        }
    }
}
