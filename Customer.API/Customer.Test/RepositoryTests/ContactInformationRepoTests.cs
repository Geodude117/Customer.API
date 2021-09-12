using System;
using System.Collections.Generic;
using System.Linq;
using Customer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Tests.RepositoryTests
{
    [TestClass]
    public class ContactInformationRepoTests
    {
        [TestMethod]
        public void GetAllContactInformation()
        {
            List<Models.ContactInformation> contactInfoList = new List<Models.ContactInformation>();

            Models.ContactInformation contactInfo1 = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.emailaddress.ToString().ToLower(),
                Value = "geovaninacay@yahoo.com"
            };

            Models.ContactInformation contactInfo2 = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.emailaddress.ToString().ToLower(),
                Value = "test@yahoo.com"
            };

            contactInfoList.Add(contactInfo1);
            contactInfoList.Add(contactInfo2);

            int expectedContactInfoCount = contactInfoList.Count;

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.ContactInformationRepo.GetAllAsync()).ReturnsAsync(contactInfoList);

            var mockContactInfoRepo = _mockUnitOfWork.Object.ContactInformationRepo;

            var contactInforGetAllResult = mockContactInfoRepo.GetAllAsync().Result;

            Assert.IsNotNull(contactInforGetAllResult);
            Assert.AreEqual(contactInforGetAllResult.Count(), expectedContactInfoCount);
        }

        [TestMethod]
        public void GetContactInformationById()
        {
            List<Models.ContactInformation> contactInfoList = new List<Models.ContactInformation>();

            Models.ContactInformation contactInfo1 = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.emailaddress.ToString().ToLower(),
                Value = "geovaninacay@yahoo.com"
            };

            contactInfoList.Add(contactInfo1);

            int expectedContactInfoCount = contactInfoList.Count;

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.ContactInformationRepo.GetAllForCustomerId(It.IsAny<Guid>())).ReturnsAsync(contactInfoList);

            var mockContactInfoRepo = _mockUnitOfWork.Object.ContactInformationRepo;

            var contactInforGetResult = mockContactInfoRepo.GetAllForCustomerId(Guid.NewGuid()).Result;

            Assert.IsNotNull(contactInforGetResult);
            Assert.AreEqual(contactInforGetResult.Count(), expectedContactInfoCount);
        }
    }
}
