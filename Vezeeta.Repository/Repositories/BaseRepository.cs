using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<T> GetById(int id) => await _entities.FindAsync(id) ?? null;

        public async Task<IEnumerable<T>> GetAll(int pageNumber, int pageSize)
        {
            return await _entities.Skip((pageNumber * 1) * pageSize).Take(pageSize).ToListAsync();
        }


        //public  IEnumerable<T> GetData(int pageNumber, int PageSize)
        //{
        //    return _entities.Skip((pageNumber * 1) * PageSize).Take(PageSize);
        //    //retrun context.stocks.Skip((pageNumber - 1) * PageSize).Take(PageSize);
        //}
    }
}
