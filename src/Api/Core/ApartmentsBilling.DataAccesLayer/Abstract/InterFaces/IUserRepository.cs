using ApartmentsBilling.Entity.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.DataAccesLayer.Abstract.InterFaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> GetSingleForAddUser(Expression<Func<User, bool>> expression);
    }
}
