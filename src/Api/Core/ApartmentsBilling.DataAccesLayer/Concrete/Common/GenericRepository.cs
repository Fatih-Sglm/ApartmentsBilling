using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.DataAccesLayer.Abstract;
using ApartmentsBilling.DataAccesLayer.Configuration.Extension;
using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.DataAccesLayer.Features.Concrete.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly ApartmentDbContext _context;
        public GenericRepository(ApartmentDbContext dbContext)
        {
            _context = dbContext;
        }
        protected DbSet<T> Table => _context.Set<T>();


        #region Add
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }
        #endregion
        #region Update
        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);

            return entityEntry.State == EntityState.Modified;
        }
        #endregion
        #region Remove
        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }
        #endregion
        #region GetList
        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool checkstatus = false, bool tracking = true)
        {
            var query = Table.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);
            if (checkstatus)
                return query.Where(x => x.Status == true).AsQueryable();

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (!tracking)
                query = query.AsNoTracking();
            return await Task.FromResult(query);
        }

        public async Task<List<T>> GetListWithInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (checkstatus)
                query = query.Where(x => x.Status == true);

            if (!tracking)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        #endregion
        #region GetSingle
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool checkstatus = false, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            if (checkstatus)
                query = query.Where(x => x.Status == true);


            if (!tracking)
                query = query.AsNoTracking();

            var value = await query.FirstOrDefaultAsync(expression);

            if (value == null)
            {
                EntityEnums entityEnums = new();
                throw new NotFoundException($"{entityEnums.Func(typeof(T).Name)} Bulunamadı");
            }
            return value;
        }
        public Task<T> GetSingleWtihInclude(Expression<Func<T, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (checkstatus)
                query = query.Where(x => x.Status == true);

            if (!tracking)
                query = query.AsNoTracking();

            var value = query.FirstOrDefaultAsync();

            if (value == null)
            {
                EntityEnums entityEnums = new();
                throw new NotFoundException($"{entityEnums.Func(typeof(T).Name)} Bulunamadı");
            }
            return value;
        }
        #endregion
        #region OtherFunction
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }
            return query;
        }

        #endregion
    }
}

