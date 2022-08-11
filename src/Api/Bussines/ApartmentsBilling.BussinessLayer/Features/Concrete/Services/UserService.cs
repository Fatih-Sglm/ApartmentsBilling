using ApartmentsBilling.BacGroundJobs.Features.Abstract;
using ApartmentsBilling.BussinessLayer.Configuration.Auth;
using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.DataAccesLayer.Features.Abstract;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class UserService : GenericServices<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IFlatService _flatService;
        private readonly IJobs _jobs;
        //private readonly IRedisService _redis;
        public UserService(IGenericRepository<User> repository, IMapper mapper, IConfiguration configuration, IJobs jobs/*, IRedisService redis*/, IFlatService flatService) : base(repository)
        {
            _mapper = mapper;
            _configuration = configuration;
            _jobs = jobs;
            _flatService = flatService;
            // _redis = redis;
        }
        public async Task<bool> AddUserAsync(CreateUserDto userDto)
        {
            var HasUser = await _repository.GetSingleAsync(x => x.Email == userDto.Email);
            if (HasUser != null) throw new ClientSideException("BU Mail İLe Daha Önce Kayıt Olunmuş");
            var pass = Guid.NewGuid().ToString("d").Substring(1, 6);
            PaswordHash.CreatePasswordHash(pass, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<User>(userDto);
            user.Password = new UserPassword()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            if (_flatService.GetSingleWtihInclude(x => x.Id == userDto.FlatId, includes: x => x.User) == null)
            {
                await _repository.AddAsync(user);
                var value = await _flatService.GetSingleAsync(x => x.Id == userDto.FlatId, true);
                value.IsEmpty = false;
                await _flatService.UpdateAsync(value);
                if (_jobs.FireAndForget(user.Email, pass))
                {
                    await SaveChangeAsync();
                    return true;
                }
                else
                    return false;
            }
            else
                throw new ClientSideException("Bu Daire Başkası Üzerine Kayıtlı");

        }

        public async Task<bool> ChangePassword(ChangePasswordDto user)
        {
            var Dbuser = await _repository.GetSingleWtihInclude(x => x.Id == user.Id, true, true, x => x.Password);
            if (Dbuser == null)
                throw new NotFoundException("Kullanıcı Bulunamadı");
            if (!PaswordHash.VerifyPasswordHash(user.OldPassword, Dbuser.Password.PasswordHash, Dbuser.Password.PasswordSalt))
            {
                throw new ClientSideException("Şİfreniz Hatalı");
            }

            PaswordHash.CreatePasswordHash(user.NewPassword, out byte[] PasswordHash, out byte[] PasswordSalt);
            Dbuser.Password.PasswordSalt = PasswordSalt;
            Dbuser.Password.PasswordHash = PasswordHash;
            _repository.Update(Dbuser);
            await SaveChangeAsync();
            return true;
        }

        public async Task<TokenDto> LoginAsync(LoginUserDto userDto)
        {
            var user = await _repository.GetSingleWtihInclude(x => x.Email == userDto.Email, true, true, x => x.Password);
            if (PaswordHash.VerifyPasswordHash(userDto.Password, user.Password.PasswordHash, user.Password.PasswordSalt))
            {
                try
                {
                    //_redis.Add($"Id_{user.Id}", user.Id.ToString());
                    //_redis.Add($"Role_{user.Role}", user.Role.ToString());
                    return CreateJwt.GetJtwtToken(_configuration, user);
                }
                catch (Exception)
                {
                    throw new Exception("Giriş Yapılamadı.Lütfen Tekrar deneyiniz.");
                }
            }
            throw new ClientSideException("Şİfreniz Hatalı");
        }
    }
}
