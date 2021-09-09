using Customer.Repository;
using Customer.Repository.Address;
using Customer.Repository.ContactInformation;
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
        private readonly IContactInformationRepository _ContactInformationRepository;
        private readonly IAddressRepository _AddressRepository;


        private readonly IConfiguration _configuration;

        public CustomerBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _CustomerRepository = unitOfWork.CustomerRepo;
            _ContactInformationRepository = unitOfWork.ContactInformationRepo;
            _AddressRepository = unitOfWork.AddressRepo;

            _configuration = configuration;
        }

        public async Task<Models.Customer> GetAsync(Guid id)
        {
            //CREATE OBJECTS
            Models.Customer customer = new Models.Customer();
            Models.Address address = new Models.Address();
            IEnumerable<Models.ContactInformation> contactInfoList = new List<Models.ContactInformation>();

            //GET ADDRESS
            address = await  _AddressRepository.GetAsync(id);

            //GET CONTACT INFO
            contactInfoList = await _ContactInformationRepository.Get(id);

            //GET CUSTOMER
            customer = await _CustomerRepository.GetAsync(id);

            //SET RESULTS
            customer.Address = address;
            customer.ContactInformation = (ICollection<Models.ContactInformation>)contactInfoList;

            return customer;
        }

        public async Task<Guid?> InsertAsync(Models.Customer model)
        {
            //ADD CUSTOMER 
            var Id = await _CustomerRepository.InsertAsync(model);

            //ADD ADDRESS
            var id = await _AddressRepository.InsertAsync(Id, model.Address);

            //ADD CONTACT INFO
            foreach (var contactInfo in model.ContactInformation)
            {
                var guid = _ContactInformationRepository.InsertAsync(Id, contactInfo);
            }

            return Id;
        }

        public async Task<IEnumerable<Models.Customer>> SearchAsync(string forename, string surename, string postcode, string emailAddress)
        {
            List<Models.Customer> customerList = new List<Models.Customer>();

           var customerIds = await _CustomerRepository.Search(forename, surename, postcode, emailAddress);

            foreach (var id in customerIds)
            {
                customerList.Add(await this.GetAsync(id));

            }
            return customerList;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _CustomerRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Models.Customer model)
        {
            //CHECK IF THE THERE ARE ANY DIFFERENCES IF NOT RETURN FALSE

            var oldCustomer = await this.GetAsync(model.Id.Value);
            var result = model.CompareTo(oldCustomer) > 0;
            if (!result)
                return result;

            //PERFOM UPDATE

            List<Task<bool>> TaskList = new List<Task<bool>>
            {
                //UPDATE CUSTOMER AND ADDRESS RECORDS
                _CustomerRepository.UpdateAsync(model),
                //DELETE CONTACT INFO  
                _ContactInformationRepository.DeleteAsync(model.Id.Value)
            };

            var updateResults = TaskSummary.TaskBoolSummary(await Task.WhenAll(TaskList));

            //INSERT NEW CONTACT INFO
            foreach (var contactInfo in model.ContactInformation)
            {
                if (await _ContactInformationRepository.InsertAsync(model.Id, contactInfo) == null)
                {
                    updateResults = false;
                }
            }

            return updateResults;
        }
    }
}
