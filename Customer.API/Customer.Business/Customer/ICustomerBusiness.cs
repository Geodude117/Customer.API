using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Business.Customer
{
    public interface ICustomerBusiness
    {
        Task<IEnumerable<Models.Customer>> GetAllAsync();

        Task<Models.Customer> GetAsync(Guid id);

        Task<Guid> InsertAsync(Models.Customer model);
    }
}