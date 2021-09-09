using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Repository.ContactInformation
{
    public class ContactInformationRepository : Repository<Models.ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(string connection) : base(connection)
        { }

       

        public async Task<IEnumerable<Models.ContactInformation>> Get(Guid CustomerId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<Models.ContactInformation>("[dbo].[ContactInformation_Get_By_CustomerId]", new { CustomerId }, commandType: CommandType.StoredProcedure));
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<Guid?> InsertAsync(Guid? guid, Models.ContactInformation entity)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();

            parameters.Add("@Type", value: entity.Type, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Value", value: entity.Value, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@CustomerId", value: guid, dbType: DbType.Guid, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[ContactInformation_Insert]",
                            parameters,
                            commandType: CommandType.StoredProcedure,
                            transaction: transactionopen)) != 0;

                        if (result)
                        {
                            return parameters.Get<Guid>("@CustomerId");

                        }
                        else
                        {
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();

            parameters.Add("@CustomerId", value: id, dbType: DbType.Guid, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result =  (await transactionopen.Connection.ExecuteAsync("[dbo].[ContactInformation_Delete_By_Id]",
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

        public override Task<Models.ContactInformation> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<Guid?> InsertAsync(Models.ContactInformation entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> UpdateAsync(Models.ContactInformation entity)
        {
            throw new NotImplementedException();
        }

    }
}
