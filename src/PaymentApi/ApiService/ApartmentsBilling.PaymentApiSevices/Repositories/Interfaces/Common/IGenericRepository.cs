using ApartmentsBilling.PaymentApiSevices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApiSevices.Repositories.Common
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<bool> CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task<T> GetById(string id);
        Task<T> Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null);
    }
}
