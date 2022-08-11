using ApartmentsBilling.BussinessLayer.Configuration.Filter.FilterAttirbute;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Cache.Interfaces;
using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Entity.Entities;
using AutoMapper;
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
        private readonly IBillTypeService _billTypeService;
        private readonly IFlatService _flatService;
        private readonly IMapper _mapper;
        private readonly IRedisService _redis;
        public BillController(IBillService billService, IMapper mapper, IRedisService redis, IBillTypeService billTypeService, IFlatService flatService)
        {
            _billService = billService;
            _mapper = mapper;
            _redis = redis;
            _billTypeService = billTypeService;
            _flatService = flatService;
        }

        [HttpPost]
        //[Permission(UserRole.Admin)]
        public async Task<IActionResult> CreateAsync(List<CreateBillDto> createBillDtos)
        {
            if (await _billService.CreateBill(createBillDtos))
                return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Veri Başarıyla eklendi!"));
            throw new Exception("Kayıt Başarısız Lütfen Tekrar Deneyiniz!");
        }

        [HttpPut]
        //[Permission(UserRole.Admin)]
        public async Task<IActionResult> UpdateAsync(UpdateBillDto updateBillDto)
        {
            await _billService.GetSingleAsync(x => x.Id == updateBillDto.Id, true);
            await _flatService.GetSingleAsync(x => x.Id == updateBillDto.FlatId, true, false);
            await _billTypeService.GetSingleAsync(x => x.Id == updateBillDto.BillTypeId, true, false);
            var value = _mapper.Map(updateBillDto, await _billService.GetSingleAsync(x => x.Id == updateBillDto.Id, true));
            await _billService.UpdateAsync(_mapper.Map<Bill>(value));
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Fatura Başarılı Bir Şekilde Güncellendi"));
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            //if (!IsAuthorize())
            //    return CreatActionResult(CustomResponseDto<List<BillDto>>.SuccesWithData(_mapper.Map<List<BillDto>>(await _billService.GetListWithInclude(x => x.Flat.User.Id == Guid.Parse(User().Id), true, false, x => x.OrderByDescending(x => x.CreatedDate), x => x.BillType, x => x.Flat, x=>x.Flat.User))));
            return CreatActionResult(CustomResponseDto<List<BillDto>>.SuccesWithData(_mapper.Map<List<BillDto>>(await _billService.GetListWithInclude(null, true, false, x => x.OrderByDescending(x => x.CreatedDate), x => x.BillType, x => x.Flat, x => x.Flat.User))));
        }

        [HttpGet("Edit")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> GetListForEdit()
        {
            return CreatActionResult(CustomResponseDto<List<BillDto>>.SuccesWithData(_mapper.Map<List<BillDto>>(await _billService.GetListWithInclude(null, false, false, null, x => x.BillType, x => x.Flat))));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(Guid id)
        {
            await _billService.GetSingleAsync(x => x.Id == id, true, false);
            return CreatActionResult(CustomResponseDto<BillDto>.SuccesWithData(_mapper.Map<BillDto>(await _billService.GetSingleWtihInclude(x => x.Id == id, true, true, x => x.BillType, x => x.Flat))));
        }



        [HttpDelete("{id}")]
        [Permission(UserRole.Admin)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var v = await _billService.GetSingleAsync(x => x.Id == id, true);
            await _billService.RemoveAsync(v);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Fatura Başarıyla Silindi"));
        }
    }
}
