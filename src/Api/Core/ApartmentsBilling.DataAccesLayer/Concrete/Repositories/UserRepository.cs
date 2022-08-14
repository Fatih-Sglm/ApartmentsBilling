using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.DataAccesLayer.Contexts;
using ApartmentsBilling.DataAccesLayer.Features.Concrete.Common;
using ApartmentsBilling.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.DataAccesLayer.Concrete.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApartmentDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> GetSingleForAddUser(Expression<Func<User, bool>> expression)
        {
            var query = Table.AsQueryable();
            query = query.Where(expression);
            query = query.AsNoTracking();
            var value = query.FirstOrDefaultAsync();
            if (value != null)
                return Task.FromResult(false);
            return Task.FromResult(true);
        }
    }
}
