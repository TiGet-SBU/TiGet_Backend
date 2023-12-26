using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetByConditionAsync(Expression<Func<T, bool>> condition, params string[] includes);
        Task<IEnumerable<T>> GetAllAsync(int first = 0, int last = int.MaxValue, Expression<Func<T, bool>>? condition = null, params string[] includes);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);

    }
}
