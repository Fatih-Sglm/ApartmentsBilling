using ApartmentsBilling.Common.Dtos.PaymentDto;
using ApartmentsBilling.PaymentApiSevices.Entities;
using ApartmentsBilling.PaymentApiSevices.Features.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _paymentService;
        private readonly IMapper _mapper;
        public ReceiptController(IReceiptService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentDto paymentDto)
        {
            Random random = new();
            if (paymentDto.BillPaymentDto == null || !ModelState.IsValid) return BadRequest();
            var v = _mapper.Map<Receipt>(paymentDto.CreateBillPaymentDto);
            v.PaymentNumber = random.Next(111111, 999999);
            if (await _paymentService.CreateAsync(v))
                return Ok();
            return BadRequest();
        }
        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_mapper.Map<List<GetReceiptDto>>(_paymentService.GetAll()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (id == null) return BadRequest();
            if (await _paymentService.GetById(id) == null) return NoContent();
            return Ok(_mapper.Map<GetReceiptDto>(await _paymentService.GetById(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            if (id == null) return BadRequest();
            if (await _paymentService.GetById(id) == null) return NoContent();
            await _paymentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
