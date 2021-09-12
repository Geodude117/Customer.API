using System;
using System.Collections.Generic;
using System.Linq;
using Customer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Tests
{
    [TestClass]
    public class ContactInformationRepoTests
    {
        [TestMethod]
        public void GetContactInformationById()
        {
            int expectedContactInfoCount = 2;

            List<Models.ContactInformation> contactInfoList = new List<Models.ContactInformation>();

            for (int i = 0; i < expectedContactInfoCount; i++)
            {
                Models.ContactInformation contactInfo = new Models.ContactInformation
                {
                    Type = Models.ContactInfomationType.emailaddress.ToString().ToLower(),
                    Value = "geovaninacay@yahoo.com"
                };

                contactInfoList.Add(contactInfo);
            }

            var _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.Setup(x => x.ContactInformationRepo.Get(It.IsAny<Guid>())).ReturnsAsync(contactInfoList);

            var mockContactInfoRepo = _mockUnitOfWork.Object.ContactInformationRepo;

            var contactInforGetResult = mockContactInfoRepo.Get(Guid.NewGuid()).Result;

            Assert.IsNotNull(contactInforGetResult);
            Assert.IsTrue(contactInforGetResult.Count() == expectedContactInfoCount);

        }
    }
}
