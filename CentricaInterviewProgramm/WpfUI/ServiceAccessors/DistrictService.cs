using InternalAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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

        public async Task<ApiServiceResponse<int>> GetDistrictCount()
        {
            var codedResponse = await _httpClient.GetClient().GetAsync("district/getdistrictcount");
            var jsonString = await codedResponse.Content.ReadAsStringAsync();
            var decodedResponse = JsonConvert.DeserializeObject<ApiServiceResponse<int>>(jsonString);
            return decodedResponse;
        }

        public async Task<ApiServiceResponse<List<District>>> GetAllDistricts()
        {
            var response = await _httpClient.GetClient().GetAsync("/district/GetAllDistricts");
            var jsonString = await response.Content.ReadAsStringAsync();
            var districts = JsonConvert.DeserializeObject<ApiServiceResponse<List<District>>>(jsonString);
            return districts;
        }

        public async Task<ApiServiceResponse<string>> CreateSalesPersonToDistrict(int districtId, int salesPersonId, int relationTypeId)
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
            var jsonString = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ApiServiceResponse<string>>(jsonString);

            return actualResult;
        }

        public async Task<ApiServiceResponse<string>> RemoveSalesPersonsFromDistrict(List<int> salesPersonIds, int districtId)
        {
            var removeSalesPersonToDistrict = new RemoveSalesPersonToDistrict
            {
                DistrictId = districtId,
                SalesPersonIds = salesPersonIds
            };

            string json = JsonConvert.SerializeObject(removeSalesPersonToDistrict);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await _httpClient.GetClient().PostAsync("/district/RemoveSalesPersonsFromDistrict", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ApiServiceResponse<string>>(jsonString);

            return actualResult;
        }
    }
}
