using ApartmentsBilling.Common.Dtos.BillsDto;
using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApartmentsBilling.WebApp.Controllers
{
    public class BillController : BaseController
    {
        private readonly IMapper _mapper;

        public BillController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> ListBillAsync()
        {
            IsAuthentic();
            var response = await _client.GetAsync(_client.BaseAddress + "Bill");
            if (response.IsSuccessStatusCode)
            {
                var bill = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<BillDto>>>();
                return View(bill.Data);
            }
            else
                return await ErrorView(response);
            //if (response.StatusCode == 403) return RedirectToAction("UnAutHorized", "Error");


        }


        public async Task<IActionResult> Update(Guid id)
        {
            IsAuthentic();
            var response = await _client.GetAsync(_client.BaseAddress + $"Bill/{id}");

            if (response.IsSuccessStatusCode)
            {
                var respons = await _client.GetFromJsonAsync<CustomResponseDto<List<GetBillTypeDto>>>(_client.BaseAddress + "BillType");
                ViewBag.BillType = respons.Data;
                var bill = await response.Content.ReadFromJsonAsync<CustomResponseDto<BillDto>>();
                return View(_mapper.Map<UpdateBillDto>(bill.Data));
            }
            else
                return await ErrorView(response);
        }
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateBillDto updateBillDto)
        {
            IsAuthentic();
            var response = await _client.PutAsJsonAsync("Bill", updateBillDto);
            if (response.IsSuccessStatusCode)
            {
                var bill = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoContent>>();
                return View(bill.Data);
            }
            else
                return await ErrorView(response);
        }

        //[HttpGet]
        //public async Task<IActionResult> CreateBill(string txt)
        //{
        //    ViewBag.Count = Int32.Parse(txt);
        //    //if (IsAuthentic())
        //    //{
        //    var response = await _client.GetFromJsonAsync<CustomResponseDto<List<GetBillTypeVm>>>(_client.BaseAddress + "BillType");

        //    ViewBag.data = response.Data;
        //    return View();
        //    //else
        //    //{
        //    //    RedirectToAction("Forbidden", "Login");
        //    //}

        //    // RedirectToAction("Forbidden", "Login");
        //    //}
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateBillAsync(CreateBillVm CreateBillVm)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //    else
        //    {

        //    }

        //    return RedirectToAction("Index", "User");
        //}
    }
}
