using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Vezeeta.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        IQueryable<T> GetAll();
        Task<T> Find(Expression<Func<T, bool>> predicate, string[] includes = null);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, string[] includes = null);
        EntityEntry Explicit(T entity);
        Task AddAsync(T entity);
        //Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void Remove(T entity);
        //public Task<IQueryable<T>> GetData(int pageNumber, int PageSize);
        //Task<IEnumerable<T>> GetData(int pageNumber, int pageSize, IEnumerable<T> entity);
        //void RemoveRange(IEnumerable<T> entities);

    }
}
