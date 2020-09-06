using APITests.Integration_Tests;
using InternalAPI.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
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
            var response = await _internalClient.GetAsync("/District/GetDistrictCount");
            var jsonString = await response.Content.ReadAsStringAsync();
            var districtTotalCount = JsonConvert.DeserializeObject<int>(jsonString);
            //assert
            Assert.AreEqual(3, districtTotalCount, "Actual count:" + districtTotalCount);
        }

        [Test]
        public async Task GetAllDistricts()
        {
            //act
            var response = await _internalClient.GetAsync("/District/GetAllDistricts");
            var jsonString = await response.Content.ReadAsStringAsync();
            var districts = JsonConvert.DeserializeObject<List<District>>(jsonString);
            //assert
            Assert.AreEqual(3, districts.Count, "Actual count:" + districts.Count);
        }
    }
}