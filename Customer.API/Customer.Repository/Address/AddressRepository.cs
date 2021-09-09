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

        public override async Task<Models.Address> GetAsync(Guid CustomerId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<Models.Address>("[dbo].[Address_Get_By_CustomerId]", new { CustomerId }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<Guid> InsertAsync(Models.Address entity)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();

            parameters.Add("@HouseNo", value: entity.HouseNo, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@Street", value: entity.Street, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@City", value: entity.City, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@PostCode", value: entity.Postcode, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@CustomerId", value: entity.CustomerId, dbType: DbType.Guid, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[Address_Insert]",
                            parameters,
                            commandType: CommandType.StoredProcedure,
                            transaction: transactionopen));
                        transactionopen.Commit();

                        return parameters.Get<Guid>("@CustomerId");
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public override Task<bool> UpdateAsync(Models.Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
