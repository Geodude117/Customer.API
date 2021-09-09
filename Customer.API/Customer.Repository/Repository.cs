using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Customer.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connection;

        public Repository(string connection)
        {
            _connection = connection;
        }

        public IDbConnection Connection => new SqlConnection(_connection);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<T> GetAsync(Guid id);

        public abstract Task<Guid> InsertAsync(T entity);

        public abstract Task<bool> UpdateAsync(T entity);

        public abstract Task<bool> DeleteAsync(Guid id);
    }
}
