using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public async ValueTask<T> GetByIdAsync(string id)
        {
            return await _entities.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return  _entities.AsQueryable();
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

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).SingleOrDefaultAsync();
        }



        //public  IEnumerable<T> GetData(int pageNumber, int PageSize)
        //{
        //    return _entities.Skip((pageNumber * 1) * PageSize).Take(PageSize);
        //    //retrun context.stocks.Skip((pageNumber - 1) * PageSize).Take(PageSize);
        //}
    }
}
