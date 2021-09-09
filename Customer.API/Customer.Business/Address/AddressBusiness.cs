using Customer.Repository;
using Customer.Repository.Address;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Business.Address
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IAddressRepository _addressRepositroy;
        private readonly IConfiguration _configuration;

        public AddressBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _addressRepositroy = unitOfWork.AddressRepo;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Models.Address>> GetAllAsync()
        {
            return await _addressRepositroy.GetAllAsync();
        }

        public async Task<Models.Address> GetAsync(Guid id)
        {
            return await _addressRepositroy.GetAsync(id);
        }

    }
}
