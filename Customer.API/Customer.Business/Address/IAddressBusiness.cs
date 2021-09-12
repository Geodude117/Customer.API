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
        Task<Models.Address> GetAsync(Guid id);
        Task<IEnumerable<Models.Address>> GetAllAsync();
    }
}
