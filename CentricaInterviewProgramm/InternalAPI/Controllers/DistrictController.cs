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

        public DistrictController(IDmDistrict dmDistrict)
        {
            _dmDistrict = dmDistrict;
        }

        [HttpGet]
        [Route("GetDistrictCount")]
        public int GetDistrictCount()
        {
            return _dmDistrict.GetAllDistricts().Count();
        }

        [HttpGet]
        [Route("GetAllDistricts")]
        public List<District> GetAllDistricts()
        {
            return _dmDistrict.GetAllDistricts();
        }
    }
}
