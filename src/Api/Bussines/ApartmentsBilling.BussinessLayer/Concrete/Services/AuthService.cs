using ApartmentsBilling.BussinessLayer.Configuration.Auth;
using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.AdminDto;
using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IFlatRepository _FlatRepo;
        private readonly IUserService _UserService;
        private readonly IApartmentRepository _ApartmentRepo;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration,
            IFlatRepository flatRepo, IUserService userService, IApartmentRepository apartmentRepo)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _FlatRepo = flatRepo;
            _UserService = userService;
            _ApartmentRepo = apartmentRepo;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = await userRepository.GetSingleAsync(x => x.Id == changePasswordDto.Id, true);
                var Dbuser = await userRepository.GetSingleWtihInclude(x => x.Id == user.Id, true, true, x => x.Password);
                if (!PaswordHash.VerifyPasswordHash(changePasswordDto.OldPassword, Dbuser.Password.PasswordHash, Dbuser.Password.PasswordSalt))
                {
                    throw new ClientSideException("Şİfreniz Hatalı");
                }
                PaswordHash.CreatePasswordHash(changePasswordDto.NewPassword, out byte[] PasswordHash, out byte[] PasswordSalt);
                Dbuser.Password.PasswordSalt = PasswordSalt;
                Dbuser.Password.PasswordHash = PasswordHash;
                userRepository.Update(Dbuser);
                await userRepository.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Şifre" + CustomErrorMessage.UpdatetErrorMessage);
            }
        }

        public async Task<TokenDto> LoginAsync(LoginUserDto userDto)
        {
            var user = await userRepository.GetSingleWtihInclude(x => x.Email == userDto.Email, true, true, x => x.Password);
            if (PaswordHash.VerifyPasswordHash(userDto.Password, user.Password.PasswordHash, user.Password.PasswordSalt))
            {
                try
                {
                    return CreateJwt.GetJtwtToken(_configuration, user);
                }
                catch (Exception)
                {
                    throw new Exception("Giriş Yapılamadı.Lütfen Tekrar deneyiniz.");
                }
            }
            throw new ClientSideException("Şİfreniz Hatalı");
        }

        public async Task<bool> Register(CreateAdminDto createAdminDto)
        {
            try
            {
                Apartment apt = new()
                {
                    Name = createAdminDto.ApartmentName,
                };
                await _ApartmentRepo.AddAsync(apt);
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
                await _FlatRepo.AddAsync(f);
                User user = new()
                {
                    FullName = createAdminDto.FullName,
                    Email = createAdminDto.Email,
                    FlatId = f.Id,
                    PhoneNumber = createAdminDto.PhoneNumber,
                    IdNumber = createAdminDto.IdNumber,
                    Role = UserRole.Admin,
                };
                await _UserService.AddUserAsync(_mapper.Map<CreateUserDto>(user), true);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Kayıt Yapılırken bir hata Oluştu lütfen tekrar deneyiniz");
            }
        }
    }
}
