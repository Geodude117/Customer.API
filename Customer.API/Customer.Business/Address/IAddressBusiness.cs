using Customer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Business.Address
{
    public interface IAddressBusiness 
    {
        Task<IEnumerable<Models.Address>> GetAllAsync();

        Task<Models.Address> GetAsync(Guid id);

    }
}
