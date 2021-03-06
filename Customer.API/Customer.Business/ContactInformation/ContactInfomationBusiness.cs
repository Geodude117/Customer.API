using Customer.Repository;
using Customer.Repository.ContactInformation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Business.ContactInformation
{
    public class ContactInfomationBusiness : IContactInfomationBusiness
    {
        private readonly IContactInformationRepository _ContactInformationRepository;
        private readonly IConfiguration _configuration;

        public ContactInfomationBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _ContactInformationRepository = unitOfWork.ContactInformationRepo;
            _configuration = configuration;
        }

        public Task<IEnumerable<Models.ContactInformation>> GetAllAsync()
        {
            return _ContactInformationRepository.GetAllAsync();
        }

        public Task<IEnumerable<Models.ContactInformation>> GetAsync(Guid id)
        {
            return _ContactInformationRepository.GetAllForCustomerId(id);
        }

        public Task<Guid?> InsertAsync(Guid? guid, Models.ContactInformation model)
        {
            return _ContactInformationRepository.InsertAsync(guid,model);

        }
    }
}
