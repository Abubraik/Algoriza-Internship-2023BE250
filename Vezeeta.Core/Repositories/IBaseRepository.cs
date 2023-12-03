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
        ValueTask<T> GetByIdAsync(string id);
        IQueryable<T> GetAll();
        Task<T> Find(Expression<Func<T, bool>> predicate);
        //Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        //Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        void Remove(T entity);
        //void RemoveRange(IEnumerable<T> entities);

    }
}
