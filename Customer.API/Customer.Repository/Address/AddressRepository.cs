using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Repository.Address
{
    public class AddressRepository : Repository<Models.Address>, IAddressRepository
    {
        public AddressRepository(string connection) : base(connection)
        { }

        public override Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Models.Address>> GetAllAsync()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<Models.Address>("[dbo].[Address_Get_All]",
                        commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override Task<Models.Address> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<Guid> InsertAsync(Models.Address entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> UpdateAsync(Models.Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
