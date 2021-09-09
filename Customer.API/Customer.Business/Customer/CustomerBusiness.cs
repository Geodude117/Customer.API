﻿using Customer.Repository;
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

        public async Task<Guid> InsertAsync(Models.Customer model)
        {
            //ADD CUSTOMER 
            var Id = await _CustomerRepository.InsertAsync(model);

            //ADD ADDRESS
            model.Address.CustomerId = model.Id;
            var id = await _AddressRepository.InsertAsync(model.Address);

            //ADD CONTACT INFO
            foreach (var contactInfo in model.ContactInformation)
            {
                contactInfo.CustomerId = model.Id;
                var guid = _ContactInformationRepository.InsertAsync(contactInfo);
            }

            return Id;
        }
    }
}
