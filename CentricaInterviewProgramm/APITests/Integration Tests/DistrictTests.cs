using APITests.Integration_Tests;
using Newtonsoft.Json;
using NUnit.Framework;
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
        public async Task GetAllDistricts()
        {
            //act
            var response = await _internalClient.GetAsync("/District");
            int districtTotalCount = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());
            //assert
            Assert.AreEqual(districtTotalCount, 3);
        }
    }
}