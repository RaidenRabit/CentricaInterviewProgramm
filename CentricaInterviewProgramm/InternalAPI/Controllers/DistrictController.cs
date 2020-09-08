using System.Collections.Generic;
using System.Linq;
using System.Net;
using InternalAPI.DataManagement.IDataManagement;
using InternalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistrictController : ControllerBase
    {
        private IDmDistrict _dmDistrict;
        private IDmSalesPersonToDistrict _dmSalesPersonToDistrict;

        public DistrictController(IDmDistrict dmDistrict, IDmSalesPersonToDistrict dmSalesPersonToDistrict)
        {
            _dmDistrict = dmDistrict;
            _dmSalesPersonToDistrict = dmSalesPersonToDistrict;
        }

        [HttpGet]
        [Route("GetDistrictCount")]
        public ApiServiceResponse<int> GetDistrictCount()
        {
            var response = _dmDistrict.GetDistrictsCount();
            if (response == 0)
            {
                return new ApiServiceResponse<int>(HttpStatusCode.NotFound, response);
            }
            return new ApiServiceResponse<int>(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("GetAllDistricts")]
        public ApiServiceResponse<List<District>> GetAllDistricts()
        {
            var result = _dmDistrict.GetAllDistricts();
            return new ApiServiceResponse<List<District>>(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("AddSalesPersonToDistrict")]
        public ApiServiceResponse<string> AddSalesPersonToDistrict([FromBody] AddSalesPersonToDistrictModel asptd)
        {
            string message = "";
            if (asptd.DistrictId == 0)
            {
                message = "DistrictId cannot be 0";
                return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
            }    

            if (asptd.SalesPersonId == 0)
            {
                message = "SalesPersonId cannot be 0";
                return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
            }

            message = _dmSalesPersonToDistrict.CreateSalesPersonToDistrict(asptd);

            if (string.IsNullOrWhiteSpace(message))
            {
                return new ApiServiceResponse<string>(HttpStatusCode.OK);
            }
            return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
        }

        [HttpPost]
        [Route("RemoveSalesPersonsFromDistrict")]
        public ApiServiceResponse<string> DeleteSalesPersonsToDistrict([FromBody] RemoveSalesPersonToDistrict rsptd)
        {
            var message = "";
            if (rsptd.DistrictId == 0)
            {
                message = "DistrictId cannot be 0";
                return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
            }

            if (!rsptd.SalesPersonIds.Any())
            {
                message = "SalesPersonId list cannot be empty";
                return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
            }

            if (rsptd.SalesPersonIds.Contains(0))
            {
                message = "SalesPersonIds list cannot contain 0s";
                return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
            }

            message = _dmSalesPersonToDistrict.DeleteSalesPersonsToDistrict(rsptd);

            if (string.IsNullOrWhiteSpace(message))
            {
                return new ApiServiceResponse<string>(HttpStatusCode.OK);
            }
            return new ApiServiceResponse<string>(HttpStatusCode.BadRequest, message);
        }

    }
}
