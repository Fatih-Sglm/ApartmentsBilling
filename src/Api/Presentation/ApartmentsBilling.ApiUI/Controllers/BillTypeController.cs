using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    // [Permission(UserRole.Admin)]
    public class BillTypeController : CustomBaseController
    {

        private readonly IBillTypeService _billTypeService;

        public BillTypeController(IBillTypeService billTypeService)
        {
            _billTypeService = billTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBillTypeDto createBillTypeDto)
        {
            await _billTypeService.AddAsync(createBillTypeDto);
            return CreatActionResult(CustomResponseDto<List<NoContent>>.SuccesWithOutData("Fatura Tipi Ekleme Başarılı"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateBillTypeDto updateBillTypeDto)
        {
            await _billTypeService.UpdateAsync(updateBillTypeDto);
            return CreatActionResult(CustomResponseDto<List<NoContent>>.SuccesWithOutData("Ürün Güncelleme Başarılı"));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var value = await _billTypeService.GetAll(null, x => x.OrderByDescending(x => x.CreatedDate), true, false);
            return CreatActionResult(CustomResponseDto<List<GetBillTypeDto>>.SuccesWithData(value));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> GetListForEditAsync()
        {
            var value = await _billTypeService.GetAll(null, x => x.OrderByDescending(x => x.CreatedDate), false, false);
            return CreatActionResult(CustomResponseDto<List<GetBillTypeDto>>.SuccesWithData(value));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            var value = await _billTypeService.GetSingleAsync(x => x.Id == id, checkstatus: true, tracking: false);
            return CreatActionResult(CustomResponseDto<GetBillTypeDto>.SuccesWithData(value));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _billTypeService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<List<NoContent>>.SuccesWithOutData("Ürün Silme Başarılı"));
        }
    }
}
