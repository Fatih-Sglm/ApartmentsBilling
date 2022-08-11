using ApartmentsBilling.PaymentApiSevices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApiSevices.Features.Abstract.common
{
    public interface IGenericService<T> where T : BaseEntity
    {
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);

        Task DeleteAsync(string id);

        Task<T> GetById(string id);

        Task<T> Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null);


    }
}
