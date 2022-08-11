using ApartmentsBilling.BussinessLayer.Configuration.Exceptions;
using ApartmentsBilling.BussinessLayer.Features.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.PaymentDto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [AllowAnonymous]
    public class PaymentController : CustomBaseController
    {
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        readonly HttpClient client = new();

        public PaymentController(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
            client.BaseAddress = new Uri("https://localhost:44382/api/");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BillPaymentDto billPaymentDto)
        {

            var value = await _billService.GetSingleWtihInclude(x => x.Id == billPaymentDto.BillId, true, true, x => x.Flat.User, x => x.BillType);
            if (value == null)
                return NotFound();
            if (value.IsPayment)
                throw new ClientSideException("Bu Fatura Daha Önce Ödenmiş");
            PaymentDto dto = new()
            {
                BillPaymentDto = billPaymentDto,
                CreateBillPaymentDto = _mapper.Map<CreateBillReceiptDto>(value)
            };
            var response = await client.PostAsJsonAsync("Receipt", dto);

            if (response.IsSuccessStatusCode)
            {
                value.IsPayment = true;
                await _billService.UpdateAsync(value);
                return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Ödeme Başarıyla gerçekleşti"));
            }
            throw new Exception("Ödeme Başarısız Oldu Lütfen Tekrar deneyin!");

        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var values = await client.GetFromJsonAsync<List<GetReceiptDto>>(client.BaseAddress + "Receipt");
            var newValue = new List<GetReceiptDto>();
            if (values == null)
                return BadRequest();
            if (!IsAuthorize())
            {
                foreach (var item in values)
                {
                    if (item.UserId.ToString() == User().Id)
                        newValue.Add(item);
                }
                return CreatActionResult(CustomResponseDto<List<GetReceiptDto>>.SuccesWithData(newValue));
            }
            else
                return CreatActionResult(CustomResponseDto<List<GetReceiptDto>>.SuccesWithData(values));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(string id)
        {
            var response = await client.GetFromJsonAsync<GetReceiptDto>(client.BaseAddress + $"Receipt/{id}");
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(string id)
        {
            var response = await client.DeleteAsync(client.BaseAddress + $"Receipt/{id}");

            if (response.IsSuccessStatusCode)
                return Ok("Fiş Silindi");
            else
                return BadRequest();
        }
    }
}
