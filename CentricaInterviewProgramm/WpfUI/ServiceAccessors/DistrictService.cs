using InternalAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfUI.Controllers
{
    public class DistrictService
    {
        private ServiceClient _httpClient;

        public DistrictService()
        {
            _httpClient = ServiceClient.GetInstance();
        }

        public async Task<int> GetDistrictCount()
        {
            var response = await _httpClient.GetClient().GetAsync("district/getdistrictcount");
            if (response.IsSuccessStatusCode)
            {
                int districtTotalCount = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
                return districtTotalCount;
            }
            return 0;
        }

        public async Task<List<District>> GetAllDistricts()
        {
            var response = await _httpClient.GetClient().GetAsync("/district/GetAllDistricts");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var districts = JsonConvert.DeserializeObject<List<District>>(jsonString);
                return districts;
            }
            return null;
        }
    }
}
