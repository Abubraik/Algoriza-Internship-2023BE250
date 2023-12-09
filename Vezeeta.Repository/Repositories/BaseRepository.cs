using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vezeeta.Core.Repositories;

namespace Vezeeta.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            this._context = context;
            this._entities = _context.Set<T>();
        }

        //public async Task<T> GetById(int id) => await _entities.FindAsync(id) ?? null;

        //public async Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize)
        //{
        //    return await _entities.Skip((pageNumber * 1) * pageSize).Take(pageSize).ToListAsync();
        //}

        public async Task<T> GetByIdAsync(string id)
        {
            return await _entities.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            await _entities.AddAsync(entity);

        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> query = _entities;
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return await query.SingleOrDefaultAsync(predicate);
        }
        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> queryable = _entities;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryable = queryable.Include(include);
                }
            }
            return queryable.Where(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, string[] includes = null)
        {
            IQueryable<T> queryable = _entities;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryable = queryable.Include(include);
                }
            }

            return queryable.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        private async Task<IEnumerable<T>> GetData(int pageNumber, int pageSize,IEnumerable<T> entity)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number should be greater than 0.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size should be greater than 0.");
            }
            return entity.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        //retrun context.stocks.Skip((pageNumber - 1) * PageSize).Take(PageSize);
    }
}
