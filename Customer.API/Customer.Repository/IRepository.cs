using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<Guid?> InsertAsync(Guid? id, T entity);
        Task<bool> UpdateAsync(T entity);
    }
}