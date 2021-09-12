using System;
using System.Collections.Generic;
using System.Linq;
using Customer.Repository;
using Customer.Repository.Address;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Tests.RepositoryTests
{
    [TestClass]
    public class AddressRepoTests
    {
        [TestMethod]
        public void Get_all_address()
        {
            List<Models.Address> addresses = new List<Models.Address>();

            Models.Address Address1 = new Models.Address
            {
                HouseNo = 1,
                Street = "Street 1",
                Postcode = "HU5 123",
                City = "Hull"
            };

            Models.Address Address2 = new Models.Address
            {
                HouseNo = 1,
                Street = "Street 2",
                Postcode = "HU5 456",
                City = "Hull"
            };

            Models.Address Address3 = new Models.Address
            {
                HouseNo = 1,
                Street = "Street 3",
                Postcode = "HU5 789",
                City = "Hull"
            };

            addresses.Add(Address1);
            addresses.Add(Address2);
            addresses.Add(Address3);

            var expectedAddressCount = addresses.Count;

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.AddressRepo.GetAllAsync()).ReturnsAsync(addresses);

            var mockAddressRepo = _mockUnitOfWork.Object.AddressRepo;

            IEnumerable<Models.Address> testAddresses = mockAddressRepo.GetAllAsync().Result;

            Assert.IsNotNull(testAddresses);
            Assert.AreEqual(expectedAddressCount, testAddresses.Count()); 
        }

        [TestMethod]
        public void Get_address_by_id()
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
        public void Insert_address()
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
        public void Update_address()
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
