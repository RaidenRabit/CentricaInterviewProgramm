using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternalAPI.DataManagement.IDataManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InternalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IDmDistrict _dmDistrict;

        public WeatherForecastController(IDmDistrict dmDistrict)
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
