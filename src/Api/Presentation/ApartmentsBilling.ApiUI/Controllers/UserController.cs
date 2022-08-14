using ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{

    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpPost]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> CreateAsync(CreateUserDto userdto)
        {
            await _userService.AddUserAsync(userdto, false);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Kullanıcı Başarılı Bir şekilde Oluşturuldu"));
        }



        [HttpPut]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> UpdateAsync(UpdateUserDto updateUser)
        {
            await _userService.UpdateAsync(updateUser);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Kullanıcı Başarılı Bir şekilde Güncelleştirildi"));
        }

        [HttpGet]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetListAsync()
        {
            return CreatActionResult(CustomResponseDto<List<GetUserDto>>.SuccesWithData(_mapper.Map<List<GetUserDto>>(await _userService.GetListWithInclude(null, true, true, x => x.OrderByDescending(x => x.CreatedDate), x => x.Flat))));
        }

        [HttpGet("Edit")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetListForEditAsync()
        {
            return CreatActionResult(CustomResponseDto<List<GetUserDto>>.SuccesWithData(_mapper.Map<List<GetUserDto>>(await _userService.GetListWithInclude(null, true, false, x => x.OrderByDescending(x => x.CreatedDate), x => x.Flat))));
        }

        [HttpGet("{id}")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            var user = await _userService.GetSingleAsync(x => x.Id == id, true);
            return CreatActionResult(CustomResponseDto<GetUserDto>.SuccesWithData(user));
        }

        [HttpDelete("{id}")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _userService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<GetUserDto>.SuccesWithOutData("Kullanıcı Silindi"));

        }
    }
}
