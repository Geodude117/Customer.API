using Customer.Business.Customer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Tests.BusinessTests
{
    [TestClass]
    public class CustomerBusinessTests
    {
        public readonly ICustomerBusiness MockCustomerBusiness;

        public CustomerBusinessTests()
        {
            List<Models.Customer> customerList = new List<Models.Customer>();

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

            List<Models.ContactInformation> contactInfoList1 = new List<Models.ContactInformation>();
            List<Models.ContactInformation> contactInfoList2 = new List<Models.ContactInformation>();

            Models.ContactInformation contactInfo1 = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.EmailAddress.ToString(),
                Value = "geovaninacay@yahoo.com"
            };

            Models.ContactInformation contactInfo2 = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.EmailAddress.ToString(),
                Value = "test@yahoo.com"
            };

            contactInfoList1.Add(contactInfo1);
            contactInfoList2.Add(contactInfo2);

            Models.Customer Customer1 = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "Geovan",
                Surename = "Inacay",
                DateOfBirth = "17/10/1996",
                Address = Address1,
                ContactInformation = contactInfoList1
            };
            Models.Customer Customer2 = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "James",
                Surename = "Jones",
                DateOfBirth = "10/11/1993",
                Address = Address2,
                ContactInformation = contactInfoList2
            };

            customerList.Add(Customer1);
            customerList.Add(Customer2);

            Mock<ICustomerBusiness> mockCustomerBusiness = new Mock<ICustomerBusiness>();

            //RETURNS ALL CUSTOMERS
            mockCustomerBusiness.Setup(mr => mr.GetAllAsync()).ReturnsAsync(customerList);

            //UPDATE CUSTOMERS
            mockCustomerBusiness.Setup(mr => mr.UpdateAsync(It.IsAny<Models.Customer>())).ReturnsAsync(
              (Models.Customer target) =>
              {
                  var original = customerList.Single(c => c.Id == target.Id);

                  customerList.Remove(original);
                  customerList.Add(target);

                  if (original == null)
                  {
                      return false;
                  }

                  customerList.Add(original);

                  return true;

              });

            //RETURNS ALL CUSTOMERS WITH SEARCH CRITERIA
            mockCustomerBusiness.Setup(mr => mr.SearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(
            (string forename, string surname, string postcode, string email) =>
            {
                List<Models.Customer> customers = new List<Models.Customer>();

                if (forename != null)
                {
                    customers.AddRange(customerList.Where(x => forename.Contains(x.ForeName)).ToList());
                }

                if (surname != null)
                {
                    customers.AddRange(customerList.Where(x => surname.Contains(x.Surename)).ToList());
                }

                if (postcode != null)
                {
                    customers.AddRange(customerList.Where(x => x.Address.Postcode.Contains(postcode)));
                }

                if (email != null)
                {
                    customers.AddRange(customerList.Where(x => x.ContactInformation.Any(x => x.Value.Contains(email))));
                }


                return customers;
            });

            //RETURNS A SINGLE CUSTOMER BY ID
           mockCustomerBusiness.Setup(mr => mr.GetAsync(
                It.IsAny<Guid>())).ReturnsAsync((Guid? i) => customerList.Where(
                x => x.Id == i).FirstOrDefault());

            mockCustomerBusiness.Setup(mr => mr.InsertAsync(It.IsAny<Models.Customer>())).ReturnsAsync(
                (Models.Customer target) =>
                {
                    customerList.Add(target);

                    return target.Id.Value;
                });

            this.MockCustomerBusiness = mockCustomerBusiness.Object;

        }
        [TestMethod]
        public void GetAllCustomers()
        {
            var testCustomers = this.MockCustomerBusiness.GetAllAsync().Result;

            Assert.IsNotNull(testCustomers);
            Assert.IsInstanceOfType(testCustomers, typeof(IEnumerable<Models.Customer>));
            Assert.AreEqual(testCustomers.Count(), 2);
        }

        [TestMethod]
        public void GetCustomersById()
        {
            var testCustomers = this.MockCustomerBusiness.GetAllAsync().Result;

            var singleCustomer = testCustomers.FirstOrDefault();

            var searchSingleCustomerResult = this.MockCustomerBusiness.GetAsync(singleCustomer.Id.Value).Result;

            Assert.IsNotNull(searchSingleCustomerResult);
            Assert.IsInstanceOfType(searchSingleCustomerResult, typeof(Models.Customer));
            Assert.AreEqual(singleCustomer, searchSingleCustomerResult);
        }


        [TestMethod]
        public void CanInsertProduct()
        {
            Models.Address Address = new Models.Address
            {
                HouseNo = 1,
                Street = "Test Street",
                Postcode = "123 123",
                City = "Derby"
            };

            List<Models.ContactInformation> contactInfoList = new List<Models.ContactInformation>();

            Models.ContactInformation contactInfo = new Models.ContactInformation
            {
                Type = Models.ContactInfomationType.EmailAddress.ToString(),
                Value = "testtest@yahoo.com"
            };

            contactInfoList.Add(contactInfo);

            Models.Customer newCustomer = new Models.Customer
            {
                Id = Guid.NewGuid(),
                ForeName = "John",
                Surename = "Proctor",
                DateOfBirth = "21/11/1992",
                Address = Address,
                ContactInformation = contactInfoList
            };
           
            // COUNT CUSTOMER LIST
            int customerCount = this.MockCustomerBusiness.GetAllAsync().Result.Count();
            Assert.AreEqual(2, customerCount);

            //INSERT NEW CUSTOMER 
            var newCustomerId = this.MockCustomerBusiness.InsertAsync(newCustomer).Result;

            //RE COUNT CUSTOMER LIST AFTER UPDATE
            customerCount = this.MockCustomerBusiness.GetAllAsync().Result.Count();
            Assert.AreEqual(3, customerCount); 

            //GET NEW CUSTOMER
            var testCustomer = this.MockCustomerBusiness.GetAsync(newCustomerId.Value).Result;

            Assert.IsNotNull(testCustomer);
            Assert.IsInstanceOfType(testCustomer, typeof(Models.Customer));
            Assert.AreEqual(newCustomer.Id, testCustomer.Id);
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            var testCustomer = this.MockCustomerBusiness.GetAllAsync().Result.FirstOrDefault();

            //UPDATE ONE OF THE FIELDS
            testCustomer.ForeName = "Hannah";

            //SAVE THE UPDATE
            var customerBool = this.MockCustomerBusiness.UpdateAsync(testCustomer).Result;

            //VERIFY THE UPDATE
            Assert.AreEqual("Hannah", this.MockCustomerBusiness.GetAsync(testCustomer.Id.Value).Result.ForeName);
        }

        [TestMethod]
        public void SearchCustomerByForename()
        {
            var testCustomer = this.MockCustomerBusiness.SearchAsync("James", null, null, null).Result.FirstOrDefault();

            Assert.AreEqual("James", this.MockCustomerBusiness.GetAsync(testCustomer.Id.Value).Result.ForeName);
        }


        [TestMethod]
        public void SearchCustomerBySurname()
        {
            var testCustomer = this.MockCustomerBusiness.SearchAsync(null, "Inacay", null, null).Result.FirstOrDefault();

            Assert.AreEqual("Inacay", this.MockCustomerBusiness.GetAsync(testCustomer.Id.Value).Result.Surename);
        }

        [TestMethod]
        public void SearchCustomerByPostcode()
        {
            var testCustomer = this.MockCustomerBusiness.SearchAsync(null, null, "HU5 456", null).Result.FirstOrDefault();

            Assert.AreEqual("HU5 456", this.MockCustomerBusiness.GetAsync(testCustomer.Id.Value).Result.Address.Postcode);
        }

        [TestMethod]
        public void SearchCustomerByEmail()
        {
            var testCustomer = this.MockCustomerBusiness.SearchAsync(null, null, null, "test@yahoo.com").Result.FirstOrDefault();

            Assert.AreEqual("test@yahoo.com", this.MockCustomerBusiness.GetAsync(testCustomer.Id.Value).Result.ContactInformation.FirstOrDefault().Value);
        }
    }
}
