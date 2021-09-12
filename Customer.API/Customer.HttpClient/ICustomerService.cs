using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.HttpClient
{
    public interface ICustomerService
    {
        Task<bool> DeleteCustomer(Guid? guid);
        Task<IEnumerable<Models.Customer>> GetAllCustomers();
        Task<Models.Customer> GetCustomers(Guid? guid);
        Task<Guid?> InsertCustomer(Models.Customer customerModel);
        Task<IEnumerable<Models.Customer>> SearchCustomerByCriteria(string forename, string surname, string postcode, string emailAddress);
        Task<bool> UpdateCustomer(Models.Customer customerModel);
    }
}