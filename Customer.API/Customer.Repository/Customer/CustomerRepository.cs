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
        public override async Task<IEnumerable<Models.Customer>> GetAllAsync()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<Models.Customer>("[dbo].[Customer_Get_All]", commandType: CommandType.StoredProcedure));
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
                    return (await connection.QueryAsync<Models.Customer>("[dbo].[Customer_Get_By_Id]", new { Id }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<Guid?> InsertAsync(Guid? id, Models.Customer entity)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();

            parameters.Add("@Forename", value: entity.ForeName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Surename", value: entity.Surename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@DateOfBirth", value: entity.DateOfBirth, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@CustomerId", value: entity.Id, dbType: DbType.Guid, direction: ParameterDirection.Input);

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

                        if (result == 1)
                        {
                            transactionopen.Commit();
                            return parameters.Get<Guid>("@CustomerId");
                        }
                        else
                        {
                            transactionopen.Rollback();
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public async Task<IEnumerable<Guid>> Search(string forename, string surename, string postcode, string emailAddress)
        {
            List<Models.Customer> customers = new List<Models.Customer>();
            var parameters = new DynamicParameters();

            parameters.Add("@Forename", value: forename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Surename", value: surename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@DateOfBirth", value: postcode, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@EmailAddress", value: emailAddress, dbType: DbType.String, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    var customerids =  (await connection.QueryAsync<Guid>("[dbo].[Customer_Search]", parameters,
                        commandType: CommandType.StoredProcedure)).DefaultIfEmpty();

                    return customerids;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public async Task<bool> DeleteAsync(Guid CustomerId)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();

            parameters.Add("@CustomerId", value: CustomerId, dbType: DbType.Guid, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        return (await transactionopen.Connection.ExecuteAsync("[dbo].[Customer_Delete_By_Id]",
                            parameters,
                            commandType: CommandType.StoredProcedure,
                            transaction: transactionopen)) != 0;
                    }
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public override async Task<bool> UpdateAsync(Models.Customer entity)
        {
            IDbTransaction transactionopen = null;

            var parameters = new DynamicParameters();

            parameters.Add("@CustomerId", value: entity.Id, dbType: DbType.Guid, direction: ParameterDirection.Input);
            parameters.Add("@Forename", value: entity.ForeName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Surename", value: entity.Surename, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@HouseNo", value: entity.Address.HouseNo, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@DateOfBirth", value: entity.DateOfBirth, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Street", value: entity.Address.Street, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@City", value: entity.Address.City, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Postcode", value: entity.Address.Postcode, dbType: DbType.String, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[Customer_Update_By_Id]",
                            parameters,
                            commandType: CommandType.StoredProcedure,
                            transaction: transactionopen)) != 0;
                        transactionopen.Commit();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }
    }
}
