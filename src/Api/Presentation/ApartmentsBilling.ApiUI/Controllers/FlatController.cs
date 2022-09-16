using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.FlatDto;
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

        public FlatController(IFlatService flatService)
        {
            _flatService = flatService;
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
            var flats = await _flatService.GetListWithInclude(null, true, false);
            return CreatActionResult(CustomResponseDto<List<GetFlatDto>>.SuccesWithData(flats));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> GetListForEditAsync()
        {
            var flats = await _flatService.GetListWithInclude(null, false, false);
            return CreatActionResult(CustomResponseDto<List<GetFlatDto>>.SuccesWithData(flats));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            var flat = await _flatService.GetSingleAsync(x => x.Id == id, true, false);
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
