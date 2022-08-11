using ApartmentsBilling.Entity.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract
{
    public interface IService<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool checkstatus = false, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool checkstatus = false, bool tracking = true);
        Task<List<T>> GetListWithInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetSingleWtihInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<T, object>>[] includes);
    }
}
