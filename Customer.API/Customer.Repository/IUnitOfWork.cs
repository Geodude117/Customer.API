using Customer.Repository.Address;
using Customer.Repository.ContactInformation;
using Customer.Repository.Customer;

namespace Customer.Repository
{
    public interface IUnitOfWork
    {
        IAddressRepository AddressRepo { get; }
        IContactInformationRepository ContactInformationRepo { get; }
        ICustomerRepository CustomerRepo{ get; }
    }
}