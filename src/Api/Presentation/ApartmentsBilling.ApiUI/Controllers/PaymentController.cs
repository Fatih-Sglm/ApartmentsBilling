using ApartmentsBilling.BussinessLayer.Abstract.InterFaces;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.PaymentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApartmentsBilling.ApiUI.Controllers
{
    [AllowAnonymous]
    public class PaymentController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BillPaymentDto billPaymentDto)
        {
            await _paymentService.CreatePayment(billPaymentDto);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Ödeme Başarıyla gerçekleşti"));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return CreatActionResult(CustomResponseDto<List<GetReceiptDto>>.SuccesWithData(await _paymentService.GetListAsync()));
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(string id)
        {
            return CreatActionResult(CustomResponseDto<GetReceiptDto>.SuccesWithData(await _paymentService.GetSingleAsync(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(string id)
        {
            await _paymentService.RemoveAsync(id);
            return CreatActionResult(CustomResponseDto<NoContent>.SuccesWithOutData("Fiş Silindi"));
        }
    }
}
