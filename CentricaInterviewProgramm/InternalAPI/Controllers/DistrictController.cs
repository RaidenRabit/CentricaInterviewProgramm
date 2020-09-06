using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public int GetDistrictCount()
        {
            return _dmDistrict.GetDistrictsCount();
        }

        [HttpGet]
        [Route("GetAllDistricts")]
        public List<District> GetAllDistricts()
        {
            var result = _dmDistrict.GetAllDistricts();
            return result;
        }

        [HttpPost]
        [Route("AddSalesPersonToDistrict")]
        public bool AddSalesPersonToDistrict([FromBody] AddSalesPersonToDistrictModel asptd)
        {
            if (asptd.DistrictId == 0)
            {
                return false;
            }    

            if (asptd.SalesPersonId == 0)
            {
                return false;
            }

            if (asptd.DistrictId == 0)
            {
                return false;
            }

            return _dmSalesPersonToDistrict.CreateSalesPersonToDistrict(asptd);
        }

    }
}
