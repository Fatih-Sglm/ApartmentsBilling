using ApartmentsBilling.BacGroundJobs.Features.Abstract;
using ApartmentsBilling.BussinessLayer.Configuration.Auth;
using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.BussinessLayer.Configuration.Extension;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.DataAccesLayer.Abstract.InterFaces;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApartmentsBilling.BussinessLayer.Features.Concrete.Repositories
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IFlatRepository _flatRepo;
        private readonly IUserRepository _userRepo;
        private readonly IJobs _jobs;

        public UserService(IMapper mapper, IFlatRepository flatRepo, IUserRepository userRepo, IJobs jobs)
        {
            _mapper = mapper;
            _flatRepo = flatRepo;
            _userRepo = userRepo;
            _jobs = jobs;
        }

        public async Task<bool> AddUserAsync(CreateUserDto userDto, bool isAdmin)
        {
            var HasUser = await _userRepo.GetSingleForAddUser(x => x.Email == userDto.Email);
            if (HasUser) throw new ClientSideException("Bu Mail İle Daha Önce Kayıt Olunmuş");
            var pass = Guid.NewGuid().ToString("d").Substring(1, 6);
            PaswordHash.CreatePasswordHash(pass, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<User>(userDto);
            user.Password = new UserPassword()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            if (!await _userRepo.GetSingleForAddUser(x => x.FlatId == userDto.FlatId))
            {
                try
                {
                    await _userRepo.AddAsync(user);
                    if (!isAdmin)
                    {
                        var value = await _flatRepo.GetSingleAsync(x => x.Id == userDto.FlatId, true);
                        value.IsEmpty = false;
                        _flatRepo.Update(value);
                    }
                    _jobs.FireAndForget(user.Email, pass);
                    await _userRepo.SaveChangeAsync();
                    return true;

                }
                catch (Exception)
                {
                    throw new Exception("Kullanıcı " + CustomErrorMessage.InsertErrorMessage);
                }
            }
            else
                throw new ClientSideException("Bu Daire Başkası Üzerine Kayıtlı");
        }

        public async Task<List<GetUserDto>> GetListWithInclude(Expression<Func<User, bool>> predicate, bool checkstatus = false, bool tracking = true, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, params Expression<Func<User, object>>[] includes)
        {
            var users = await _userRepo.GetListWithInclude(predicate, checkstatus, tracking, orderBy, includes);
            return _mapper.Map<List<GetUserDto>>(users);
        }

        public async Task<GetUserDto> GetSingleAsync(Expression<Func<User, bool>> expression, bool checkstatus = false, bool tracking = true)
        {
            var user = await _userRepo.GetSingleAsync(expression, checkstatus, tracking);
            return _mapper.Map<GetUserDto>(user);
        }

        public async Task<GetUserDto> GetSingleWtihInclude(Expression<Func<User, bool>> predicate, bool checkstatus = false, bool tracking = true, params Expression<Func<User, object>>[] includes)
        {
            var user = await _userRepo.GetSingleWtihInclude(predicate, checkstatus, tracking, includes);
            return _mapper.Map<GetUserDto>(user);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var user = await _userRepo.GetSingleAsync(x => x.Id == id, true);
            try
            {
                _userRepo.Remove(user);
                await _userRepo.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Kullanıcı " + CustomErrorMessage.DeleteErrorMessage);
            }
        }

        public async Task<bool> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepo.GetSingleAsync(x => x.Id == updateUserDto.Id, true);
            try
            {
                _userRepo.Update(_mapper.Map(updateUserDto, user));
                await _userRepo.SaveChangeAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Kullanıcı " + CustomErrorMessage.UpdatetErrorMessage);

            }
        }
    }
}
