using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.TestHelper;
using Customer.API.Validators;
using System;
using System.Collections.Generic;

namespace Customer.Tests.ValidatorTests
{
    [TestClass]

    public class ContactInformationValidatorTests
    {
        private ContactInformationValidator validator;
        public ContactInformationValidatorTests()
        {
            validator = new ContactInformationValidator();
        }

        [TestInitialize]
        public void Initialize_test()
        {
            validator = new ContactInformationValidator();
        }

        [TestMethod]
        public void Should_have_no_errors_as_contact_Info_model_is_has_valid_email_address_type()
        {
            //SINGLE VALID CONTACT INFO WITH EMAIL ADDRESS TYPE
            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.EmailAddress.ToString(),
                Value = "testemail@yahoo.com"
            };

            var result = validator.TestValidate(contactInfo);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_no_errors_as_contact_Info_model_is_has_valid_telephone_type()
        {
            //SINGLE VALID CONTACT INFO
            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.Telephone.ToString(),
                Value = "2131313131"
            };

            var result = validator.TestValidate(contactInfo);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_as_contact_Info_model_has_invalid_email_address_type()
        {
            //SINGLE INVALID CONTACT INFO 
            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = "EMAILAD",
                Value = "testemail2@yahoo.com"
            };

            var result = validator.TestValidate(contactInfo);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_as_contact_Info_model_has_invalid_telephone_type()
        {
            //SINGLE INVALID CONTACT INFO
            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = "Phone",
                Value = "2131313131"
            };

            var result = validator.TestValidate(contactInfo);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_as_contact_Info_model_has_invalid_telephone_value()
        {
            //SINGLE INVALID CONTACT INFO 
            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.Telephone.ToString(),
                Value = "dwdwdwddwdwdd"
            };

            var result = validator.TestValidate(contactInfo);

            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void Should_have_errors_as_contact_Info_model_has_invalid_email_address_value()
        {
            //SINGLE INVALID CONTACT INFO 
            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.EmailAddress.ToString(),
                Value = "testemail2@@@@@@yahoo.com.com"
            };

            var result = validator.TestValidate(contactInfo);

            Assert.IsFalse(result.IsValid);
        }
    }
}
