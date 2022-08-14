using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.FlatDto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    //[Permission(UserRole.Admin)]
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
            await _flatService.AddAsync(createFlatDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Daire Eklendi!"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateFlatDto updateFlatDto)
        {
            var result = await _flatService.UpdateAsync(updateFlatDto);
            if (result)
                return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Daire Güncelleme Başarılı"));
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var flats = await _flatService.GetListWithInclude(null);
            return CreatActionResult(CustomResponseDto<List<GetFlatDto>>.SuccesWithData(flats));
        }

        //[HttpGet("Edit")]
        //public async Task<IActionResult> GetListForEditAsync()
        //{
        //    return CreatActionResult(CustomResponseDto<List<GetFlatDto>>.SuccesWithData(_mapper.Map<List<GetFlatDto>>(await _flatService.GetListWithInclude(null, tracking: false, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User))));
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            var flat = await _flatService.GetSingleAsync(x => x.Id == id);
            return CreatActionResult(CustomResponseDto<GetFlatDto>.SuccesWithData(flat));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _flatService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithData(null, "Daire Silindi"));
        }
    }
}
