using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete
{
    public class GenericServices<T> : IService<T> where T : BaseEntity
    {

        protected readonly IGenericRepository<T> _repository;

        public GenericServices(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        #region Add
        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                await SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _repository.AddRangeAsync(entities);
                await _repository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Update
        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _repository.Update(entity);
                await SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        #region Remove
        public async Task<bool> RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            return await SaveChangeAsync();
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            return await SaveChangeAsync();
        }
        #endregion
        #region GetList
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool checkstatus = false, bool tracking = true)
        {
            return await _repository.GetAll(orderBy, checkstatus, tracking).ToListAsync();
        }


        public Task<List<T>> GetListWithInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetListWithInclude(predicate, checkstatus, tracking, orderBy, includes);
        }
        #endregion
        #region GetSingle

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool checkstatus = false, bool tracking = true)
        {
            var value = await _repository.GetSingleAsync(expression, checkstatus, tracking);
            if (value == null)
            {
                EntityToDto entityToDto = new();
                throw new NotFoundException($"{entityToDto.Func(typeof(T).Name)} Bulunamadı");
            }

            return value;
        }
        public async Task<T> GetSingleWtihInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var value = await _repository.GetSingleWtihInclude(predicate, checkstatus, tracking, includes);
            if (value == null)
            {
                EntityToDto entityToDto = new();
                throw new NotFoundException($"{entityToDto.Func(typeof(T).Name)} Bulunamadı");
            }
            return value;
        }

        #endregion
        #region SaveChanges
        public async Task<bool> SaveChangeAsync()
        {
            try
            {
                await _repository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion




















    }
}
