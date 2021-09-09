using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Repository.ContactInformation
{
    public interface IContactInformationRepository : IRepository<Models.ContactInformation>
    {
        Task<IEnumerable<Models.ContactInformation>> Get(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}