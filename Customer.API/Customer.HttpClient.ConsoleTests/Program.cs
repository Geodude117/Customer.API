using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.HttpClient.ConsoleTests
{
    class Program
    {
        private static ICustomerService _customerService;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            _customerService = new CustomerService();

            var _newGuid = Guid.NewGuid();

            var allCustomer = GetAllCustomers().Result;
            Console.WriteLine("Get all customer response, response : " + JsonConvert.SerializeObject(allCustomer, Formatting.Indented));

            var insertResult = InsertValidCustomer(_newGuid).Result;
            Console.WriteLine("Insert customer response, Guid :" + insertResult.Value);

            var getResult = GetCustomerById(insertResult.Value).Result;
            Console.WriteLine("Get customer response, Customer :" + JsonConvert.SerializeObject(getResult, Formatting.Indented));

            string newName = "Dave";
            getResult.ForeName = newName;

            var updateResult = UpdateCustomer(getResult).Result;
            Console.WriteLine("Update customer response : " + updateResult);

            var deleteResult = DeleteCustomer(getResult.Id.Value).Result;
            Console.WriteLine("Delete customer response : " + deleteResult);

        }

        private static async Task<IEnumerable<Models.Customer>> GetAllCustomers()
        {
            return  await _customerService.GetAllCustomers();
        }

        private static async Task<Models.Customer> GetCustomerById(Guid? guid)
        {
            return await _customerService.GetCustomers(guid);
        }

        private static void GetCustomerByName()
        {
            var result = _customerService.SearchCustomerByCriteria("Geovan", null, null, null);
        }

        private static async Task<Guid?> InsertValidCustomer(Guid? guid)
        {
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
                Value = "testtest@yahoo.com"
            };

            contactInfoList.Add(contactInfo);

            Models.Customer newCustomer = new Models.Customer
            {
                Id = guid,
                ForeName = "Geovan",
                Surename = "Inacay",
                DateOfBirth = "21/11/1992",
                Address = Address,
                ContactInformation = contactInfoList
            };

            return await _customerService.InsertCustomer(newCustomer);
        }

        private static async Task<bool> DeleteCustomer(Guid? guid)
        {
            return await _customerService.DeleteCustomer(guid);
        }

        private static async Task<bool> UpdateCustomer(Models.Customer customerModel)
        {
            return await _customerService.UpdateCustomer(customerModel);
        }

    }
}
