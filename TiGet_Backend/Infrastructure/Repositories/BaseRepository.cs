using Application.Interfaces.Repositories;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(int first = 0, int last = int.MaxValue, Expression<Func<T, bool>>? condition = null, params string[] includes)
        {
            
            IQueryable<T> context = _context.Set<T>().OrderByDescending(e => e.CreatedDate);

            if (condition != null) context = context.Where(condition);
            if (first != 0) context = context.Skip(first);
            if (last != int.MaxValue) context = context.Take(last - first);
            // todo : Test... prone to Bug! 

            var query = ApplyIncludes(context, includes);

            return await query.ToListAsync();
        }

        public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, params string[] includes)
        {
            var query = ApplyIncludes(_context.Set<T>(), includes);

            return await query.FirstOrDefaultAsync(condition);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        private IQueryable<T> ApplyIncludes(IQueryable<T> query, params string[] includes)
        {
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }
    }
}
