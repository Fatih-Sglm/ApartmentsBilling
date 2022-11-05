using ApartmentsBilling.Common.Dtos.CustomDto;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBilling.PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreatActionResult<GetBillTypeDto>(CustomResponseDto<GetBillTypeDto> dto)
        {
            return new ObjectResult(dto)
            {
                StatusCode = Response.StatusCode,
            };
        }
    }
}
