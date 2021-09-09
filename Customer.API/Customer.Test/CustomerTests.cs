using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customer.API.Controllers;
using Customer.Business.Customer;
using Microsoft.Extensions.Configuration;
using Customer.Repository;
using System;

namespace Customer.Test
{
    [TestClass]
    public class CustomerTests
    {

        private IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;


        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = new UnitOfWork("server=(localdb)\\MSSQLLocalDB;database=Customer;Integrated Security=SSPI;");

        }

        [TestMethod]
        public void GetCustomerFromBusinessLayer()
        {
            CustomerBusiness customerBusiness = new CustomerBusiness(_unitOfWork, _configuration);

            var customer = customerBusiness.GetAsync(Guid.Parse("1FA85F64-5717-4562-B3FC-2C963F66AFA6")).Result;

            Assert.IsNotNull(customer);

        }
    }
}
