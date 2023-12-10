using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Vezeeta.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        IQueryable<T> GetAll();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null);
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, string[] includes = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        void Remove(T entity);
    }
}
