using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        IQueryable<T> GetAll();
        Task<T> Find(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, int skip, int take, string[] includes = null);
        Task AddAsync(T entity);
        //Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void Remove(T entity);
        public Task<IQueryable<T>> GetData(int pageNumber, int PageSize);
        //void RemoveRange(IEnumerable<T> entities);

    }
}
