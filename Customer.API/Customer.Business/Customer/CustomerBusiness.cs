using Customer.Repository;
using Customer.Repository.Customer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Business.Customer
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IConfiguration _configuration;

        public CustomerBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _CustomerRepository = unitOfWork.CustomerRepo;
            _configuration = configuration;
        }
        public Task<IEnumerable<Models.Customer>> GetAllAsync()
        {
            return _CustomerRepository.GetAllAsync();
        }

        public Task<Models.Customer> GetAsync(Guid id)
        {
            return _CustomerRepository.GetAsync(id);
        }

        public Task<Guid> InsertAsync(Models.ContactInformation model)
        {
            //TODO : ADD INSERT LOGIC
            throw new NotImplementedException();
        }
    }
}
