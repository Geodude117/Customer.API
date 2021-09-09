using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Repository.Customer
{
    public class CustomerRepository : Repository<Models.Customer>, ICustomerRepository
    {
        public CustomerRepository(string connection) : base(connection)
        { }

        public override Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<Models.Customer>> GetAllAsync()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<Models.Customer>("[dbo].[Customer_Get_All]",
                        commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<Models.Customer> GetAsync(Guid Id)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return ((Models.Customer)await connection.QueryAsync<Models.Customer>("[dbo].[Customer_Get_By_Id]", new { Id }, commandType: CommandType.StoredProcedure));
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<Guid> InsertAsync(Models.Customer entity)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();

            parameters.Add("@Forename", value: entity.ForeName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Surename", value: entity.Surename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@DateOfBirth", value: entity.DateOfBirth, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@HouseNo", value: entity.Address.HouseNo, dbType: DbType.UInt64, direction: ParameterDirection.Input);
            parameters.Add("@Street", value: entity.Address.Street, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@City", value: entity.Address.City, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@PostCode", value: entity.Address.Postcode, dbType: DbType.String, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[Customer_Insert]",
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

        public override Task<bool> UpdateAsync(Models.Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
