using ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.ViewModels;
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
    public class BillTypeController : CustomBaseController
    {

        private readonly IBillTypeService _billTypeService;
        private readonly IMapper _mapper;

        public BillTypeController(IBillTypeService billTypeService, IMapper mapper)
        {
            _billTypeService = billTypeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBillTypeDto createBillTypeDto)
        {

            if (await _billTypeService.AddAsync(_mapper.Map<BillType>(createBillTypeDto)))
            {
                return CreatActionResult(CustomResponseDto<List<NoContent>>.SuccesWithOutData("Fatura Tipi Oluşturuldu"));
            }
            return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateBillTypeDto updateBillTypeDto)
        {
            var value = _mapper.Map(updateBillTypeDto, await _billTypeService.GetSingleAsync(x => x.Id == updateBillTypeDto.Id, true));
            if (await _billTypeService.UpdateAsync(_mapper.Map<BillType>(value)))
            {
                return CreatActionResult(CustomResponseDto<List<NoContent>>.SuccesWithOutData("Ürün Güncelleme Başarılı"));
            }
            return BadRequest();
        }

        [HttpGet]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetListAsync()
        {
            var value = await _billTypeService.GetAllAsync(x => x.OrderByDescending(x => x.CreatedDate), true, false);
            return CreatActionResult(CustomResponseDto<List<GetBillTypeVm>>.SuccesWithData(_mapper.Map<List<GetBillTypeVm>>(value)));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> GetListForEditAsync()
        {
            var value = await _billTypeService.GetAllAsync(x => x.OrderByDescending(x => x.CreatedDate), true, false);
            return CreatActionResult(CustomResponseDto<List<GetBillTypeVm>>.SuccesWithData(_mapper.Map<List<GetBillTypeVm>>(value)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            var value = await _billTypeService.GetSingleAsync(x => x.Id == id, checkstatus: true, tracking: false);
            return CreatActionResult(CustomResponseDto<GetBillTypeVm>.SuccesWithData(_mapper.Map<GetBillTypeVm>(value)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _billTypeService.GetSingleAsync(x => x.Id == id, true);
            await _billTypeService.RemoveAsync(await _billTypeService.GetSingleAsync(x => x.Id == id, true));
            return CreatActionResult(CustomResponseDto<List<NoContent>>.SuccesWithOutData("Ürün Silme Başarılı"));
        }
    }
}
