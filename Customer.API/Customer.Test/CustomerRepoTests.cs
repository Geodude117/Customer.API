using System;
using System.Collections.Generic;
using Customer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Tests
{
    [TestClass]

    public class CustomerRepoTests
    {
        [TestMethod]
        public void GetCustomerById()
        {
            Models.Customer Customer = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "Geovan",
                Surename = "Inacay",
                DateOfBirth = "17/10/1996"
            };

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.CustomerRepo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(Customer);

            var mockCustomerRepo = _mockUnitOfWork.Object.CustomerRepo;

            var customerGetResponse = mockCustomerRepo.GetAsync(Guid.NewGuid()).Result;

            Assert.IsNotNull(customerGetResponse);
            Assert.IsInstanceOfType(customerGetResponse, typeof(Models.Customer));
        }

        [TestMethod]
        public void InsertCustomer()
        {
            Models.Customer Customer = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "Arther",
                Surename = "Grace",
                DateOfBirth = "09/05/2000"
            };

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.CustomerRepo.InsertAsync(It.IsAny<Guid>(), Customer)).ReturnsAsync(Customer.Id.Value);

            var mockCustomerRepo = _mockUnitOfWork.Object.CustomerRepo;

            var customerInsertResponse = mockCustomerRepo.InsertAsync(Customer.Id.Value, Customer).Result;

            Assert.IsNotNull(customerInsertResponse);
            Assert.IsInstanceOfType(customerInsertResponse, typeof(Guid?));
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            Models.Customer Customer = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "James",
                Surename = "Jones",
                DateOfBirth = "22/09/1998"
            };

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            bool expectedUpdateResult = true;

            _mockUnitOfWork.Setup(x => x.CustomerRepo.UpdateAsync(Customer)).ReturnsAsync(expectedUpdateResult);

            var mockCustomerRepo = _mockUnitOfWork.Object.CustomerRepo;

            var customerUpdateResponse = mockCustomerRepo.UpdateAsync(Customer).Result;

            Assert.IsNotNull(customerUpdateResponse);
            Assert.IsInstanceOfType(customerUpdateResponse, typeof(bool));
            Assert.AreEqual(customerUpdateResponse, expectedUpdateResult);

        }

    }
}
