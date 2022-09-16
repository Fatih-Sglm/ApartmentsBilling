using ApartmentsBilling.BussinessLayer.Configuration.Authorize;
using ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{

    public class BillController : CustomBaseController
    {
        private readonly IBillService _billService;
        private readonly IAuthHorize _authHorize;
        public BillController(IBillService billService, IAuthHorize authHorize)
        {
            _billService = billService;
            _authHorize = authHorize;
        }

        [HttpPost]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> CreateAsync(List<CreateBillDto> createBillDtos)
        {
            await _billService.AddRangeAsync(createBillDtos);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Veri Başarıyla eklendi!"));
        }

        [HttpPut]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> UpdateAsync(UpdateBillDto updateBillDto)
        {
            await _billService.UpdateAsync(updateBillDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Fatura Başarılı Bir Şekilde Güncellendi"));

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetList()
        {
            if (!_authHorize.IsAuthorize())
                return CreatActionResult(CustomResponseDto<List<BillDto>>.SuccesWithData(await _billService.GetListWithInclude(x => x.Flat.User.Id == Guid.Parse(_authHorize.User().Id), true, false, x => x.OrderByDescending(x => x.CreatedDate), x => x.BillType, x => x.Flat, x => x.Flat.User)));
            return CreatActionResult(CustomResponseDto<List<BillDto>>.SuccesWithData(await _billService.GetListWithInclude(null, true, false, x => x.OrderByDescending(x => x.CreatedDate), x => x.BillType, x => x.Flat, x => x.Flat.User)));
        }

        [HttpGet("Edit")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetListForEdit()
        {
            return CreatActionResult(CustomResponseDto<List<BillDto>>.SuccesWithData(await _billService.GetListWithInclude(null, false, false, null, x => x.BillType, x => x.Flat)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            return CreatActionResult(CustomResponseDto<BillDto>.SuccesWithData(await _billService.GetSingleAsync(x => x.Id == id, true, false)));
        }

        [HttpDelete("{id}")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _billService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Fatura Başarıyla Silindi"));
        }
    }
}
