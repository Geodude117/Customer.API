using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Business.ContactInformation
{
    public interface IContactInfomationBusiness
    {

        Task<IEnumerable<Models.ContactInformation>> GetAsync(Guid id);

        Task<Guid?> InsertAsync(Models.ContactInformation model);
    }
}
