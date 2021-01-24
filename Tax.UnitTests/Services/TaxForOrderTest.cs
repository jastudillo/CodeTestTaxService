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
    public class TaxForOrderTest
    {
        private ITaxCalculator taxCalculator;

        [TestInitialize]
        public void InitializeTest()
        {
            var mapper = new AutoMapperSetup().CreateAutoMapperConfig();
            var configuration = new ConfigurationSetup().CreateConfiguration();
            taxCalculator = new TaxCalculatorTaxJarApi(configuration, mapper);
        }

        [TestCleanup]
        public void Cleanup()
        {
            //add code for clean up if needed
        }

        [TestMethod]
        public async Task Test_Valid_Result_US_90405_5656()
        {
            var order = new Order
            {
                FromState = "CA",
                FromZip = "92093",
                FromCountry ="US",
                ToCountry ="US",
                ToZip = "90002",
                ToState="CA",
                Shipping = (decimal)481111,
                Amount = 550
            };
            var result = await taxCalculator.GetTaxForOrder(order);

            Assert.IsTrue(result.Result == (decimal)56.38);
        }

        

        [TestMethod]
        public async Task Test_InValid_Result_US_90405_5656_Rounding()
        {
            var order = new Order
            {
                FromState = "CA",
                FromZip = "92093",
                FromCountry = "US",
                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                Shipping = (decimal)1.5,
                Amount = 2
            };
            var result = await taxCalculator.GetTaxForOrder(order);

            Assert.IsFalse(result.Result == (decimal)56.38);
        }

        [TestMethod]
        public async Task Test_InValid_Result_US_from_92093()
        {
            var order = new Order
            {
                FromState = "CA",
                FromZip = "92093",
                FromCountry = "US",
                ToCountry = "US",
                ToZip = "90002",
                ToState = "CA",
                Shipping = (decimal)1.5,
                Amount = 2
            };
            var result = await taxCalculator.GetTaxForOrder(order);

            Assert.IsFalse(result.Result == (decimal)0.5630);
        }
    }
}
