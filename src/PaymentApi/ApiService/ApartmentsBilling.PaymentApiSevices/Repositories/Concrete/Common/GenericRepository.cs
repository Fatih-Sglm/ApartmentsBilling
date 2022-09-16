using ApartmentsBilling.PaymentApiSevices.DBSettings;
using ApartmentsBilling.PaymentApiSevices.Entities.Common;
using ApartmentsBilling.PaymentApiSevices.Repositories.Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApiSevices.Repositories.Concrete.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> Collection;
        public GenericRepository(IDbSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DatabaseName);
            this.Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }
        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await Collection.InsertOneAsync(entity);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task DeleteAsync(string id)
        {
            await Collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await Collection.Find(expression).FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? Collection.AsQueryable() : Collection.AsQueryable().Where(expression);
        }


        public async Task<T> GetById(string id)
        {
            return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
