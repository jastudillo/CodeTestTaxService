using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax.Models.Models;
using Tax.Services.Services;
using Tax.Services.Services.Interfaces;
using Tax.UnitTests.Setup;

namespace Tax.UnitTests.Services
{
    [TestClass]
    public class TaxForLocationTest
    {
        private ITaxCalculator taxCalculator;
        [TestInitialize]
        public void InitializeTest()
        {
            var mapper = new AutoMapperSetup().CreateAutoMapperConfig();
            var configuration = new ConfigurationSetup().CreateConfiguration();
            taxCalculator = new TaxCalculatorTaxJarApi(configuration, mapper);
        }

        [TestMethod]
        public async Task Test_Valid_Result_US_90405_5656()
        {
            var location = new Location
            {
                Zip = "90405-5656",
                Country = "US"
            };
            var result = await taxCalculator.GetTaxRateForLocation(location);

            Assert.IsTrue(result.Result == (decimal)0.1025);
        }

        

        [TestMethod]
        public async Task Test_InValid_Result_US_90405_5656_Rounding()
        {
            var location = new Location
            {
                Zip = "90405-5656",
                Country = "US"
            };
            var result = await taxCalculator.GetTaxRateForLocation(location);

            Assert.IsFalse(result.Result == (decimal)0.10);
        }

        [TestMethod]
        public async Task Test_InValid_Result_US_90405_5656()
        {
            var location = new Location
            {
                Zip = "90405-5656",
                Country = "US"
            };
            var result = await taxCalculator.GetTaxRateForLocation(location);

            Assert.IsFalse(result.Result == (decimal)0.5630);
        }
    }
}
