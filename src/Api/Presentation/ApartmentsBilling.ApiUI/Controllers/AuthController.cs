using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.AdminDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly IApartmentService _apartmentService;
        private readonly IFlatService _flatService;
        private readonly IMapper _mapper;
        public AuthController(IUserService userService, IApartmentService apartmentService, IFlatService flatService, IMapper mapper)
        {
            _userService = userService;
            _apartmentService = apartmentService;
            _flatService = flatService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            return CreatActionResult(CustomResponseDto<TokenDto>.SuccesWithData(await _userService.LoginAsync(loginUserDto)));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateAdminDto createAdminDto)
        {
            Apartment apt = new()
            {
                Name = createAdminDto.ApartmentName,
            };
            await _apartmentService.AddAsync(apt);
            Flat f = new()
            {
                FloorLocation = createAdminDto.FloorLocation,
                ApartmentId = apt.Id,
                WhichBlock = createAdminDto.WhichBlock,
                FloorType = createAdminDto.FloorType,
                IsRented = createAdminDto.IsRented,
                FloorNumber = createAdminDto.FloorNumber,
                IsEmpty = false
            };
            await _flatService.AddAsync(f);
            User user = new()
            {
                FullName = createAdminDto.FullName,
                Email = createAdminDto.Email,
                FlatId = f.Id,
                PhoneNumber = createAdminDto.PhoneNumber,
                IdNumber = createAdminDto.IdNumber,
                Role = UserRole.Admin,
            };
            await _userService.AddUserAsync(_mapper.Map<CreateUserDto>(user));
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Kayıt Başarılı"));
        }
    }
}
