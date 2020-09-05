using System.Linq;
using InternalAPI.DataManagement.IDataManagement;
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
        public int Get()
        {
            return _dmDistrict.GetAllDistricts().Count();
        }
    }
}
