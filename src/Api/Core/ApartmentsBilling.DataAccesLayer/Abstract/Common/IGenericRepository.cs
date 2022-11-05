using ApartmentsBilling.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.DataAccesLayer.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        bool Update(T entity);
        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool checkstatus = false, bool tracking = true);
        Task<List<T>> GetListWithInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool checkstatus = false, bool tracking = true);
        Task<T> GetSingleWtihInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<T, object>>[] includes);
        Task SaveChangeAsync();
    }
}
