using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax.Models.Models;
using Tax.Services.Validator;

namespace Tax.UnitTests.Validator
{
    [TestClass]
    public class LocationValidationTest
    {
        [TestMethod]
        public void Zip_Required_Test()
        {
            var location = new Location();
            var message = string.Empty;
            var isValid = location.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "Zip is required");
        }

        [TestMethod]
        public void Zip_Valid_Test()
        {
            var location = new Location { Zip = "90405-5656" };
            var message = string.Empty;
            var isValid = location.IsValid(out message);

            Assert.IsTrue(isValid == true);
            Assert.IsTrue(message == "Valid");
        }

        [TestMethod]
        public void Country_Number_Test()
        {
            var location = new Location { Zip = "90405-5656", Country="12" };
            var message = string.Empty;
            var isValid = location.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "Country must be 2 letter ISO");
        }

        [TestMethod]
        public void Country_Valid_Test()
        {
            var location = new Location { Zip = "90405-5656", Country = "US" };
            var message = string.Empty;
            var isValid = location.IsValid(out message);

            Assert.IsTrue(isValid == true);
            Assert.IsTrue(message == "Valid");
        }

        [TestMethod]
        public void Country_Invalid_No_US_Location_Test()
        {
            var location = new Location { Zip = "ABCDE"};
            var message = string.Empty;
            var isValid = location.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "Country is required for non US Locations");
        }


        [TestMethod]
        public void Country_Valid_No_US_Location_Test()
        {
            var location = new Location { Zip = "ABCDE" , Country ="CA"};
            var message = string.Empty;
            var isValid = location.IsValid(out message);

            Assert.IsTrue(isValid == true);
            Assert.IsTrue(message == "Valid");
        }

    }
}
