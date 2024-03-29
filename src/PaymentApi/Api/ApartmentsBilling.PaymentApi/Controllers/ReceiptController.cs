﻿using ApartmentsBilling.Common.Dtos.PaymentDto;
using ApartmentsBilling.PaymentApiSevices.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApi.Controllers
{
    public class ReceiptController : CustomBaseController
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentDto paymentDto)
        {
            if (paymentDto.BillPaymentDto == null || !ModelState.IsValid) return BadRequest();
            await _receiptService.CreatePayment(paymentDto);
            return Ok();
        }
        [HttpGet("{userId?}")]
        public async Task<IActionResult> GetList([FromRoute] string userId = null)
        {
            return Ok(await _receiptService.GetAllReceipt(userId));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(string id)
        {
            return Ok(await _receiptService.GetSingleReceipt(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _receiptService.Remove(id);
            return Ok();
        }
    }
}
