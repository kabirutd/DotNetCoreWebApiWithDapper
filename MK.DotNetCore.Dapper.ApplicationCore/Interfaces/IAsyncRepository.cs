using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MK.DotNetCore.Dapper.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : class //BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
