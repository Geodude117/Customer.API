using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customer.API.Controllers;
using Customer.Business.Customer;
using Microsoft.Extensions.Configuration;
using Customer.Repository;
using System;
using System.Collections.Generic;

namespace Customer.Test
{
    [TestClass]
    public class CustomerTests
    {

        private IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        private Guid _guid;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = new UnitOfWork("server=(localdb)\\MSSQLLocalDB;database=Customer;Integrated Security=SSPI;");
            _guid = Guid.NewGuid();
        }

        [TestMethod]
        public void InsertCustomerFromBusinessLayer()
        {
            CustomerBusiness customerBusiness = new CustomerBusiness(_unitOfWork, _configuration);

            Models.Customer Customer = new Models.Customer();
            Models.Address Address = new Models.Address();
            List<Models.ContactInformation> ContactInfoList = new List<Models.ContactInformation>();

            Customer.Id = _guid;
            Customer.ForeName = "Geovan";
            Customer.Surename = "Inacay";
            Customer.DateOfBirth = "17/10/1996";

            Address.HouseNo = 1;
            Address.Street = "Test street";
            Address.Postcode = "123 456";
            Address.City = "Hull";

            Models.ContactInformation email = new Models.ContactInformation();
            email.Type = "emailaddress";
            email.Value = "geovaninacay@yahoo.com";

            ContactInfoList.Add(email);

            Customer.Address = Address;
            Customer.ContactInformation = ContactInfoList;

            var result = customerBusiness.InsertAsync(Customer).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Guid?));
        }

        [TestMethod]
        public void GetCustomerFromBusinessLayer()
        {
            CustomerBusiness customerBusiness = new CustomerBusiness(_unitOfWork, _configuration);

            var customer = customerBusiness.GetAsync(_guid).Result;

            Assert.IsNotNull(customer);
            Assert.IsInstanceOfType(customer, typeof(Models.Customer));
        }


    }
}
