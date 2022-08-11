using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApartmentsBilling.WebApp.Controllers
{
    public class BillTypeController : BaseController
    {
        private readonly IMapper _mapper;
        public BillTypeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Update(Guid id)
        {
            IsAuthentic();
            var response = await _client.GetAsync(_client.BaseAddress + $"BillType/{id}");
            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<CustomResponseDto<GetBillTypeVm>>();
                return View(_mapper.Map<UpdateBillTypeDto>(value.Data));
            }
            return await ErrorView(response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBillTypeDto updateBillTypeDto)
        {
            var response = await _client.PutAsJsonAsync("BillType", updateBillTypeDto);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoContent>>();
                TempData["Meesage"] = body.Message;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Forbidden", "Login");
        }
    }
}
