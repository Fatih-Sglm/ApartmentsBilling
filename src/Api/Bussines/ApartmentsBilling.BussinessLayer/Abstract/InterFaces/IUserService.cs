using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(CreateUserDto user, bool isAdmin);

        Task<bool> UpdateAsync(UpdateUserDto updateUserDto);
        Task<bool> RemoveAsync(Guid id);
        Task<List<GetUserDto>> GetListWithInclude(Expression<Func<User, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, params Expression<Func<User, object>>[] includes);

        Task<GetUserDto> GetSingleWtihInclude(Expression<Func<User, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<User, object>>[] includes);

        Task<GetUserDto> GetSingleAsync(Expression<Func<User, bool>> expression, bool checkstatus = false, bool tracking = true);

    }
}
