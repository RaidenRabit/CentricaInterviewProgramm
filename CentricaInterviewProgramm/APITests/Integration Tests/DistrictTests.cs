using APITests.Integration_Tests;
using InternalAPI.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
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
            var districtTotalCount = JsonConvert.DeserializeObject<int>(jsonString);
            //assert
            Assert.AreEqual(3, districtTotalCount, "Actual count:" + districtTotalCount);
        }

        [Test]
        public async Task GetAllDistricts()
        {
            //act
            var response = await _internalClient.GetAsync("/district/GetAllDistricts");
            var jsonString = await response.Content.ReadAsStringAsync();
            var districts = JsonConvert.DeserializeObject<List<District>>(jsonString);
            //assert
            Assert.AreEqual(3, districts.Count, "Actual count:" + districts.Count);
            Assert.AreEqual(0, districts[2].Stores.Count, "district " + districts[2].DistrictId + " stores were NOT null");
        }

        [Test]
        [TestCase(0, 1, 2, false)] //fails, 0's are not accepted
        [TestCase(1, 0, 2, false)]
        [TestCase(1, 3, 0, false)]
        [TestCase(4, 3, 1, false)] //fails, district 4 doesn't exist
        [TestCase(3, 5, 2, false)] //fails, salesperson 5 doesn't exist
        [TestCase(3, 3, 3, false)] // relationType 3 doesn't exist
        [TestCase(1, 1, 2, false)] //fails, district 1 already has person 1
        [TestCase(1, 3, 1, false)] //fails, district 1 already has a priamry
        [TestCase(1, 3, 2, true)] //succedes
        [TestCase(3, 3, 1, true)] //succedes
        public async Task AddSalesPersonToDistrict(int districtId, int salesPersonId, int relationTypeId, bool expectedResult)
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
            var actualResult = JsonConvert.DeserializeObject<bool>(jsonString);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}