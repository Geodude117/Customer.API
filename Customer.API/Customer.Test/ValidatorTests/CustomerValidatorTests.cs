using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.TestHelper;
using Customer.API.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Tests.ValidatorTests
{
    [TestClass]
    public class CustomerValidatorTests
    {

        private CustomerValidator validator;
        private Models.Customer Customer { get; set; }

        public CustomerValidatorTests()
        {
            validator = new CustomerValidator();
        }

        [TestInitialize]
        public void Initialize_test()
        {
            validator = new CustomerValidator();

            Models.Address Address = new Models.Address
            {
                HouseNo = 1,
                Street = "Test Street",
                Postcode = "DE22 3TN",
                City = "Derby"
            };

            List<Models.ContactInformation> contactInfoList = new List<Models.ContactInformation>();

            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.EmailAddress.ToString(),
                Value = "testemail@yahoo.com"
            };

            contactInfoList.Add(contactInfo);

            Models.Customer Customer = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "John",
                Surename = "James",
                DateOfBirth = "21/11/1992",
                Address = Address,
                ContactInformation = contactInfoList
            };

            this.Customer = Customer;

        }

        [TestMethod]
        public void Should_have_errors_as_contact_information_is_null()
        {
            Customer.ContactInformation = null;

            var result = validator.TestValidate(Customer);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_as_contact_information_as_1_valid_type()
        {
            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_as_customer_model_is_valid()
        {
            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_when_Surname_is_null()
        {
            //UPDATE SURNAME
            Customer.Surename = "";

            var result = validator.TestValidate(Customer);

            var errorList = result.ShouldHaveValidationErrorFor(c => c.Surename);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_when_Date_Of_Birth_is_null()
        {
            //UPDATE DOB
            Customer.DateOfBirth = "";

            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_when_Date_Of_Birth_is_valid_format_01()
        { 
            //UPDATE DOB - FORMAT - DD/MM/YYYY
            Customer.DateOfBirth = "05/05/2020";

            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_when_Date_Of_Birth_is_valid_format_02()
        {
            //UPDATE DOB - FORMAT - YYYY/MM/DD
            Customer.DateOfBirth = "1996/10/17";

            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_when_Date_Of_Birth_is_valid_format_03()
        {
            //UPDATE DOB - FORMAT - DD/MM/YYYY HH:MM
            Customer.DateOfBirth = "05/05/2020 12:12";

            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_when_Date_Of_Birth_is_valid_format_04()
        {
            //UPDATE DOB - FORMAT - YYYY/MM/DD HH:MM
            Customer.DateOfBirth = "2020/05/05 12:12";

            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_when_Date_Of_Birth_is_not_valid_format_01()
        {
            //UPDATE DOB - FORMAT - D/M/YYYY
            Customer.DateOfBirth = "5/5/2020";

            var result = validator.TestValidate(Customer);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_when_Date_Of_Birth_is_not_valid_format_02()
        {
            //UPDATE DOB - FORMAT - YYYY/D/M
            Customer.DateOfBirth = "2020/5/5";

            var result = validator.TestValidate(Customer);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_when_Psotcode_is_not_valid()
        {
            //UPDATE POSTCODE 
            Customer.Address.Postcode = "DWDDWDWDWD";

            var result = validator.TestValidate(Customer);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_when_Psotcode_is_valid()
        {
            //UPDATE POSTCODE 
            Customer.Address.Postcode = "HU5 2SY";

            var result = validator.TestValidate(Customer);

            Assert.IsTrue(result.IsValid);
        }




    }
}
