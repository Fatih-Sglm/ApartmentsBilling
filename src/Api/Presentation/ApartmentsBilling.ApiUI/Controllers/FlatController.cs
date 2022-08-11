using ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.FlatDto;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [Permission(UserRole.Admin)]
    public class FlatController : CustomBaseController
    {
        private readonly IFlatService _flatService;
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public FlatController(IFlatService flatService, IMapper mapper, IApartmentService apartmentService)
        {
            _flatService = flatService;
            _mapper = mapper;
            _apartmentService = apartmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateFlatDto createFlatDto)
        {
            var v = _mapper.Map<Flat>(createFlatDto);
            if (await _flatService.AddAsync(v))
                return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Daire Eklendi!"));
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateFlatDto updateFlatDto)
        {
            var flat = _mapper.Map(updateFlatDto, await _flatService.GetSingleAsync(x => x.Id == updateFlatDto.Id, true));
            var v = _mapper.Map<Flat>(flat);
            if (await _flatService.UpdateAsync(v))
                return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Daire Güncelleme Başarılı"));
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return CreatActionResult(CustomResponseDto<List<GetFlatDto>>.SuccesWithData(_mapper.Map<List<GetFlatDto>>(await _flatService.GetListWithInclude(null, checkstatus: true, tracking: false, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User))));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> GetListForEditAsync()
        {
            return CreatActionResult(CustomResponseDto<List<GetFlatDto>>.SuccesWithData(_mapper.Map<List<GetFlatDto>>(await _flatService.GetListWithInclude(null, tracking: false, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User))));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            return CreatActionResult(CustomResponseDto<GetFlatDto>.SuccesWithData(_mapper.Map<GetFlatDto>(await _flatService.GetSingleWtihInclude(x => x.Id == id, true, false, x => x.User))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var v = await _flatService.GetSingleAsync(x => x.Id == id, true);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithData(null, "Daire Silindi"));
        }
    }
}
