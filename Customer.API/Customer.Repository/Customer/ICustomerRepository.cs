using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Repository.Customer
{
    public interface ICustomerRepository : IRepository<Models.Customer>
    {
        Task<IEnumerable<Guid>> Search(string forename, string surename, string postcode, string emailAddress);
        Task<bool> DeleteAsync(Guid id);

    }
}