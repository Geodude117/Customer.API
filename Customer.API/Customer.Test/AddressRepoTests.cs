using System;
using Customer.Repository;
using Customer.Repository.Address;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Tests
{
    [TestClass]
    public class AddressRepoTests
    {
       
        [TestMethod]
        public void GetAddressById()
        {
            Models.Address Address = new Models.Address
            {
                HouseNo = 1,
                Street = "Test street",
                Postcode = "123 456",
                City = "Hull"
            };

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.AddressRepo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(Address);

            var mockAddressRepo = _mockUnitOfWork.Object.AddressRepo;

            var addressGetResult = mockAddressRepo.GetAsync(Guid.NewGuid()).Result;

            Assert.IsNotNull(addressGetResult);
            Assert.IsInstanceOfType(addressGetResult, typeof(Models.Address));
        }

        [TestMethod]
        public void InsertAddress()
        {
            Models.Address Address = new Models.Address
            {
                HouseNo = 1,
                Street = "Test street",
                Postcode = "123 456",
                City = "Hull"
            };

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            Guid? _guid = Guid.NewGuid();

            _mockUnitOfWork.Setup(x => x.AddressRepo.InsertAsync(_guid, Address)).ReturnsAsync(_guid);

            var mockAddressRepo = _mockUnitOfWork.Object.AddressRepo;

            var addressInsertResult = mockAddressRepo.InsertAsync(_guid, Address).Result;

            Assert.IsNotNull(addressInsertResult);
            Assert.IsInstanceOfType(addressInsertResult, typeof(Guid?));
            Assert.AreEqual(addressInsertResult, _guid);

        }

        [TestMethod]
        public void UpdateAddress()
        {
            Models.Address Address = new Models.Address
            {
                HouseNo = 1,
                Street = "Test street",
                Postcode = "123 456",
                City = "Hull"
            };

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            bool expectedUpdateResult = true;

            _mockUnitOfWork.Setup(x => x.AddressRepo.UpdateAsync(Address)).ReturnsAsync(expectedUpdateResult);

            var mockAddressRepo = _mockUnitOfWork.Object.AddressRepo;

            var addressUpdateResult = mockAddressRepo.UpdateAsync(Address).Result;

            Assert.IsNotNull(addressUpdateResult);
            Assert.IsInstanceOfType(addressUpdateResult, typeof(bool));
            Assert.AreEqual(addressUpdateResult, expectedUpdateResult);

        }

    }
}
