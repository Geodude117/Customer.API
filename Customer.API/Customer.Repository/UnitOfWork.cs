using Customer.Repository.Address;
using Customer.Repository.ContactInformation;
using Customer.Repository.Customer;

namespace Customer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connection;

        /// <summary>
        /// Designed like this for DI _connection, Connection will generate new instance on call.
        /// </summary>
        /// <param name="connection"></param>
        public UnitOfWork(string connection)
        {
            _connection = connection;
            AddressRepo = new AddressRepository(connection);
            ContactInformationRepo = new ContactInformationRepository(connection);
            CustomerRepo = new CustomerRepository(connection);
        }

        public IAddressRepository AddressRepo { get; }
        public IContactInformationRepository ContactInformationRepo { get; }
        public ICustomerRepository CustomerRepo { get; }

    }
}
