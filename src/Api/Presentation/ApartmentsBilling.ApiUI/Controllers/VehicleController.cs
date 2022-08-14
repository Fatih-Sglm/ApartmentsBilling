using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.VehicleDto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    public class VehicleController : CustomBaseController
    {
        private readonly IMapper _mapper;

        private readonly IVehicleService _vehicleService;

        public VehicleController(IMapper mapper, IVehicleService vehicleService)
        {
            _mapper = mapper;
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleDto createVehicleDto)
        {
            await _vehicleService.AddAsync(createVehicleDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Araç Eklendi"));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateVehicleDto updateVehicleDto)
        {
            await _vehicleService.UpdateAsync(updateVehicleDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Araç Güncellendi"));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!IsAuthorize())
            {
                var value = await _vehicleService.GetListWithInclude(x => x.UserId == Guid.Parse(User().Id), checkstatus: true, tracking: false, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User);
                var map = _mapper.Map<List<GetVehicleDto>>(value);
                return CreatActionResult(CustomResponseDto<List<GetVehicleDto>>.SuccesWithData(map));
            }
            var values = await _vehicleService.GetListWithInclude(null, checkstatus: true, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User);
            if (values == null)
                return BadRequest();
            return CreatActionResult(CustomResponseDto<List<GetVehicleDto>>.SuccesWithData(_mapper.Map<List<GetVehicleDto>>(values)));
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> GetAllForEdit()
        {
            if (!IsAuthorize())
            {
                var value = await _vehicleService.GetListWithInclude(x => x.UserId == Guid.Parse(User().Id), tracking: false, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User);
                var map = _mapper.Map<List<GetVehicleDto>>(value);
                return CreatActionResult(CustomResponseDto<List<GetVehicleDto>>.SuccesWithData(map));
            }
            var values = await _vehicleService.GetListWithInclude(null, orderBy: x => x.OrderByDescending(x => x.CreatedDate), includes: x => x.User);
            if (values == null)
                return BadRequest();
            return CreatActionResult(CustomResponseDto<List<GetVehicleDto>>.SuccesWithData(_mapper.Map<List<GetVehicleDto>>(values)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var values = await _vehicleService.GetSingleWtihInclude(x => x.Id == id, checkstatus: true, tracking: false, includes: x => x.User);
            return CreatActionResult(CustomResponseDto<GetVehicleDto>.SuccesWithData(_mapper.Map<GetVehicleDto>(values)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            await _vehicleService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Araç Silindi"));
        }
    }
}
