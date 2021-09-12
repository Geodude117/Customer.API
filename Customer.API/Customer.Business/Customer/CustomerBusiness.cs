using Customer.Repository;
using Customer.Repository.Address;
using Customer.Repository.ContactInformation;
using Customer.Repository.Customer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Models.Customer>> GetAllAsync()
        {
            //GET ALL CUSTOMERS 
            var customers = await _CustomerRepository.GetAllAsync();

            foreach (var customer in customers)
            {
                //POPULATE ADDRESS

                var address = await _AddressRepository.GetAsync(customer.Id.Value);
                customer.Address = address;

                //POPULATE CONTACT INFO
                var contactInfoList = await _ContactInformationRepository.GetAllForCustomerId(customer.Id.Value);
                customer.ContactInformation = (ICollection<Models.ContactInformation>)contactInfoList;
            }

            return customers;
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
            contactInfoList = await _ContactInformationRepository.GetAllForCustomerId(id);

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
            var customerResponseId = await _CustomerRepository.InsertAsync(model.Id.Value, model);

            //ADD ADDRESS
            var addressReponseId = await _AddressRepository.InsertAsync(customerResponseId, model.Address);

            //ADD CONTACT INFO
            foreach (var contactInfo in model.ContactInformation)
            {
                var guid = await _ContactInformationRepository.InsertAsync(addressReponseId, contactInfo);
            }

            return customerResponseId;
        }

        public async Task<IEnumerable<Models.Customer>> SearchAsync(string forename, string surename, string postcode, string emailAddress)
        {
            List<Models.Customer> customerList = new List<Models.Customer>();

           var customerIds = await _CustomerRepository.Search(forename, surename, postcode, emailAddress);

           //REMOVES DUPLICATES
            customerIds.Distinct();

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

            List<Task<bool>> TaskList = new()
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
