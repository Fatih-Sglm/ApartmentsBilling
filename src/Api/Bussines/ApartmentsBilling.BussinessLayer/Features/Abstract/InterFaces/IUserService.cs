using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.Entity.Entities;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IUserService : IService<User>
    {
        Task<bool> AddUserAsync(CreateUserDto user);
        Task<bool> ChangePassword(ChangePasswordDto user);
        Task<TokenDto> LoginAsync(LoginUserDto userDto);
    }
}
