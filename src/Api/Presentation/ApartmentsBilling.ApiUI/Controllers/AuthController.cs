using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.AdminDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            return CreatActionResult(CustomResponseDto<TokenDto>.SuccesWithData(await _authService.LoginAsync(loginUserDto)));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateAdminDto createAdminDto)
        {
            await _authService.Register(createAdminDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Kayıt Başarılı"));
        }

        [HttpPatch]
        public async Task<IActionResult> ChangePassWordAsync(ChangePasswordDto changePasswordDto)
        {
            await _authService.ChangePassword(changePasswordDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Şifreniz Başarılı Bir şekilde Değiştirildi"));
        }
    }
}
