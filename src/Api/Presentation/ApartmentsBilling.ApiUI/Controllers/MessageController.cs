using ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.MessageDto;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    public class MessageController : CustomBaseController
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IMapper mapper, IUserService userService)
        {
            _messageService = messageService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Permission(UserRole.User)]
        public async Task<IActionResult> CreateAsync(CreateMessageDto createMessageDto)
        {
            await _messageService.AddAsync(createMessageDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Mesaj Gönerildi"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateMessageDto updateMessageDto)
        {
            await _messageService.UpdateAsync(updateMessageDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Mesaj Güncellendi"));
        }
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            if (!IsAuthorize())
            {
                var values = await _messageService.GetListWithInclude(x => x.UserId == Guid.Parse(User().Id), checkstatus: true, tracking: true, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User);
                return CreatActionResult(CustomResponseDto<List<GetMessageDto>>.SuccesWithData(_mapper.Map<List<GetMessageDto>>(values)));
            }
            var value = await _messageService.GetListWithInclude(null, orderBy: x => x.OrderByDescending(x => x.CreatedDate), checkstatus: true, includes: x => x.User);
            if (value == null)
                return BadRequest();
            return CreatActionResult(CustomResponseDto<List<GetMessageDto>>.SuccesWithData(_mapper.Map<List<GetMessageDto>>(value)));
        }

        [HttpGet("Edit")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetListForEditAsync()
        {
            return CreatActionResult(CustomResponseDto<List<GetMessageDto>>.SuccesWithData(_mapper.Map<List<GetMessageDto>>(await _messageService.GetListWithInclude(null, tracking: true, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User))));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            var value = await _messageService.GetSingleWtihInclude(x => x.Id == id, checkstatus: true, includes: x => x.User);
            return CreatActionResult(CustomResponseDto<GetMessageDto>.SuccesWithData(_mapper.Map<GetMessageDto>(value), null));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _messageService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Mesaj Silindi"));
        }
    }
}
