using ApartmentsBilling.Common.Dtos.AdminDto;
using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces
{
    public interface IAuthService
    {
        Task<bool> Register(CreateAdminDto createAdminDto);
        Task<bool> ChangePassword(ChangePasswordDto user);
        Task<TokenDto> LoginAsync(LoginUserDto userDto);
    }
}
