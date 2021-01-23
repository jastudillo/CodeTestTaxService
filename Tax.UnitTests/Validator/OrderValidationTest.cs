using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tax.Models.Models;
using Tax.Services.Validator;
namespace Tax.UnitTests.Validator
{
    [TestClass]
    public class OrderValidationTest
    {
        [TestMethod]
        public void ToCountry_Required_Test()
        {
            var order = new Order();
            var message = string.Empty;
            var isValid = order.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "To Country is required");
        }

        [TestMethod]
        public void ToCountry_Must_be_2_Letter_ISO_Test()
        {
            var order = new Order
            {
                ToCountry ="12"
            };
            var message = string.Empty;
            var isValid = order.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "To Country must be 2 letter ISO");
        }

        [TestMethod]
        public void ToZip_Required_When_US_Test()
        {
            var order = new Order
            {
                ToCountry = "US"
            };
            var message = string.Empty;
            var isValid = order.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "To Zip is required when To Country is US");
        }

        [TestMethod]
        public void ToState_Required_When_US_Test()
        {
            var order = new Order
            {
                ToCountry = "US",
                ToZip="33021"

            };
            var message = string.Empty;
            var isValid = order.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "To State is required when To Country is US");
        }

        [TestMethod]
        public void ToState_Must_Be_2_letter_ISO_Test()
        {
            var order = new Order
            {
                ToCountry = "US",
                ToZip = "33021",
                ToState = "12"


            };
            var message = string.Empty;
            var isValid = order.IsValid(out message);

            Assert.IsTrue(isValid == false);
            Assert.IsTrue(message == "To State must be a 2 letter ISO");
        }


        [TestMethod]
        public void Valid_Test()
        {
            var order = new Order
            {
                ToCountry = "US",
                ToZip = "33021",
                ToState = "FL",
                Shipping = 200000
                

            };
            var message = string.Empty;
            var isValid = order.IsValid(out message);

            Assert.IsTrue(isValid == true);
            Assert.IsTrue(message == "Valid");
        }
    }
}
