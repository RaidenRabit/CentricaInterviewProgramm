using InternalAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<bool> CreateSalesPersonToDistrict(int districtId, int salesPersonId, int relationTypeId)
        {

            var asptd = new AddSalesPersonToDistrictModel
            {
                DistrictId = districtId,
                SalesPersonId = salesPersonId,
                RelationTypeId = relationTypeId
            };
            string json = JsonConvert.SerializeObject(asptd);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await _httpClient.GetClient().PostAsync("/district/AddSalesPersonToDistrict", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var actualResult = JsonConvert.DeserializeObject<bool>(jsonString);

                return actualResult;
            }
            return false;
        }
    }
}
