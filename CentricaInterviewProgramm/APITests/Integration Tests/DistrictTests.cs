using APITests.Integration_Tests;
using InternalAPI.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APITests
{
    public class DistrictTests : IntegrationTestBase
    {

        [SetUp]
        public void Setup()
        {
            DatabaseTesting.ResetDatabase();
        }

        [Test]
        public async Task GetDistrictCount()
        {
            //act
            var response = await _internalClient.GetAsync("/district/GetDistrictCount");
            var jsonString = await response.Content.ReadAsStringAsync();
            var districtTotalCount = JsonConvert.DeserializeObject<ApiServiceResponse<int>>(jsonString);
            //assert
            Assert.AreEqual(3, districtTotalCount.Content);
        }

        [Test]
        public async Task GetAllDistricts()
        {
            //act
            var response = await _internalClient.GetAsync("/district/GetAllDistricts");
            var jsonString = await response.Content.ReadAsStringAsync();
            var districts = JsonConvert.DeserializeObject<ApiServiceResponse<List<District>>>(jsonString);
            //assert
            Assert.AreEqual(3, districts.Content.Count, "Actual count:" + districts.Content.Count);
            Assert.AreEqual(0, districts.Content[2].Stores.Count);
        }

        [Test]
        [TestCase(0, 1, 2, HttpStatusCode.BadRequest)] //fails, 0's are not accepted
        [TestCase(1, 0, 2, HttpStatusCode.BadRequest)]
        [TestCase(1, 3, 0, HttpStatusCode.BadRequest)]
        [TestCase(4, 3, 1, HttpStatusCode.BadRequest)] //fails, district 4 doesn't exist
        [TestCase(3, 5, 2, HttpStatusCode.BadRequest)] //fails, salesperson 5 doesn't exist
        [TestCase(3, 3, 3, HttpStatusCode.BadRequest)] // relationType 3 doesn't exist
        [TestCase(1, 1, 2, HttpStatusCode.BadRequest)] //fails, district 1 already has person 1
        [TestCase(1, 3, 1, HttpStatusCode.BadRequest)] //fails, district 1 already has a priamry
        [TestCase(1, 3, 2, HttpStatusCode.OK)] //succedes
        [TestCase(3, 3, 1, HttpStatusCode.OK)] //succedes
        public async Task AddSalesPersonToDistrict(int districtId, int salesPersonId, int relationTypeId, HttpStatusCode expectedResult)
        {
            //arrange
            var asptd = new AddSalesPersonToDistrictModel
            {
                DistrictId = districtId,
                SalesPersonId = salesPersonId,
                RelationTypeId = relationTypeId
            };
            string json = JsonConvert.SerializeObject(asptd);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //act
            var response = await _internalClient.PostAsync("/district/AddSalesPersonToDistrict", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ApiServiceResponse<string>>(jsonString);

            //assert
            Assert.AreEqual(expectedResult, actualResult.HttpResponse);
        }

        [Test]
        [TestCase(0, new[] { 1, 2}, HttpStatusCode.BadRequest)] //fails, districtId invalid
        [TestCase(4, new[] { 1, 2 }, HttpStatusCode.BadRequest)] //fails, districtId non-existent
        [TestCase(1, new[] { 0 }, HttpStatusCode.BadRequest)] //fails, salesPersonId invalid
        [TestCase(1, new[] { 5 }, HttpStatusCode.BadRequest)] //failse, salesPersonId non-existent
        [TestCase(1, new[] { 1, 5 }, HttpStatusCode.BadRequest)] //fails, fails because 1 salesPersonId does not exist
        [TestCase(1, new[] { 3 }, HttpStatusCode.OK)] //success, sp 3 not related to district
        [TestCase(1, new[] { 1, 3 }, HttpStatusCode.OK)] //success, sp 3 not related to d 1 but sp 1 gets deleted
        [TestCase(3, new[] { 1, 2 }, HttpStatusCode.OK)] //success, nothing gets deleted
        [TestCase(1, new[] { 1, 2 }, HttpStatusCode.OK)] //success
        public async Task RemoveSalesPersonFromDistrict(int districtId, int[] salesPersonIds, HttpStatusCode expectedResult)
        {
            //arrange
            var removeSalesPersonToDistrict = new RemoveSalesPersonToDistrict
            {
                DistrictId = districtId,
                SalesPersonIds = salesPersonIds.ToList()
            };
            string json = JsonConvert.SerializeObject(removeSalesPersonToDistrict);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //act
            var response = await _internalClient.PostAsync("/district/RemoveSalesPersonsFromDistrict", content);
            var jsonString = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ApiServiceResponse<string>>(jsonString);

            //assert
            Assert.AreEqual(expectedResult, actualResult.HttpResponse);
        }
    }
}